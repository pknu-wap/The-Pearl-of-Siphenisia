using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    [Header("플레이어 정보")]
    public PlayerData playerData;
    public int health;
    public bool isArmored;

    [Header("이벤트")]
    public UnityEvent onGameOver;
    public UnityEvent onPlayerDamaged;

    [Header("현재 인식된 정보")]
    public LampItem currentLamp;
    public ArmorItem currentArmor;
    [SerializeField] ItemTrigger currentFocusedItem = null;
    public KeySword currentKeySword = null;

    [Header("기타")]
    public bool isAttacked = false;
    private Image[] hp = new Image[3];
    private SpriteRenderer spriteRenderer;
    public Color damagedColor;

    public void Start()
    {
        health = playerData.health;
        isArmored = playerData.armor;

        spriteRenderer = GetComponent<SpriteRenderer>();

        Transform hpBar = GameObject.Find("HP Bar").transform;
        for (int i = 0; i < hp.Length; i++)
        {
            hp[i] = hpBar.GetChild(i).GetComponent<Image>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetItem();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isAttacked)
        {
            PlayerDamaged();
            StartCoroutine(SetInvincible());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            currentFocusedItem = collision.GetComponent<ItemTrigger>();
        }

        if (collision.gameObject.CompareTag("KeySword"))
        {
            currentKeySword = collision.GetComponent<KeySword>();
            GameUIManager.Instance.ShowInteractionUI(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 거리를 충분히 벌릴 것
        if (collision.gameObject.CompareTag("Item"))
        {
            currentFocusedItem = null;
        }

        if (collision.gameObject.CompareTag("KeySword"))
        {
            currentKeySword = null;
            GameUIManager.Instance.HideInteractionUI();
        }
    }

    private IEnumerator SetInvincible()
    {
        isAttacked = true;
        spriteRenderer.color = damagedColor;
        yield return new WaitForSecondsRealtime(0.4f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSecondsRealtime(0.4f);
        spriteRenderer.color = damagedColor;
        yield return new WaitForSecondsRealtime(0.4f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSecondsRealtime(0.4f);
        spriteRenderer.color = damagedColor;
        yield return new WaitForSecondsRealtime(0.4f);
        spriteRenderer.color = Color.white;
        isAttacked = false;
    }

    public void PlayerDamaged()
    {
        if (isArmored == true)
        {
            isArmored = false;
            onPlayerDamaged.Invoke();
            return;
        }

        else
        {
            health--;
            UpdateHPUI();
        }

        if (health == 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// currentFocusedItem을 획득한다.
    /// </summary>
    private void GetItem()
    {
        if (currentFocusedItem == null)
        {
            return;
        }

        currentFocusedItem.GetItem();
        currentFocusedItem = null;
    }

    public void DestroyKeySword()
    {
        currentKeySword.DestroyKeySword();
    }

    void UpdateHPUI()
    {
        for(int i = 0; i < health; i++)
        {
            hp[i].color = Color.white;
        }

        for(int i = health; i < hp.Length; i++)
        {
            hp[i].color = Color.black;
        }
    }
    
    void GameOver()
    {
        Time.timeScale = 0f;
        GameUIManager.Instance.ShowGameOverUI();
    }
}
