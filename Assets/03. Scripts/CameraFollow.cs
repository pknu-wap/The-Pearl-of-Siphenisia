using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform playerTr;

    public float followSpeed = 0.5f;

    Vector3 targetPos;
    public Vector3 offset = new(0f, 0f, 0f);
    float minX = -30f;
    float minY = -89.37f;
    float maxX = 30f;
    float maxY = -0.625f;

    private void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
        targetPos = new(0f, 0f, transform.position.z);
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        
        targetPos.x = Mathf.Clamp(playerTr.position.x, minX, maxX);
        targetPos.y = Mathf.Clamp(playerTr.position.y, minY, maxY);
        transform.position = targetPos + offset;
    }

    void SmoothFollowPlayer(float speed)
    {
        targetPos.x = Mathf.Clamp(playerTr.position.x, minX, maxX);
        targetPos.y = Mathf.Clamp(playerTr.position.y, minY, maxY);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed) + offset;
    }
}
