using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
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
        HideGameOverUI();
        HideAlertMessage();
        HideGameClearUI();
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        AssignObjects();

        HideInventoryUI();
        HideInteractionUI();
        HidePausePanelUI();
        HideGameOverUI();
        HideAlertMessage();
        HideGameClearUI();
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
            interactionText = interactionUIObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        catch { }
        try
        {
            itemInfoWindow = GameObject.Find("Item Info Window").GetComponent<ItemInfoWindow>();
        }
        catch { }
        try
        {
            pausePanel = GameObject.Find("Pause Panel");
        }
        catch { }
        try
        {
            gameOverPanel = GameObject.Find("GameOver Panel");
        }
        catch { }
        try
        {
            alertMessagePanel = GameObject.Find("Alert Panel");
            alertMessage = alertMessagePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        catch { }
        try
        {
            gameClearPanel = GameObject.Find("Game Clear Panel");
        }
        catch { }

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
    public void SetInventoryObject(Inventory inventory)
    {
        this.inventory = inventory;
    }
    #endregion Inventory

    #region Interaction UI
    [Header("상호 작용")]
    [SerializeField] private GameObject interactionUIObj;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private Vector3 interactOffset = new(0f, 1f, 0f);

    public void ShowInteractionUI(Transform targetTransform, string key)
    {
        if(interactionUIObj == null)
        {
            return;
        }

        interactionText.text = key;
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

    private void MoveInteractionUI(Transform targetTransform)
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
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private bool isPaused = false;

    public void TogglePauseState()
    {
        if (pausePanel == null)
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
        if (pausePanel == null)
        {
            return;
        }

        Time.timeScale = 0f;
        ShowPausePanelUI();
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pausePanel == null)
        {
            return;
        }

        Time.timeScale = 1f;
        HidePausePanelUI();
        isPaused = false;
    }

    public void ShowPausePanelUI()
    {
        if (pausePanel == null)
        {
            return;
        }

        pausePanel.SetActive(true);
    }

    public void HidePausePanelUI()
    {
        if (pausePanel == null)
        {
            return;
        }

        pausePanel.SetActive(false);
    }
    #endregion 일시정지

    #region 게임오버
    [Header("게임 오버")]
    [SerializeField] private GameObject gameOverPanel;
    public void ShowGameOverUI()
    {
        if (gameOverPanel == null)
        {
            return;
        }

        gameOverPanel.SetActive(true);
    }

    public void HideGameOverUI()
    {
        if (gameOverPanel == null)
        {
            return;
        }

        gameOverPanel.SetActive(false);
    }
    #endregion 게임오버

    #region 게임 클리어
    [Header("게임 클리어")]
    [SerializeField] private GameObject gameClearPanel;

    public void ShowGameClearUI()
    {
        if (gameClearPanel == null)
        {
            return;
        }

        gameClearPanel.SetActive(true);
    }

    public void HideGameClearUI()
    {
        if (gameClearPanel == null)
        {
            return;
        }

        gameClearPanel.SetActive(false);
    }
    #endregion 게임 클리어

    #region 알림 메시지
    [Header("알림 메시지")]
    [SerializeField] private GameObject alertMessagePanel;
    [SerializeField] private TextMeshProUGUI alertMessage;

    [SerializeField] private Coroutine alertCoroutine;

    public void AlertMessage(string message)
    {
        if(alertCoroutine != null)
        {
            StopCoroutine(alertCoroutine);
        }

        alertCoroutine = StartCoroutine(AlertMessageCoroutine(message));
    }

    public IEnumerator AlertMessageCoroutine(string message)
    {
        alertMessage.text = message;
        ShowAlertMessage();

        yield return new WaitForSeconds(3.0f);

        HideAlertMessage();
    }

    public void ShowAlertMessage()
    {
        if (alertMessage == null)
        {
            return;
        }

        alertMessagePanel.SetActive(true);
    }

    public void HideAlertMessage()
    {
        if (alertMessage == null)
        {
            return;
        }

        alertMessagePanel.SetActive(false);
    }
    #endregion 게임오버
}
