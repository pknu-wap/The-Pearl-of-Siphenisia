using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushableTile : MonoBehaviour
{
    Rigidbody2D rb2d;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.rigidbody.mass >= 0.15f)
        {
            Debug.Log("질량이 10 이상이므로 밀기 가능");
            rb2d.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            Debug.Log("질량이 10 이하이므로 밀기 불가");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("플레이어가 pushable tile 에서 멀어짐");
        rb2d.bodyType = RigidbodyType2D.Static;
    }
}
