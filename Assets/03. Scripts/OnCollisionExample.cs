using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionExample : MonoBehaviour
{
    public ObjTest obj;
    public UnityEvent gameOver;

    int health;
    int armor;

    public void Start()
    {
        health = obj.health;
        armor = obj.armor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerDamaged();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("OnCollisionStay " + collision.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("OnCollisionExit " + collision.gameObject.name);
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
