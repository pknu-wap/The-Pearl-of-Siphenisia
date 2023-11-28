using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : Singleton<GameUIManager>
{
    #region 초기 설정

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;

        AssignObjects();

        HideInventoryUI();
        HideInteractionUI();
        HidePausePanelUI();
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        AssignObjects();

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
        try
        {
            inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        }
        catch { }
        try
        {
            interactionUIObj = GameObject.Find("Interaction Button");
        }
        catch { }
        try
        {
            itemInfoWindow = GameObject.Find("Item Info Window").GetComponent<ItemInfoWindow>();
        }
        catch { }
        try
        {
            PausePanel = GameObject.Find("Pause Panel");
        }
        catch { }
    }

    public void SetInventoryObject(Inventory inventory)
    {
        this.inventory = inventory;
    }
    #endregion 초기 설정

    #region Inventory
    [Header("인벤토리")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private ItemInfoWindow itemInfoWindow;

    // 인벤토리 UI가 켜져 있으면 끄고, 꺼져 있으면 켜는 함수
    public void ToggleInventoryUI()
    {
        if (inventory == null)
        {
            return;
        }

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
        if (inventory == null)
        {
            return;
        }

        inventory.HideInventoryUI();
    }

    public void HideInventoryUI()
    {
        if(inventory == null || itemInfoWindow == null)
        {
            return;
        }

        inventory.HideInventoryUI();
        itemInfoWindow.HideInfoUI();
    }
    #endregion Inventory

    #region Interaction UI
    [Header("상호 작용")]
    [SerializeField] private GameObject interactionUIObj;
    [SerializeField] private Vector3 interactOffset = new(0f, 1f, 0f);

    public void ShowInteractionUI(Transform targetTransform)
    {
        if(interactionUIObj == null)
        {
            return;
        }

        MoveInteractionUI(targetTransform);
        interactionUIObj.SetActive(true);
    }

    public void HideInteractionUI()
    {
        if (interactionUIObj == null)
        {
            return;
        }

        interactionUIObj.SetActive(false);
    }

    public void MoveInteractionUI(Transform targetTransform)
    {
        if (interactionUIObj == null)
        {
            return;
        }

        interactionUIObj.transform.position = Camera.main.WorldToScreenPoint(targetTransform.position + interactOffset);
    }
    #endregion Interaction UI

    #region 일시정지
    [Header("일시 정지")]
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private bool isPaused = false;

    public void TogglePauseState()
    {
        if (PausePanel == null)
        {
            return;
        }

        if (isPaused)
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
        if (PausePanel == null)
        {
            return;
        }

        Time.timeScale = 0f;
        ShowPausePanelUI();
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (PausePanel == null)
        {
            return;
        }

        Time.timeScale = 1f;
        HidePausePanelUI();
        isPaused = false;
    }

    public void ShowPausePanelUI()
    {
        if (PausePanel == null)
        {
            return;
        }

        PausePanel.SetActive(true);
    }

    public void HidePausePanelUI()
    {
        if (PausePanel == null)
        {
            return;
        }

        PausePanel.SetActive(false);
    }
    #endregion 일시정지
}
