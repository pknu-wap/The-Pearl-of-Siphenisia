using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    Transform playerTr;
    Rigidbody2D rigid;

    Vector3 offset = new Vector3(0f, -0.5f, 0f);

    private void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(playerTr.position.x, playerTr.position.y, -10) + offset;
    }
}
