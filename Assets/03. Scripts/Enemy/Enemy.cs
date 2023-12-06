using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityEvent StunEvent;
    public Transform playerTransform;
    public Rigidbody2D enemyRig2d;
    public SpriteRenderer spriteRenderer;

    public float moveSpeed = 3f;
    public float rotateSpeed = 10f;
    public float dashPower = 2f;
    public float dashTime = 1f;
    public float dashCooltime = 3f;
    public float stunTime = 3f;
    public bool isDashing = false;
    public bool isStunned = false;

    private Coroutine dashCoroutine;

    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        enemyRig2d = GetComponent<Rigidbody2D>();
        StunEvent.AddListener(Stun);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어가 범위 내에 들어오면 노멀 모드 실행
        if (collision.gameObject.CompareTag("Player"))
        {
            dashCoroutine = StartCoroutine(NormalMode());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 스턴 상태일 시 아무 일도 하지 않는다.
        if(isStunned)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            // 대시 중이면 Dash 실행
            if (isDashing)
            {
                Dash();
            }

            // 대시 중이 아니라면 플레이어 추적
            else
            {
                LookAtPlayer();
                Move();
                BlockFlipedSprite();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어가 범위를 벗어나면 코루틴 종료
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(dashCoroutine);
        }
    }

    public void Stun()
    {
        // 노멀 모드나 대시 모드가 실행 중이라면 종료한다.
        if(dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }

        dashCoroutine = StartCoroutine(Stunned());
    }

    private IEnumerator Stunned()
    {
        isStunned = true;
        yield return new WaitForSecondsRealtime(stunTime);
        isStunned = false;
        // 스턴이 끝나면 노멀 모드로 실행
        dashCoroutine = StartCoroutine(NormalMode());
    }

    // 플레이어를 바라본다.
    public void LookAtPlayer()
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

    private void Move()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector2.down, Space.Self);
    }

    // 오브젝트가 뒤집어지는 현상을 방지한다.
    private void BlockFlipedSprite()
    {
        if (transform.rotation.z > 0) { spriteRenderer.flipY = true; }
        else { spriteRenderer.flipY = false; }
    }

    public void Dash()
    {
        transform.Translate(dashPower * moveSpeed * Time.deltaTime * Vector2.down, Space.Self);
    }

    private IEnumerator NormalMode()
    {
        isDashing = false;
        yield return new WaitForSecondsRealtime(dashCooltime);
        isDashing = true;
        // 노멀 모드가 끝나면 대시 모드로 스위칭
        dashCoroutine = StartCoroutine(DashMode());
    }

    private IEnumerator DashMode()
    {
        isDashing = true;
        yield return new WaitForSecondsRealtime(dashTime);
        isDashing = false;
        // 대시 모드가 끝나면 노멀 모드로 스위칭
        dashCoroutine = StartCoroutine(NormalMode());
    }
}