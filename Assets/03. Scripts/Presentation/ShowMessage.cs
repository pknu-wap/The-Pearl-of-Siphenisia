using UnityEngine;

public class ShowMessage : MonoBehaviour
{
    public GameObject message;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            message.SetActive(false);
        }
    }
}
