using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌");

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("인벤토리에 추가");
            AddToInventory();
        }
    }


    public void AddToInventory()
    {
        Inventory.Instance.AddItem(itemData);
    }
}
