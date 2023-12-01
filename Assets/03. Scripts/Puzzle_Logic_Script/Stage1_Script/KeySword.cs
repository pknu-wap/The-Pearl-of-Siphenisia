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
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void DestroyKeySword()
    {
        gameManager.Count();
        Destroy(gameObject);
    }
}
