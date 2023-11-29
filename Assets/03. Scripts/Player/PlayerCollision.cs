using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    public PlayerData playerData;

    public UnityEvent onGameOver;
    public UnityEvent onPlayerDamaged;

    public int health = 3;
    public bool isArmored;

    public LampItem currentLamp;
    public ArmorItem currentArmor;

    public bool isAttacked = false;

    public Image[] hp = new Image[3];

    public SpriteRenderer spriteRenderer;

    public Color damagedColor = new Color(150, 150, 150);

    public void Start()
    {
        //health = playerData.health;
        isArmored = playerData.armor;
        spriteRenderer = GetComponent<SpriteRenderer>();

        Transform hpBar = GameObject.Find("HP Bar").transform;
        
        for (int i = 0; i < hp.Length; i++)
        {
            hp[i] = hpBar.GetChild(i).GetComponent<Image>();
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
