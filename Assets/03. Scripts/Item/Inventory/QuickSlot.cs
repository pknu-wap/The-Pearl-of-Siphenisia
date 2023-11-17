using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    private Item currentItem;
    private Image itemImage;

    private void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();

        ClearSlot();
    }
    
    // 플레이어로 옮길 예정
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ActivateItem();
        }
    }

    public void ActivateItem()
    {
        if(currentItem == null)
        {
            return;
        }

        currentItem.ActivateItem();
    }

    public void SetItem(Item item)
    {
        currentItem = item;
        itemImage.sprite = item.itemData.sprite;
        itemImage.gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        currentItem = null;
        itemImage.sprite = null;
        itemImage.gameObject.SetActive(false);
    }
}
