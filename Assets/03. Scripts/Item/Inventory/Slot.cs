using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    #region 변수
    public Item slotItem;
    public Image icon;
    public DragSlot dragSlot;
    public ItemInfoWindow infoWindow;
    private QuickSlot quickSlot;

    public UnityEvent[] clickEvent = new UnityEvent[3];

    // 현재 슬롯이 장비/장착/소비 슬롯 중 어떤 것인가?
    public UseTag slotTag;

    [SerializeField]
    private GameObject itemStatus;  // E 마크, Q 마크, 숫자 등 상태를 나타내는 오브젝트
    [SerializeField]
    private TextMeshProUGUI countText;
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
        infoWindow = GameObject.Find("Item Info Window").GetComponent<ItemInfoWindow>();
        quickSlot = GameObject.Find("Quick Slot").GetComponent<QuickSlot>();

        itemStatus = transform.GetChild(1).gameObject;

        // 소비 슬롯이라면 텍스트도 받아온다.
        if(slotTag == UseTag.Consume)
        {
            countText = itemStatus.GetComponent<TextMeshProUGUI>();
        }
    }

    void AddEvent()
    {
        // 장비 아이템 사용 시
        clickEvent[(int)UseTag.Equip].AddListener(HideInfo);
        clickEvent[(int)UseTag.Equip].AddListener(ToggleEquip);

        // 핸드 아이템 사용 시
        clickEvent[(int)UseTag.Hand].AddListener(HideInfo);
        clickEvent[(int)UseTag.Hand].AddListener(ToggleHand);

        // 소비 아이템 사용 시
        clickEvent[(int)UseTag.Consume].AddListener(HideInfo);
        clickEvent[(int)UseTag.Consume].AddListener(ActivateCurrentItem);
        clickEvent[(int)UseTag.Consume].AddListener(DecreaseItemCount);
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
        if(slotItem == null)
        {
            return;
        }

        dragSlot.SetItem(slotItem);
        dragSlot.ShowImage();
    }

    /// <summary>
    /// 드래그 중 호출된다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (slotItem == null)
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
        if (slotItem == null)
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
    /// 현재 슬롯과 드래그 슬롯의 아이템을 변경한다.
    /// </summary>
    void ChangeSlot()
    {
        Item temp = slotItem;

        AddItem(dragSlot.dragItem);
        UpdateSlotUI();

        // 다른 슬롯에 추가하기 위해 dragItem에 temp를 넣어둔다.
        dragSlot.SetItem(temp);
    }

    /// <summary>
    /// 아이템 추가
    /// </summary>
    /// <param name="item"></param> 
    public void AddItem(Item item)
    {
        if(item == null)
        {
            slotItem = item;
            return;
        }

        // 같은 소비 아이템이 존재한다면 개수를 1 증가시키고 종료
        if(item.itemData.useTag == UseTag.Consume && item == slotItem)
        {
            item.count++;
            return;
        }

        // 그 외 슬롯에 새 아이템 추가
        slotItem = item;
        item.transform.parent = transform;
    }

    /// <summary>
    /// 아이템 개수를 1 줄인다.
    /// </summary>
    public void DecreaseItemCount()
    {
        if(slotItem == null)
        {
            return;
        }

        // 소비 아이템이 아니거나, 소비 아이템의 개수를 1 줄인 후 0 이하일 때 비운다.
        if(slotItem.itemData.useTag != UseTag.Consume || --slotItem.count <= 0)
        {
            slotItem = null;
        }

        UpdateSlotUI();
    }

    /// <summary>
    /// 슬롯에 등록된 아이템을 사용한다. 해당 아이템의 clickEvent가 실행된다.
    /// </summary>
    void ActivateCurrentItem()
    {
        if (slotItem == null)
        {
            return;
        }

        slotItem.ActivateItem();
    }

    /// <summary>
    /// 슬롯을 클릭했을 때 실행되는 함수. 아이템 종류에 따라 3가지 이벤트 중 하나가 실행된다.
    /// </summary>
    void ClickItem()
    {
        if (slotItem == null)
        {
            return;
        }

        // UseTag에 맞는 이벤트 실행
        clickEvent[(int)slotItem.itemData.useTag].Invoke();
    }

    /// <summary>
    /// Equip/Unequip 상태를 토글한다.
    /// </summary>
    void ToggleEquip()
    {
        if(slotItem == null)
        {
            return;
        }

        if(slotItem.isEquiped == true)
        {
            UnequipItem();
            slotItem.DeactivateItem();
        }

        else
        {
            EquipItem();
            slotItem.ActivateItem();
        }
    }

    /// <summary>
    /// 아이템을 장착했음을 표시한다.
    /// </summary>
    void EquipItem()
    {
        itemStatus.SetActive(true);
        slotItem.isEquiped = true;
    }

    /// <summary>
    /// 아이템을 해제했음을 표시한다.
    /// </summary>
    void UnequipItem()
    {
        itemStatus.SetActive(false);
        slotItem.isEquiped = false;
    }

    /// <summary>
    /// Hand 상태를 토글한다.
    /// </summary>
    void ToggleHand()
    {
        if (slotItem == null)
        {
            return;
        }

        if (slotItem.isEquiped == true)
        {
            UnhandItem();
        }

        else
        {
            HandItem();
        }
    }

    /// <summary>
    /// 아이템을 퀵슬롯에 등록한다.
    /// </summary>
    void HandItem()
    {
        itemStatus.SetActive(true);
        slotItem.isEquiped = true;
        quickSlot.SetInventorySlot(this);
        quickSlot.SetItem(slotItem);
    }
    
    /// <summary>
    /// 아이템을 퀵슬롯에서 해제한다.
    /// </summary>
    public void UnhandItem()
    {
        itemStatus.SetActive(false);
        slotItem.isEquiped = false;
        quickSlot.ClearQuickSlot();
    }
    #endregion 아이템 상호작용

    #region UI
    /// <summary>
    /// 현재 아이템으로 UI 갱신
    /// </summary>
    public void UpdateSlotUI()
    {
        if(slotItem == null)
        {
            icon.enabled = false;
            itemStatus.SetActive(false);
            return;
        }

        icon.sprite = slotItem.itemData.icon;
        // TODO: 주인공 손에 아이템 스프라이트를 겹친다.
        icon.enabled = true;

        // 소비 아이템일 경우 개수도 최신화
        if (slotItem.itemData.useTag == UseTag.Consume)
        {
            itemStatus.SetActive(true);
            countText.text = slotItem.count.ToString();
        }
        // 그 외 아이템은 장착 여부 최신화
        else
        {
            itemStatus.SetActive(slotItem.isEquiped);
        }
    }

    /// <summary>
    /// 상세정보 표시
    /// </summary>
    public void ShowInfo()
    {
        // 아이템이 없다면 띄우지 않는다.
        if(slotItem == null)
        {
            return;
        }

        // 자신의 위치로 상세정보창을 이동
        infoWindow.ChangePosition(transform.position);
        infoWindow.UpdateInfo(slotItem.itemData);
        infoWindow.ShowInfoUI();
    }

    /// <summary>
    /// 상세정보 숨김
    /// </summary>
    public void HideInfo()
    {
        // 아이템이 없다면 호출하지 않는다.
        if (slotItem == null)
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
        return slotItem == null;
    }
}
