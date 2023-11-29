using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    #region 변수
    [Header("아이템")]
    public Item slotItem;
    public Image icon;

    [Header("인벤토리")]
    private DragSlot dragSlot;
    private ItemInfoWindow infoWindow;
    private QuickSlot quickSlot;

    [Header("플레이어 정보")]
    private PlayerHand playerHand;

    [Header("이벤트")]
    public UnityEvent[] clickEvent = new UnityEvent[3];

    // 현재 슬롯이 장비/장착/소비 슬롯 중 어떤 것인가?
    public UseTag slotTag;

    private GameObject itemStatus;  // E 마크, Q 마크, 숫자 등 상태를 나타내는 오브젝트
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
        playerHand = GameObject.FindWithTag("Player").transform.GetChild(0).GetComponent<PlayerHand>();

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

    public void AddItem(Item item)
    {
        if(item == null)
        {
            slotItem = null;
            return;
        }

        // 같은 소비 아이템이 존재한다면 개수를 1 증가시키고 종료
        if(item.itemData.useTag == UseTag.Consume && item == slotItem)
        {
            item.count++;
            return;
        }

        // 그 외 슬롯에 새 아이템 추가
        slotItem = item;    // Load에 덮어씌워지는 중
        item.transform.parent = transform;
        item.gameObject.SetActive(false);
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

        // 소비 아이템이 아니라면 삭제,
        // 소비 아이템은 개수를 1 줄인 후 0이라면 비운다.
        if(slotItem.itemData.useTag != UseTag.Consume || --slotItem.count <= 0)
        {
            slotItem = null;
        }

        UpdateSlotUI();
    }

    public void ClearSlot()
    {
        slotItem = null;

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

    void ToggleEquip()
    {
        if(slotItem.isEquiped == true)
        {
            UnequipItem();
        }

        else
        {
            EquipItem();
        }
    }

    void EquipItem()
    {
        if (slotItem.ActivateItem() == false)
        {
            return;
        }

        itemStatus.SetActive(true);
        slotItem.isEquiped = true;
        UpdateSlotUI();
    }

    public void UnequipItem()
    {
        if (slotItem.DeactivateItem() == false)
        {
            return;
        }

        itemStatus.SetActive(false);
        slotItem.isEquiped = false;
        UpdateSlotUI();
    }

    void ToggleHand()
    {
        if (slotItem.isEquiped == true)
        {
            UnhandItem();
        }

        else
        {
            HandItem();
        }
    }

    void HandItem()
    {
        if (slotItem == null)
        {
            return;
        }

        itemStatus.SetActive(true);
        slotItem.isEquiped = true;

        quickSlot.SetInventorySlot(this);
        quickSlot.SetItem(slotItem);

        UpdateSlotUI();

        // HandItemToPlayer();
    }
    
    public void UnhandItem()
    {
        if (slotItem == null)
        {
            return;
        }

        itemStatus.SetActive(false);
        slotItem.isEquiped = false;

        quickSlot.ClearQuickSlot();

        UpdateSlotUI();

        ReturnItemToSlot();
    }

    private void HandItemToPlayer()
    {
        slotItem.transform.parent = playerHand.transform;
        slotItem.transform.localPosition = Vector3.zero;
        slotItem.transform.localScale = Vector3.one;

        playerHand.HandItem(slotItem);
        slotItem.gameObject.SetActive(true);
    }

    private void ReturnItemToSlot()
    {
        slotItem.transform.parent = transform;
        slotItem.gameObject.SetActive(false);
        slotItem.transform.localPosition = Vector3.zero;
    }
    #endregion 아이템 상호작용

    #region UI
    public void UpdateSlotUI()
    {
        if(slotItem == null)
        {
            icon.enabled = false;
            itemStatus.SetActive(false);
            return;
        }

        icon.sprite = slotItem.itemData.icon;
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

        // 장착된 Hand 아이템이라면 퀵슬롯 정보 최신화
        if (slotItem.itemData.useTag == UseTag.Hand && slotItem.isEquiped)
        {
            quickSlot.currentSlot = this;
            HandItemToPlayer();
        }
    }

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

    public bool IsEmpty()
    {
        return slotItem == null;
    }
}
