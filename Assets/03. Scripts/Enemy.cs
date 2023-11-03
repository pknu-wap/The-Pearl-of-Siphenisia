using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    Transform playerTransform;
    public float enemySpeed = 2f;
    public float rotateSpeed = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        FollowPlayer();
        Move();
    }

    public void FollowPlayer()
    {
        // https://unitybeginner.tistory.com/50 ¿¡¼­ °¡Á®¿È
        Vector2 direction = new(
            transform.position.x - playerTransform.position.x,
            transform.position.y - playerTransform.position.y
        );

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }

    private void Move()
    {
        transform.Translate(enemySpeed * Time.deltaTime * Vector2.down, Space.Self);
    }
}