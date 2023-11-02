using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class buttonEffect : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("버튼에 플레이어가 닿음");
            GameObject.FindWithTag("bridge").GetComponent<bridge>().falldown();
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("버튼에서 플레이어가 멀어짐");
        }
    }
}