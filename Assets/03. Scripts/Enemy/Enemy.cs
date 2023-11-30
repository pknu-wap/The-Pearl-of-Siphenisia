using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using static Unity.Collections.AllocatorManager;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Enemy : MonoBehaviour
{
    public UnityEvent StunEvent;
    public Transform playerTransform;
    public Rigidbody2D enemyRig2d;
    public SpriteRenderer spriteRenderer;
    public float enemySpeed = 3f;
    public float rotateSpeed = 10f;
    public float enemyDashPower = 2f;
    public float enemyDashTime = 1f;
    public float enemyDashCooltime = 3f;
    public float stunTime = 3f;
    public bool isFacing = false;
    public bool isDashing = false;
    public bool isStunned = false;

    // Start is called before the first frame update
    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        enemyRig2d = GetComponent<Rigidbody2D>();
        StunEvent.AddListener(Stun);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isDashing = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isStunned)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Toggle();
                if (!isDashing)
                {
                    FollowPlayer();
                    FlipYSprite();
                    Move();
                }
                else
                {
/*                    if (!isFacing)
                    {
                        FollowPlayer();
                        FlipYSprite();
                        isFacing = true;
                    }*/
                    Dash();
                }
                Debug.Log(gameObject);
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        isDashing = false;
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

    private void FlipYSprite()
    {
        if (transform.rotation.z > 0) { spriteRenderer.flipY = true; }
        else { spriteRenderer.flipY = false; }
    }

    public void Dash()
    {
        //float dashX = 0, dashY = 0;
        //if (!setCoord)
        //{
        //    dashX = (playerTransform.position.x - transform.position.x) / 10;
        //    dashY = (playerTransform.position.y - transform.position.y) / 10;
        //    setCoord = true;
        //}
        //transform.Translate(new Vector2(dashX, dashY) * enemyDashPower * enemySpeed * Time.deltaTime);
        transform.Translate(enemyDashPower * enemySpeed * Time.deltaTime * Vector2.down, Space.Self);
    }

    public void Toggle()
    {
        if (isDashing) { StartCoroutine(DashMode()); }
        else { StartCoroutine(NormalMode()); }
    }

    private IEnumerator NormalMode()
    {
        isDashing = false;
        yield return new WaitForSecondsRealtime(enemyDashCooltime);
        isDashing = true;
    }

    private IEnumerator DashMode()
    {
        isDashing = true;
        yield return new WaitForSecondsRealtime(enemyDashTime);
        isDashing = false;
        isFacing = false;
    }
}