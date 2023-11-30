using UnityEngine;

public class bridge : MonoBehaviour
{
/*    Rigidbody2D rb2d1;
    void Start()
    {
        rb2d1 = GetComponent<Rigidbody2D>();
    }

    public void falldown()
    {
        rb2d1.bodyType = RigidbodyType2D.Dynamic;
    }
*/
    public void DestroyBridge()
    {
        Destroy(gameObject);
    }
}