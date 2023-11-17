using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flypaper : MonoBehaviour
{
    float originalSpeed;

    private void Start()
    {
        originalSpeed = GameObject.Find("Player").GetComponent<Player>().speed;
    }
    
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Player>().speed = 0.7f;
            // 플레이어 스크립트에서 끈끈이가 붙었을때의 함수 발동
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Player>().speed = originalSpeed;
            // 플레이어 스크립트에서 끈끈이가 해제되었을때의 함수 발동
        }
    }
}
