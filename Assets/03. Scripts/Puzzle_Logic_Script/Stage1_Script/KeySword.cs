using UnityEngine;

public enum Cubic
{
    Ruby,
    Emerald,
    Topaz
}

public class KeySword : MonoBehaviour
{
    public Cubic cubic;

    public void DestroyKeySword()
    {
        Destroy(gameObject);
    }
}
