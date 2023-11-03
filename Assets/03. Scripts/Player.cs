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
    public bool isMovingLeft = false;
    public bool isMovingRight = false;
    public bool isMovingUp = false;
    public bool isMovingDown = false;



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

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rig2d.AddForce(new Vector2(horizontal * speed, vertical * speed), ForceMode2D.Force);

        isMovingLeft = horizontal < 0;
        isMovingRight = horizontal > 0;
        isMovingUp = vertical > 0;
        isMovingDown = vertical < 0;

        if (animator.GetBool("isWalking"))
        {
            animator.SetBool("isMovingLeft", isMovingLeft);
            animator.SetBool("isMovingRight", isMovingRight);
            animator.SetBool("isMoving", isMovingLeft && isMovingRight);

            if (isMovingRight) { spriteRenderer.flipX = true; }
            else { spriteRenderer.flipX = false; }
        }
        else
        {
            animator.SetBool("isMovingLeft", isMovingLeft);
            animator.SetBool("isMovingRight", isMovingRight);
            animator.SetBool("isMovingUp", isMovingUp);
            animator.SetBool("isMovingDown", isMovingDown);
            animator.SetBool("isMoving", isMovingLeft && isMovingRight && isMovingUp && isMovingDown);

            if (isMovingRight) { spriteRenderer.flipX = true; }
            else { spriteRenderer.flipX = false; }

            if (!isMovingLeft && !isMovingRight && isMovingDown) { spriteRenderer.flipY = true; }
            else { spriteRenderer.flipY = false; }
        }
    }
}
