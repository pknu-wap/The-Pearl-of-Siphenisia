using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pushableTile : MonoBehaviour
{
    public float raycastDistance = .1f; // 물체가 있는지 빔을 쏴서 확인할때 빔의 길이
    float moveSpeed = 0.03f;             // (0 < movespeed < 1)
    bool isMove = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMove == false && collision.gameObject.CompareTag("Player") && collision.rigidbody.mass > 0)
        {
            float posx = transform.position.x;
            float posy = transform.position.y;
            Vector2 normal = collision.contacts[0].normal;

            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                if (normal.x > 0)
                {
                    Debug.Log("플레이어가 왼쪽에 있습니다.");
                    if (isCollision(Vector2.right) == false) posx++;
                }
                else
                {
                    Debug.Log("플레이어가 오른쪽에 있습니다.");
                    if (isCollision(Vector2.left) == false) posx--;
                }
            }
            else
            {
                if (normal.y > 0)
                {
                    Debug.Log("플레이어가 아래에 있습니다.");
                    if (isCollision(Vector2.up) == false) posy++;
                }
                else
                {
                    Debug.Log("플레이어가 위에 있습니다.");
                    if (isCollision(Vector2.down) == false) posy--;
                }
            }

            Vector2 targetPosition = new Vector2(posx, posy);
            StartCoroutine(MoveObject(targetPosition));
        }
    }

    private IEnumerator MoveObject(Vector2 targetPosition)
    {
        isMove = true;

        for (int i = 0; i < 100; i++)
        {
            transform.position = Vector2.Lerp(gameObject.transform.position, targetPosition, moveSpeed);
            yield return null;
        }
        resetPosition(targetPosition.x, targetPosition.y);
        isMove = false;
    }

    void resetPosition(float posx, float posy)
    {
        transform.position = new Vector2((int)posx, (int)posy);
    }

    bool isCollision(Vector2 direction) // 수정예정 (왜 안댐;;)
    {
        RaycastHit2D hit;

        // Raycast를 사용하여 특정 방향으로 레이를 쏴서 충돌을 감지합니다.
        hit = Physics2D.Raycast(transform.position, direction, raycastDistance);

        // 만약 충돌이 감지되면, 여기에서 원하는 동작을 수행할 수 있습니다.
        if (hit.collider != null)
        {
            Debug.Log("HIT");
            return true;
        }
        Debug.Log("NOT HIT");
        return false;
    }
}
