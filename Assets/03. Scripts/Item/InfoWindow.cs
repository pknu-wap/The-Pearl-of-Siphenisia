using TMPro;


public class InfoWindow : Singleton<InfoWindow>
{
    public TextMeshProUGUI nameText;
    private TextMeshProUGUI descriptionText;

    private void Awake()
    {
        InfoWindow.instance = this;

        AssignObjects();
        HideInfoUI();
    }

    void AssignObjects()
    {
        nameText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        descriptionText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 아이템의 정보로 갱신
    /// </summary>
    /// <param name="item"></param>
    public void UpdateInfo(ItemData item)
    {
        if(item == null)
        {
            return;
        }

        nameText.text = item.itemName;
        descriptionText.text = item.description;
    }

    /// <summary>
    /// 상세정보창 표시
    /// </summary>
    public void FloatInfoUI()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 상세정보창 숨김
    /// </summary>
    public void HideInfoUI()
    {
        gameObject.SetActive(false);
    }
}
