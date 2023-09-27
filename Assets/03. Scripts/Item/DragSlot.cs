using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : Singleton<DragSlot>
{
    Image icon;
    public ItemData dragItem;

    private void Start()
    {
        icon = GetComponent<Image>();

        // 싱글톤 instance 할당(비활성화로 인한 검색 불가)
        instance = this;

        HideImage();
    }

    /// <summary>
    /// 드래그 슬롯의 아이템(현재 선택된 아이템)을 item으로 변경한다.
    /// </summary>
    /// <param name="item"></param>
    public void SetItem(ItemData item)
    {
        dragItem = item;
        icon.sprite = item.sprite;
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
