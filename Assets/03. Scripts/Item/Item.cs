using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    private Inventory inventory;

    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    // 아이템 획득 조건을 임시로 플레이어와 부딪혔을 떄로 설정
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddToInventory();
        }
    }

    public void AddToInventory()
    {
        inventory.AddItem(itemData);
    }
}
