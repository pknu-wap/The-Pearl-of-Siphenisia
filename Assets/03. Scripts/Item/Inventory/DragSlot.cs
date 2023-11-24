using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    Image icon;
    public Item dragItem;

    private void Start()
    {
        icon = GetComponent<Image>();

        HideImage();
    }

    /// <summary>
    /// 드래그 슬롯의 아이템(현재 선택된 아이템)을 item으로 변경한다.
    /// </summary>
    /// <param name="item"></param>
    public void SetItem(Item item)
    {
        dragItem = item;
        
        if(item != null)
        {
            icon.sprite = item.itemData.sprite;
        }
    }

    /// <summary>
    /// 드래그 슬롯의 아이템을 비운다.
    /// </summary>
    public void ClearItem()
    {
        dragItem = null;
    }

    /// <summary>
    /// 드래그 슬롯을 보이게 한다.
    /// </summary>
    public void ShowImage()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 드래그 슬롯을 숨긴다.
    /// </summary>
    public void HideImage()
    {
        gameObject.SetActive(false);
    }
}
