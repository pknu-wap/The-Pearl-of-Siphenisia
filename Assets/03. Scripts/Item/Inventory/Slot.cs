using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public ItemData itemData;
    public Image icon;
    public DragSlot dragSlot;
    public ItemInfoWindow infoWindow;

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

    #region 마우스 이벤트
    /// <summary>
    /// 클릭 시 이벤트, 현재 비어있음
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {

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
        ItemData temp = itemData;

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
    public void AddItem(ItemData item)
    {
        itemData = item;
    }

    /// <summary>
    /// 아이템 삭제
    /// </summary>
    public void RemoveItem()
    {
        itemData = null;

        UpdateSlot();
    }

    /// <summary>
    /// 현재 아이템으로 UI 갱신
    /// </summary>
    public void UpdateSlot()
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

    /// <summary>
    /// 비어 있는 슬롯이라면 true 반환
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return itemData == null;
    }
}
