using System.Collections;
using UnityEngine;

public class BombItem : HandItem
{
    private LineRenderer lineRenderer;
    private BoxCollider2D bombBody;
    private CircleCollider2D bombRange;

    private bool isHanded = false;
    private bool canAim = true;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public float distance = 10f;

    public bool isCrashed = false;

    public float speed = 10f;

    public override bool ActivateItem()
    {
        isHanded = true;
        return true;
    }

    public override bool DeactivateItem()
    {
        isHanded = false;
        return true;
    }

    protected override void Awake()
    {
        base.Awake();

        lineRenderer = GetComponent<LineRenderer>();
        bombBody = GetComponent<BoxCollider2D>();
        bombRange = GetComponent<CircleCollider2D>();
        bombBody.enabled = false;
        bombRange.enabled = false;

        EraseAimLine();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        if (collision.gameObject.CompareTag("Ground")
            || collision.gameObject.CompareTag("Land")
            || collision.gameObject.CompareTag("Enemy"))
        {
            isCrashed = true;
        }
    }

    private void Update()
    {
        if(isHanded == false)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            canAim = true;
        }

        if(canAim == true)
        {
            AimBomb();

            if(Input.GetMouseButtonDown(1))
            {
                EraseAimLine();
                canAim = false;
            }

            else if(Input.GetMouseButtonDown(0))
            {
                EraseAimLine();
                canAim = false;
                StartCoroutine(ThrowBomb());
            }
        }
    }

    private void AimBomb()
    {
        GetLineTransform();
        DrawAimLine();
    }

    private IEnumerator ThrowBomb()
    {
        transform.SetParent(null);
        bombBody.enabled = true;

        Vector2 direction = endPosition - startPosition;

        while(isCrashed == false && Vector3.Magnitude(transform.position - startPosition) < distance)
        {
            transform.Translate(speed * Time.deltaTime * direction);
            yield return null;
        }

        StartCoroutine(Bomb());
    }

    private IEnumerator Bomb()
    {
        bombRange.enabled = true;

        // 충돌 감지를 위한 1프레임 대기
        yield return null;
        // TODO: 폭발 이펙트
        DestroyItem();
    }

    public void DrawAimLine()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    public void EraseAimLine()
    {
        lineRenderer.positionCount = 0;
    }

    // Start: 손 위치, End: 마우스 커서 위치
    private void GetLineTransform()
    {
        startPosition = transform.position;

        // 마우스 위치를 받아온다.
        endPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        endPosition.z = 0;

        // 조준선의 길이를 고정
        Vector3 direction = endPosition - startPosition;
        direction = direction.normalized * distance;
        endPosition = direction + startPosition;
    }
}
