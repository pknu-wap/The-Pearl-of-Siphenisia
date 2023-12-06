using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoWindow : MonoBehaviour
{
    private RectTransform rectTransform;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI descriptionText;

    public float clampX, clampY;


    private void Awake()
    {
        AssignObjects();
        AssignValues();
    }

    private void Start()
    {
        HideInfoUI();
    }

    void AssignObjects()
    {
        rectTransform = GetComponent<RectTransform>();
        nameText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        descriptionText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void AssignValues()
    {
        Vector2 resolution = transform.parent.GetComponent<CanvasScaler>().referenceResolution;

        clampX = resolution.x / 2 - rectTransform.rect.width;
        clampY = resolution.y / 2 - rectTransform.rect.height;
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
    /// position으로 위치를 이동한다. (월드 좌표계 기준)
    /// </summary>
    /// <param name="position"></param>
    public void ChangePosition(Vector3 position)
    {
        ClampPositionWithAnchor(position);
        Debug.Log(position);

        rectTransform.anchoredPosition = position;
    }

    /// <summary>
    /// 상세정보창 표시
    /// </summary>
    public void ShowInfoUI()
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

    void ClampPositionWithAnchor(Vector2 position)
    {
        // 좌측 하단
        if(position.x < clampX && position.y < -clampY)
        {
            // 피벗을 좌측 하단으로 설정
            rectTransform.pivot = new Vector2(0, 0);
        }

        // 우측 하단
        else if (position.x > clampX && position.y < -clampY)
        {
            // 피벗을 우측 하단으로 설정
            rectTransform.pivot = new Vector2(1, 0);
        }

        // 우측 상단
        else if (position.x > clampX && position.y > -clampY)
        {
            // 피벗을 우측 상단으로 설정
            rectTransform.pivot = new Vector2(1, 1);
        }

        // 디폴트 피벗은 좌측 상단
        else
        {
            // 피벗을 좌측 상단으로 설정
            rectTransform.pivot = new Vector2(0, 1);
        }

        rectTransform.localPosition = position;
    }
}
