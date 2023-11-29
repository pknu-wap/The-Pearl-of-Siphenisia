using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    Transform playerTransform;
    public Rigidbody2D enemyRig2d;
    public SpriteRenderer spriteRenderer;
    public float enemySpeed = 3f;
    public float rotateSpeed = 10f;
    public float enemyDashPower = 2f;
    public float enemyDashTime = 1f;
    public float enemyDashCooltime = 3f;
    public float minimumDistance = 5f;
    public bool isFacing = false;
    public bool isDashing = false;

    public GameObject Enemy;

    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        enemyRig2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FollowPlayer();
            FlipYSprite();
            NearToPlayer();
            if (Vector2.Distance(transform.position, playerTransform.position) > minimumDistance + 1)
            {
                shoot();
            }
        }
    }

    private void NearToPlayer()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) > minimumDistance)
        {
            transform.Translate(enemySpeed * Time.deltaTime * Vector2.down, Space.Self);
        }
    }

    public void FlipYSprite()
    {
        if (transform.rotation.z > 0) { spriteRenderer.flipY = true; }
        else { spriteRenderer.flipY = false; }
    }

    public void FollowPlayer()
    {
        // https://unitybeginner.tistory.com/50 에서 가져옴
        Vector2 direction = new(
            transform.position.x - playerTransform.position.x,
            transform.position.y - playerTransform.position.y
        );

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }

    public void shoot()
    {
        Debug.Log(1111);
        for (int i = 3; i < 0; i++)
        {
            Debug.Log(i + "초 후 발사");
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(1.0f);
    }
}