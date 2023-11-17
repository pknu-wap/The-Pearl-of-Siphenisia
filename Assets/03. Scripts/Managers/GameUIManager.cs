using UnityEngine;

public class GameUIManager : Singleton<GameUIManager>
{
    private Inventory inventory;
    private ItemInfoWindow itemInfoWindow;
    private GameObject interactionUIObj;
    private Vector3 interactOffset = new(0f, 1f, 0f);

    public void Test()
    {
        Debug.Log("레이캐스트 문제 없음");
    }

    #region 초기 설정
    private void Awake()
    {
        AssignObjects();
    }

    private void Start()
    {
        HideInventoryUI();
        HideInteractionUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventoryUI();
        }
    }

    private void AssignObjects()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        interactionUIObj = GameObject.Find("Interaction Button");
        itemInfoWindow = GameObject.Find("Item Info Window").GetComponent<ItemInfoWindow>();
    }
    #endregion 초기 설정

    #region Inventory
    // 인벤토리 UI가 켜져 있으면 끄고, 꺼져 있으면 켜는 함수
    public void ToggleInventoryUI()
    {
        if (inventory.IsInventoryShowed() == false)
        {
            inventory.ShowInventoryUI();
        }

        else
        {
            inventory.HideInventoryUI();
            itemInfoWindow.HideInfoUI();
        }
    }

    public void ShowInventoryUI()
    {
        inventory.HideInventoryUI();
    }

    public void HideInventoryUI()
    {
        inventory.HideInventoryUI();
        itemInfoWindow.HideInfoUI();
    }
    #endregion Inventory

    #region Interaction UI
    public void ShowInteractionUI(Transform targetTransform)
    {
        MoveInteractionUI(targetTransform);
        interactionUIObj.SetActive(true);
    }

    public void HideInteractionUI()
    {
        interactionUIObj.SetActive(false);
    }

    public void MoveInteractionUI(Transform targetTransform)
    {
        interactionUIObj.transform.position = Camera.main.WorldToScreenPoint(targetTransform.position + interactOffset);
    }
    #endregion Interaction UI
}
