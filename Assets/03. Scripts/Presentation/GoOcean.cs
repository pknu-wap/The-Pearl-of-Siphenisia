using UnityEngine;

public class GoOcean : MonoBehaviour
{
    public string sceneName;

    Inventory inventory;

    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inventory.SaveInventory();
            SceneLoader.Instance.LoadScene(sceneName);
        }
    }
}
