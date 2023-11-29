using UnityEngine;

public class GameUIManager : Singleton<GameUIManager>
{
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
        HidePausePanelUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventoryUI();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseState();
        }
    }

    private void AssignObjects()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        interactionUIObj = GameObject.Find("Interaction Button");
        itemInfoWindow = GameObject.Find("Item Info Window").GetComponent<ItemInfoWindow>();
        PausePanel = GameObject.Find("Pause Panel");
    }
    #endregion 초기 설정

    #region Inventory
    [Header("인벤토리")]
    private Inventory inventory;
    private ItemInfoWindow itemInfoWindow;

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
    [Header("상호 작용")]
    private GameObject interactionUIObj;
    private Vector3 interactOffset = new(0f, 1f, 0f);

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

    #region 일시정지
    [Header("일시 정지")]
    private GameObject PausePanel;
    private bool isPaused = false;

    public void TogglePauseState()
    {
        if(isPaused)
        {
            ResumeGame();
        }

        else
        {
            PauseGame();
        }
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
        ShowPausePanelUI();
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        HidePausePanelUI();
        isPaused = false;
    }

    public void ShowPausePanelUI()
    {
        PausePanel.SetActive(true);
    }

    public void HidePausePanelUI()
    {
        PausePanel.SetActive(false);
    }
    #endregion 일시정지
}
