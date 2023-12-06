using UnityEngine;

public class buttonEffect : MonoBehaviour
{
    public bridge bridge;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (bridge == null)
        {
            return;
        }

        if (other.gameObject.tag == "Player")
        {
            bridge.DestroyBridge();
        }
    }
}