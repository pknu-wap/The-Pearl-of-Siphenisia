using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    #region 변수
    public Item item;
    public Image icon;
    public DragSlot dragSlot;
    public ItemInfoWindow infoWindow;
    #endregion 변수

    #region 초기 설정
    private void Awake()
    {
        AssignObjects();

        // 세이브 파일에서 아이템을 받아온 후
        
        UpdateSlot();
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
            // 아이템을 사용한다.
            item.UseItem();
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
        if(item == null)
        {
            return;
        }

        dragSlot.SetItem(item);
        dragSlot.ShowImage();
    }

    /// <summary>
    /// 드래그 중 호출된다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (item == null)
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
        if (item == null)
        {
            return;
        }

        dragSlot.HideImage();
        AddItem(dragSlot.dragItem);
        UpdateSlot();
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

    /// <summary>
    /// 슬롯을 서로 변경하는 함수.
    /// </summary>
    void ChangeSlot()
    {
        Item temp = item;

        AddItem(dragSlot.dragItem);
        UpdateSlot();

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
    public void AddItem(Item item)
    {
        this.item = item;
    }

    /// <summary>
    /// 아이템 삭제
    /// </summary>
    public void RemoveItem()
    {
        item = null;

        UpdateSlot();
    }

    /// <summary>
    /// 현재 아이템으로 UI 갱신
    /// </summary>
    public void UpdateSlot()
    {
        if(item == null)
        {
            icon.enabled = false;
            return;
        }

        // 아이콘을 변경하고 종료
        icon.sprite = item.itemData.icon;
        // + 주인공 손에 스프라이트 겹치는 로직 생성 예정
        icon.enabled = true;
    }

    /// <summary>
    /// 상세정보 표시
    /// </summary>
    public void ShowInfo()
    {
        // 아이템이 없다면 띄우지 않는다.
        if(item == null)
        {
            return;
        }

        // 자신의 위치로 상세정보창을 이동
        infoWindow.ChangePosition(transform.position);
        infoWindow.UpdateInfo(item.itemData);
        infoWindow.ShowInfoUI();
    }

    /// <summary>
    /// 상세정보 숨김
    /// </summary>
    public void HideInfo()
    {
        // 아이템이 없다면 호출하지 않는다.
        if (item == null)
        {
            return;
        }

        infoWindow.HideInfoUI();
    }

    /// <summary>
    /// 비어 있는 슬롯이라면 true 반환
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return item == null;
    }
}
