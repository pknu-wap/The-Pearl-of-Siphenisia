using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    Item currentItem;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void HandItem(Item item)
    {
        currentItem = item;
        spriteRenderer.sprite = item.itemData.sprite;
    }

    public void DrawAimLine()
    {

    }
}
