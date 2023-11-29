using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    public PlayerData obj;
    public Player player;
    public UnityEvent gameOver;

    public int health;
    public int armor;

    public bool isAttacked = false;

    public void Start()
    {
        health = obj.health;
        armor = obj.armor;
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
        if (health == 0 && armor == 0)
        {
            Debug.Log("게임 오버");
        }
        else if (armor == 0)
        {
            Debug.Log("플레이어 체력 1 감소");
            health--;
        }
        else
        {
            Debug.Log("플레이어 아머 1 감소");
            armor--;
        }
    }
}
