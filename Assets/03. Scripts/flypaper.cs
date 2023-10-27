using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flypaper : MonoBehaviour
{
    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("끈끈이 발동");
            // 플레이어 스크립트에서 끈끈이가 붙었을때의 함수 발동
        }
    }

    void OnCollisionExit2D (Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("끈끈이 해제");
            // 플레이어 스크립트에서 끈끈이가 해제되었을때의 함수 발동
        }
    }
}
