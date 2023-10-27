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
        int thishealth = obj.health;
        int thisarmor = obj.armor;
        int health = 0;
        int armor = 0;

        for (int i = 0; i < thishealth; i++)
        {
            health++;
        }

        for (int i = 0; i < thisarmor; i++)
        {
            armor++;
        }

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
