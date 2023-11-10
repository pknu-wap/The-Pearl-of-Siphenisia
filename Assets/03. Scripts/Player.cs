using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Rigidbody2D rig2d;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public UnityEvent gameOver;
    
    public bool isSwimming = true;
    public bool isWalking = false;
    public float speed = 30f; 
    public float gravityScale = 2f;
    public bool isMovingLeft = false;
    public bool isMovingRight = false;
    public bool isMovingUp = false;
    public bool isMovingDown = false;

    public Item equipedItem = null;    // 현재 장착 중인 아이템
    public Item currentFocusedItem = null; // 현재 주목 중인 아이템 (근처에 다가간 아이템)
    public Collider2D currentCollision = null;



    // Start is called before the first frame update
    void Start()
    {
        rig2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("isWalking", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isWalking", true);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // 아이템에 닿을 시 해당 아이템을 캐싱해둔다.
        if (collision.gameObject.CompareTag("Item"))
        {
            if(collision == currentCollision)
            {
                // 이미 등록한 콜리젼이라면 리턴
                return;
            }

            // 아이템 등록
            currentFocusedItem = collision.GetComponent<Item>();
            currentCollision = collision;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isWalking", false);
        }

        // 아이템에서 벗어나면 주목 중인 아이템을 비운다.
        if (collision.gameObject.CompareTag("Item"))
        {
            // 등록된 아이템을 벗어난 게 아니라면
            if (collision != currentCollision)
            {
                // 비우지 않는다.
                return;
            }

            currentFocusedItem = null;
        }
    }



    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rig2d.AddForce(new Vector2(horizontal * speed, vertical * speed), ForceMode2D.Force);

        /*isMovingLeft = rig2d.velocity.normalized.x < -0.15f;
        isMovingRight = rig2d.velocity.normalized.x > 0.15f;
        isMovingUp = rig2d.velocity.normalized.y > 0.15f;
        isMovingDown = rig2d.velocity.normalized.y < -0.15f;*/

        isMovingLeft = horizontal < -0.15f;
        isMovingRight = horizontal > 0.15f;
        isMovingUp = vertical > 0.15f;
        isMovingDown = vertical < -0.15f;

        if (animator.GetBool("isWalking"))
        {
            animator.SetBool("isMovingLeft", isMovingLeft);
            animator.SetBool("isMovingRight", isMovingRight);
            animator.SetBool("isMoving", isMovingLeft || isMovingRight);
            rig2d.gravityScale = gravityScale;

            if (isMovingRight) { spriteRenderer.flipX = true; }
            else { spriteRenderer.flipX = false; }
        }
        else
        {
            animator.SetBool("isMovingLeft", isMovingLeft);
            animator.SetBool("isMovingRight", isMovingRight);
            animator.SetBool("isMovingUp", isMovingUp);
            animator.SetBool("isMovingDown", isMovingDown);
            animator.SetBool("isMoving", isMovingLeft || isMovingRight || isMovingUp || isMovingDown);
            rig2d.gravityScale = 0;

            if (isMovingRight) { spriteRenderer.flipX = true; }
            else { spriteRenderer.flipX = false; }

            if (!isMovingLeft && !isMovingRight && isMovingDown) { spriteRenderer.flipY = true; }
            else { spriteRenderer.flipY = false; }
        }
    }

    private void Update()
    {
        // 인터렉션 키 입력 (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetItem();
        }
    }

    /// <summary>
    /// currentFocusedItem을 획득한다.
    /// </summary>
    private void GetItem()
    {
        if (currentFocusedItem == null)
        {
            return;
        }

        // 아이템 획득
        currentFocusedItem.GetItem();
        Debug.Log("E키 입력");
    }
}
