using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class MoveTest : MonoBehaviour
{
    Rigidbody2D rig2d;

    public UnityEvent gameOver;
    public float speed;

    public void Test()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rig2d = GetComponent<Rigidbody2D>();
        
        gameOver.Invoke();
        Test();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += new Vector3(-1 * speed * Time.deltaTime, 0, 0);
            rig2d.AddForce(new Vector2(-1 * speed, 0), ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.S))
        {
            //transform.position += new Vector3(1 * speed * Time.deltaTime, 0, 0);
            rig2d.AddForce(new Vector2(1 * speed, 0), ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.A))
        {
            //transform.position += new Vector3(0, 1 * speed * Time.deltaTime, 0);
            rig2d.AddForce(new Vector2(0, 1 * speed), ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //transform.position += new Vector3(0, -1 * speed * Time.deltaTime, 0);
            rig2d.AddForce(new Vector2(0, -1 * speed), ForceMode2D.Force);
        }
    }
}
