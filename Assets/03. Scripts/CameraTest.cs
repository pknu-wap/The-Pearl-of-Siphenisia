using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    Transform playerTr;

    private void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(playerTr.position.x, playerTr.position.y, -10);
    }
}
