using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
    public Rigidbody2D rig2d;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public UnityEvent gameOver;
    
    public bool isSwimming = true;
    public bool isWalking = false;
    public float walkingSpeed = 55f;
    public float swimmingSpeed = 30f;
    public float gravityScale = 2f;
    public float jumpPower = 500f;
    public float horizontal;
    public float vertical;
    public bool isMovingLeft = false;
    public bool isMovingRight = false;
    public bool isMovingUp = false;
    public bool isMovingDown = false;
    public bool isJumpAble = false;
    public float movingConstant = (float)(Math.Sqrt(2) / 2);

    public Item equipedItem = null;    // ���� ���� ���� ������
    public ItemTrigger currentFocusedItem = null; // ���� �ָ� ���� ������ (��ó�� �ٰ��� ������)    // inventory changed this
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

        if(collision.gameObject.CompareTag("Item"))
        {
            currentFocusedItem = collision.GetComponent<ItemTrigger>();
        }
    }

    void OnTriggerExit2D(Collider2D collision) { if (collision.gameObject.CompareTag("Ground")) { animator.SetBool("isWalking", false); } }

    void OnCollisionEnter2D(Collision2D collision) { if (collision.gameObject.CompareTag("Land")) { isJumpAble = true; } }

    void OnCollisionExit2D(Collision2D collision) { if (collision.gameObject.CompareTag("Land")) { isJumpAble = false; } }

    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        /*isMovingLeft = rig2d.velocity.normalized.x < -0.15f;
        isMovingRight = rig2d.velocity.normalized.x > 0.15f;
        isMovingUp = rig2d.velocity.normalized.y > 0.15f;
        isMovingDown = rig2d.velocity.normalized.y < -0.15f;*/

        isMovingLeft = horizontal < -0.15f;
        isMovingRight = horizontal > 0.15f;
        isMovingUp = vertical > 0.15f;
        isMovingDown = vertical < -0.15f;

        if (animator.GetBool("isWalking")) { Walk(); }
        else { Swim(); }
    }

    void Walk()
    {
        if (horizontal != 0) { rig2d.AddForce(new Vector2(horizontal * walkingSpeed, 0), ForceMode2D.Force); }
        if (vertical == 1 && isJumpAble) { rig2d.AddForce(new Vector2(0, jumpPower), ForceMode2D.Force); }

        animator.SetBool("isMovingLeft", isMovingLeft);
        animator.SetBool("isMovingRight", isMovingRight);
        animator.SetBool("isMoving", isMovingLeft || isMovingRight);
        rig2d.gravityScale = gravityScale;

        if (isMovingLeft) { spriteRenderer.flipX = false; }
        if (isMovingRight) { spriteRenderer.flipX = true; }
    }

    void Swim()
    {
        if (horizontal != 0 && vertical != 0) { rig2d.AddForce(new Vector2(movingConstant * horizontal * swimmingSpeed, movingConstant * vertical * swimmingSpeed), ForceMode2D.Force); }
        else { rig2d.AddForce(new Vector2(horizontal * swimmingSpeed, vertical * swimmingSpeed), ForceMode2D.Force); }

        animator.SetBool("isMovingLeft", isMovingLeft);
        animator.SetBool("isMovingRight", isMovingRight);
        animator.SetBool("isMovingUp", isMovingUp);
        animator.SetBool("isMovingDown", isMovingDown);
        animator.SetBool("isMoving", isMovingLeft || isMovingRight || isMovingUp || isMovingDown);
        rig2d.gravityScale = 0;

        if (isMovingRight) { spriteRenderer.flipX = true; }
        else { spriteRenderer.flipX = false; }
    }

    private void Update()
    {
        // ���ͷ��� Ű �Է� (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetItem();
        }
    }

    /// <summary>
    /// currentFocusedItem�� ȹ���Ѵ�.
    /// </summary>
    private void GetItem()
    {
        if (currentFocusedItem == null)
        {
            return;
        }

        // ������ ȹ��
        currentFocusedItem.GetItem();
        currentFocusedItem = null;
        Debug.Log("EŰ �Է�");
    }
}
