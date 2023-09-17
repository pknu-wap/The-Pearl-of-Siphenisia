using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1 * speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1 * speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 1 * speed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -1 * speed * Time.deltaTime, 0);
        }
    }
}
