using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Rigidbody2D rig2d;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public UnityEvent gameOver;

    public bool isSwimming = true;
    public float speed;
    public float minimumStop = 1f;
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

        gameOver.Invoke();
    }
    
    void FixedUpdate()
    {
        if (isSwimming) { Swim(); }
        else { }
    }

    void Swim()
    {
        isMovingLeft = rig2d.velocity.normalized.x < -minimumStop;
        isMovingRight = rig2d.velocity.normalized.x > minimumStop;
        isMovingUp = rig2d.velocity.normalized.y > minimumStop;
        isMovingDown = rig2d.velocity.normalized.y < -minimumStop;

        animator.SetBool("isMovingLeft", isMovingLeft);
        animator.SetBool("isMovingRight", isMovingRight);
        animator.SetBool("isMovingUp", isMovingUp);
        animator.SetBool("isMovingDown", isMovingDown);

        if (isMovingRight) { spriteRenderer.flipX = true; }
        else { spriteRenderer.flipX = false; }

        if (!isMovingLeft && !isMovingRight && isMovingDown) { spriteRenderer.flipY = true; }
        else { spriteRenderer.flipY = false; }

        if (Input.GetKey(KeyCode.A))
        {
            //transform.position += new Vector3(-1 * speed * Time.deltaTime, 0, 0);
            rig2d.AddForce(new Vector2(-1 * speed, 0), ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //transform.position += new Vector3(1 * speed * Time.deltaTime, 0, 0);
            rig2d.AddForce(new Vector2(1 * speed, 0), ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += new Vector3(0, 1 * speed * Time.deltaTime, 0);
            rig2d.AddForce(new Vector2(0, 1 * speed), ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.S))
        {
            //transform.position += new Vector3(0, -1 * speed * Time.deltaTime, 0);
            rig2d.AddForce(new Vector2(0, -1 * speed), ForceMode2D.Force);
        }
    }

    void Walk()
    {

    }
}
