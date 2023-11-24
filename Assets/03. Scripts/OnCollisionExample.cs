using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionExample : MonoBehaviour
{
    public ObjTest obj;
    public Player player;
    public UnityEvent gameOver;

    int health;
    int armor;

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
                playerDamaged();
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

    public void playerDamaged()
    {
        if (health == 0 && armor == 0)
        {
            Debug.Log("게임 오버");
        }
        else if (armor == 0)
        {
            Debug.Log("플레이어 체력 1 감소");
            health--;
            print(health);
        }
        else
        {
            Debug.Log("플레이어 아머 1 감소");
            armor--;
            print(armor);
        }
    }
}
