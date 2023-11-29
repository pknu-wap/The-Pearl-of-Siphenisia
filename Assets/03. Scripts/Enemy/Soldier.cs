using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Soldier : MonoBehaviour
{
    public UnityEvent StunEvent;
    public PlayerCollision player;
    public GameObject spear;
    public Transform playerTransform;
    public Rigidbody2D enemyRig2d;
    public SpriteRenderer spriteRenderer;
    public GameObject Player;
    public Transform hand;
    public float enemySpeed = 3f;
    public float rotateSpeed = 10f;
    public float enemyDashPower = 2f;
    public float enemyDashTime = 1f;
    public float enemyDashCooltime = 3f;
    public float minimumDistance = 5f;
    public bool isFacing = false;
    public bool isDashing = false;
    public bool isShooting = false;
    public bool isStunned = false;
    public float stunTime = 3f;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCollision>();
        playerTransform = player.transform;
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        enemyRig2d = GetComponent<Rigidbody2D>();
        spear = GameObject.Find("Spear");
        spear.SetActive(false);
        hand = transform.GetChild(1).transform;
        StunEvent.AddListener(Stun);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isStunned)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                FollowPlayer();
                FlipYSprite();
                NearToPlayer();
                if (Vector2.Distance(transform.position, playerTransform.position) < minimumDistance + 1)
                {
                    //Debug.Log("shooting started");
                    if (!isShooting) { isShooting = true; Shoot(); }
                }
            }
        }
    }

    public void Stun()
    {
        StartCoroutine(Stunned());
    }

    private IEnumerator Stunned()
    {
        isStunned = true;
        yield return new WaitForSecondsRealtime(stunTime);
        isStunned = false;
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

    public void Shoot()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        for (int i = 3; i > 0; i--)
        {
            Debug.Log(i + "초 후 발사");
            yield return new WaitForSecondsRealtime(1.0f);
        }
        Debug.Log("발사");
        spear.transform.position = hand.position;
        spear.SetActive(true);
        
        yield return new WaitForSecondsRealtime(1.0f);

        isShooting = false;
    }
}