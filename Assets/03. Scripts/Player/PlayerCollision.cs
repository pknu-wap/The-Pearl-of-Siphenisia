using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    public PlayerData playerData;

    public UnityEvent onGameOver;
    public UnityEvent onPlayerDamaged;

    int health;
    public bool isArmored;

    public LampItem currentLamp;
    public ArmorItem currentArmor;

    public bool isAttacked = false;

    public void Start()
    {
        health = playerData.health;
        isArmored = playerData.armor;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isAttacked)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                PlayerDamaged();
                StartCoroutine(SetInvincible());
            }
        }
    }

    private IEnumerator SetInvincible()
    {
        isAttacked = true;
        yield return new WaitForSecondsRealtime(1.0f);
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
        }

        if (health == 0)
        {
            Debug.Log("게임 오버");
            onGameOver.Invoke();
        }
    }
}
