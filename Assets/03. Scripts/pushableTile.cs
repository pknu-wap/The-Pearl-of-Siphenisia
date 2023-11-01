using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushableTile : MonoBehaviour
{
    Transform player; // 플레이어의 Transform 컴포넌트
    Transform myTransform; // 오브젝트의 Transform 컴포넌트

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.rigidbody.mass > 0) 
        {
            Vector2 normal = collision.contacts[0].normal;  

            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                if (normal.x > 0)
                {
                    Debug.Log("플레이어가 왼쪽에 있습니다.");
                    MoveObject(Vector2.right);
                }
                else
                {
                    Debug.Log("플레이어가 오른쪽에 있습니다.");
                    MoveObject(Vector2.left);
                }
            }
            else
            {
                if (normal.y > 0)
                {
                    Debug.Log("플레이어가 아래에 있습니다.");
                    MoveObject(Vector2.up); 
                }
                else
                {
                    Debug.Log("플레이어가 위에 있습니다.");
                    MoveObject(Vector2.down);
                }
            }
        }
    }

    private void MoveObject(Vector2 direction)
    {
        // 오브젝트의 위치를 원하는 방향으로 이동합니다.
        float moveDistance = 0.05f; // 이동 거리 설정
        transform.Translate(direction * moveDistance);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("플레이어가 pushable tile 에서 멀어짐");
    }
}
