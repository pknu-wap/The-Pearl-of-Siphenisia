using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flypaper : MonoBehaviour
{
    float originalSpeed;
    public float acceleration;

    private void Start()
    {
        originalSpeed = GameObject.Find("Player").GetComponent<Player>().swimmingSpeed;
    }
    
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("끈끈이 발동");
            GameObject.Find("Player").GetComponent<Player>().swimmingSpeed *= acceleration;
            // 플레이어 스크립트에서 끈끈이가 붙었을때의 함수 발동
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("끈끈이 해제");
            GameObject.Find("Player").GetComponent<Player>().swimmingSpeed = originalSpeed;
            // 플레이어 스크립트에서 끈끈이가 해제되었을때의 함수 발동
        }
    }
}
