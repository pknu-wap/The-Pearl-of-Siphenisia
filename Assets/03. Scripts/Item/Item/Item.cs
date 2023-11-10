using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    private Inventory inventory;
    public UnityEvent useItemEvent;

    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어와 닿으면 자신의 위에 인터렉션 버튼 UI를 띄운다.
            GameUIManager.Instance.ShowInteractionUI(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어와 닿으면 자신 위의 인터렉션 버튼 UI를 제거한다.
            GameUIManager.Instance.HideInteractionUI();
        }
    }

    public void GetItem()
    {
        AddToInventory();
        DestroyItem();
        Debug.Log("아이템 획득");
    }

    /// <summary>
    /// 자신을 인벤토리에 추가한다.
    /// </summary>
    public void AddToInventory()
    {
        inventory.AddItem(itemData);
    }

    /// <summary>
    /// 아이템 획득 후 자기 자신을 삭제하는 함수
    /// 우선 비활성화로 구현해두었다.
    /// </summary>
    public void DestroyItem()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 아이템을 사용한다.
    /// </summary>
    public void UseItem()
    {
        useItemEvent.Invoke();
    }
}
