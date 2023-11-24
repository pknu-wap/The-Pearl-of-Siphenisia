using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    public Item item;
    private Inventory inventory;
    private GameUIManager gameUIManager;

    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        gameUIManager = GameObject.Find("Game UI Manager").GetComponent<GameUIManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어가 범위 내에 들어오면 자신의 위에 인터렉션 버튼 UI를 띄운다.
            gameUIManager.ShowInteractionUI(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어가 감지 범위에서 벗어나면 자신 위의 인터렉션 버튼 UI를 제거한다.
            gameUIManager.HideInteractionUI();
        }
    }

    /// <summary>
    /// 아이템을 획득한다.
    /// </summary>
    public void GetItem()
    {
        AddToInventory();
        gameUIManager.HideInteractionUI();
        DestroyItem();
    }

    /// <summary>
    /// 자신을 인벤토리에 추가한다.
    /// </summary>
    public void AddToInventory()
    {
        inventory.AddItem(item);
    }

    /// <summary>
    /// 아이템 획득 후 자기 자신을 삭제하는 함수
    /// 우선 비활성화로 구현해두었다.
    /// </summary>
    public void DestroyItem()
    {
        gameObject.SetActive(false);
    }
}
