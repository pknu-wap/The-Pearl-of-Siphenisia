using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item itemInfo;
    Image icon;

    public GameObject infoPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    private void Awake()
    {
        AssignObjects();
        UpdateSlot(itemInfo);
        infoPanel.SetActive(false);
    }

    void AssignObjects()
    {
        // 자식 오브젝트 Slot Item의 이미지
        icon = transform.GetChild(0).GetComponent<Image>();
        // Info 오브젝트
        infoPanel = transform.GetChild(1).gameObject;
        nameText = infoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        descriptionText = infoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void UpdateSlot(Item item)
    {
        if(item == null)
        {
            return;
        }

        itemInfo = item;
        icon.sprite = item.Icon;
        nameText.text = item.Name;
        descriptionText.text = item.Description;
    }

    public void FloatInfo()
    {
        infoPanel.SetActive(true);
    }
}
