using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    Rigidbody2D rig2d;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public UnityEvent gameOver;
    public float speed = 30f;
    public float minimumStop = 0.15f;
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
        //if (Input.GetKeyDown(KeyCode.A)) { isMovingLeft = true; Debug.Log("Left ON"); }
        //if (Input.GetKeyDown(KeyCode.D)) { isMovingRight = true; Debug.Log("Right ON"); }
        //if (Input.GetKeyDown(KeyCode.W)) { isMovingUp = true; Debug.Log("Up ON"); }
        //if (Input.GetKeyDown(KeyCode.S)) { isMovingDown = true; Debug.Log("Down ON"); }

        //if (Input.GetKeyUp(KeyCode.A)) { isMovingLeft = false; Debug.Log("Left OFF"); }
        //if (Input.GetKeyUp(KeyCode.D)) { isMovingRight = false; Debug.Log("Right OFF"); }
        //if (Input.GetKeyUp(KeyCode.W)) { isMovingUp = false; Debug.Log("Up OFF"); }
        //if (Input.GetKeyUp(KeyCode.S)) { isMovingDown = false; Debug.Log("Down OFF"); }

        isMovingLeft = rig2d.velocity.normalized.x < -minimumStop;
        isMovingRight = rig2d.velocity.normalized.x > minimumStop;
        isMovingUp = rig2d.velocity.normalized.y > minimumStop;
        isMovingDown = rig2d.velocity.normalized.y < -minimumStop;

        animator.SetBool("isMovingLeft", isMovingLeft);
        animator.SetBool("isMovingRight", isMovingRight);
        animator.SetBool("isMovingUp", isMovingUp);
        animator.SetBool("isMovingDown", isMovingDown);

        //if (isMovingLeft && !isMovingUp && !isMovingDown) { spriteRenderer.flipX = false; spriteRenderer.flipY = false; }
        //if (isMovingLeft && isMovingUp) { spriteRenderer.flipX = false; spriteRenderer.flipY = false; }
        //if (!isMovingLeft && !isMovingRight && isMovingUp) { spriteRenderer.flipX = false; spriteRenderer.flipY = false; }
        //if (isMovingRight && isMovingUp) { spriteRenderer.flipX = true; spriteRenderer.flipY = true; }
        //if (isMovingRight && !isMovingUp && !isMovingDown) { spriteRenderer.flipX = true; spriteRenderer.flipY = false; }
        //if (isMovingRight && isMovingDown) { spriteRenderer.flipX = true; spriteRenderer.flipY = true; }
        //if (!isMovingLeft && !isMovingRight && isMovingDown) { spriteRenderer.flipX = false; spriteRenderer.flipY = true; }
        //if (isMovingLeft && isMovingDown) { spriteRenderer.flipX = false; spriteRenderer.flipY = false; }

        //if (isMovingRight) { spriteRenderer.flipX = true; }
        //if (isMovingUp) { spriteRenderer.flipY = false; }
        //if (isMovingDown) { spriteRenderer.flipY = true; }

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
}
