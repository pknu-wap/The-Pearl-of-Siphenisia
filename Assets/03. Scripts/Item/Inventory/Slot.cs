using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    #region 변수
    public ItemData itemData;
    public Image icon;
    public DragSlot dragSlot;
    public ItemInfoWindow infoWindow;

    public UnityEvent[] useItemEvent = new UnityEvent[3];
    #endregion 변수

    #region 초기 설정
    private void Awake()
    {
        AssignObjects();
        AddEvent();

        // TODO: 세이브 파일에서 아이템을 받아온다.

        UpdateSlotUI();
    }

    /// <summary>
    /// 변수 할당
    /// </summary>
    void AssignObjects()
    {
        // 자식 오브젝트 Slot Item의 이미지
        icon = transform.GetChild(0).GetComponent<Image>();
        dragSlot = GameObject.Find("DragSlot").GetComponent<DragSlot>();
        infoWindow = GameObject.Find("ItemInfoWindow").GetComponent<ItemInfoWindow>();
    }

    void AddEvent()
    {
        // 장비 아이템 사용 시
        useItemEvent[(int)UseTag.Equip].AddListener(UseItem);
        useItemEvent[(int)UseTag.Equip].AddListener(EquipItem);
        useItemEvent[(int)UseTag.Equip].AddListener(HideInfo);

        // 핸드 아이템 사용 시
        useItemEvent[(int)UseTag.Hand].AddListener(HandItem);
        useItemEvent[(int)UseTag.Hand].AddListener(HideInfo);

        // 소비 아이템 사용 시
        useItemEvent[(int)UseTag.Consume].AddListener(UseItem);
        useItemEvent[(int)UseTag.Consume].AddListener(DecreaseItemCount);
        useItemEvent[(int)UseTag.Consume].AddListener(HideInfo);
    }

    #endregion 초기 설정

    #region 마우스 이벤트
    /// <summary>
    /// 클릭 시 이벤트
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        // 우클릭 시
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            ClickItem();
        }
    }

    /// <summary>
    /// 마우스가 슬롯 위에 올라왔을 때 호출된다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowInfo();
    }

    /// <summary>
    /// 마우스가 슬롯에서 벗어났을 때 호출된다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        HideInfo();
    }

    /// <summary>
    /// 드래그가 시작되면 호출된다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(itemData == null)
        {
            return;
        }

        dragSlot.SetItem(itemData);
        dragSlot.ShowImage();
    }

    /// <summary>
    /// 드래그 중 호출된다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (itemData == null)
        {
            return;
        }

        // 드래그 슬롯이 마우스를 따라간다.
        dragSlot.transform.position = eventData.position;
    }

    /// <summary>
    /// 드래그가 끝났을 때 호출된다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemData == null)
        {
            return;
        }

        dragSlot.HideImage();
        AddItem(dragSlot.dragItem);
        UpdateSlotUI();
    }

    /// <summary>
    /// 슬롯 위에서 마우스가 놓아졌을 때 호출, OnEndDrag보다 먼저 실행된다.
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        if(dragSlot.dragItem == null)
        {
            return;
        }

        ChangeSlot();
    }
    #endregion 마우스 이벤트

    #region 아이템 상호작용
    /// <summary>
    /// 슬롯을 서로 변경하는 함수.
    /// </summary>
    void ChangeSlot()
    {
        ItemData temp = itemData;

        AddItem(dragSlot.dragItem);
        UpdateSlotUI();

        if (temp == null)
        {
            dragSlot.ClearItem();
        }
        else
        {
            // 현재 슬롯에 아이템이 있었다면, 다른 슬롯에 추가하기 위해 dragItem에 temp를 넣어둔다.
            dragSlot.SetItem(temp);
        }
    }

    /// <summary>
    /// 아이템 추가
    /// </summary>
    /// <param name="item"></param> 
    public void AddItem(ItemData item)
    {
        this.itemData = item;
    }

    /// <summary>
    /// 아이템 삭제
    /// </summary>
    public void DecreaseItemCount()
    {
        if(itemData == null)
        {
            return;
        }

        itemData.count--;

        if(itemData.count <= 0)
        {
            itemData = null;
        }

        UpdateSlotUI();
    }

    /// <summary>
    /// 슬롯에 등록된 아이템을 사용한다. 해당 아이템의 useItemEvent가 실행된다.
    /// </summary>
    void UseItem()
    {
        if (itemData == null)
        {
            return;
        }

        itemData.useItemEvent.Invoke();
    }

    /// <summary>
    /// 슬롯을 클릭했을 때 실행되는 함수. 아이템 종류에 따라 3가지 이벤트 중 하나가 실행된다.
    /// </summary>
    void ClickItem()
    {
        if (itemData == null)
        {
            return;
        }

        // UseTag에 맞는 이벤트 실행
        useItemEvent[(int)itemData.useTag].Invoke();
    }

    void EquipItem()
    {
        // TODO: 슬롯에 E 표시를 한다.
    }

    // 퀵슬롯에 아이템을 등록한다. 즉, 손에 든다.
    void HandItem()
    {
        // TODO: 슬롯에 S 표시를 한다.
        // TODO: 퀵슬롯에 등록한다.
    }
    #endregion 아이템 상호작용

    #region UI
    /// <summary>
    /// 현재 아이템으로 UI 갱신
    /// </summary>
    public void UpdateSlotUI()
    {
        if(itemData == null)
        {
            icon.enabled = false;
            return;
        }

        // 아이콘을 변경하고 종료
        icon.sprite = itemData.icon;
        // + 주인공 손에 스프라이트 겹치는 로직 생성 예정
        icon.enabled = true;
    }

    /// <summary>
    /// 상세정보 표시
    /// </summary>
    public void ShowInfo()
    {
        // 아이템이 없다면 띄우지 않는다.
        if(itemData == null)
        {
            return;
        }

        // 자신의 위치로 상세정보창을 이동
        infoWindow.ChangePosition(transform.position);
        infoWindow.UpdateInfo(itemData);
        infoWindow.ShowInfoUI();
    }

    /// <summary>
    /// 상세정보 숨김
    /// </summary>
    public void HideInfo()
    {
        // 아이템이 없다면 호출하지 않는다.
        if (itemData == null)
        {
            return;
        }

        infoWindow.HideInfoUI();
    }
    #endregion UI

    /// <summary>
    /// 비어 있는 슬롯이라면 true 반환
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return itemData == null;
    }
}
