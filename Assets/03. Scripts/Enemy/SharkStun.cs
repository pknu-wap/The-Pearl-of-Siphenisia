using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkStun : MonoBehaviour
{
    public Enemy Stunned;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            Stunned.StunEvent.Invoke();
        }
    }
}
