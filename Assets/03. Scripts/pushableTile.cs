using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushableTile : MonoBehaviour
{
    Transform player; // 플레이어의 Transform 컴포넌트
    Transform myTransform; // 오브젝트의 Transform 컴포넌트
    public float moveDistance = 1f;
    public int frameRate = 250;
    public float raycastDistance = .1f;
    bool isMove = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMove == false && collision.gameObject.CompareTag("Player") && collision.rigidbody.mass > 0)
        {
            Vector2 normal = collision.contacts[0].normal;

            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                if (normal.x > 0)
                {
                    Debug.Log("플레이어가 왼쪽에 있습니다.");
                    StartCoroutine(MoveObject(Vector2.right));
                }
                else
                {
                    Debug.Log("플레이어가 오른쪽에 있습니다.");
                    StartCoroutine(MoveObject(Vector2.left));
                }
            }
            else
            {
                if (normal.y > 0)
                {
                    Debug.Log("플레이어가 아래에 있습니다.");
                    StartCoroutine(MoveObject(Vector2.up));
                }
                else
                {
                    Debug.Log("플레이어가 위에 있습니다.");
                    StartCoroutine(MoveObject(Vector2.down));
                }
            }
        }
    }

    private IEnumerator MoveObject(Vector2 direction)
    {
        isMove = true;
        float t = moveDistance / frameRate;
        for (int i = 0; i < frameRate; i++)
        {
            transform.Translate(direction * t);
            yield return null;
        }
        isMove = false;
    }

    /*bool isCollision(Vector2 direction)
    {
        RaycastHit2D hit;

        // Raycast를 사용하여 특정 방향으로 레이를 쏴서 충돌을 감지합니다.
        hit = Physics2D.Raycast(transform.position, direction, raycastDistance);

        // 만약 충돌이 감지되면, 여기에서 원하는 동작을 수행할 수 있습니다.
        if (hit.collider != null) return false;
        return true;
    }*/
}
