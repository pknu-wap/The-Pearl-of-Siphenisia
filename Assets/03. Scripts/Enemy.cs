using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform enemyTransform;
    Transform playerTransform;
    public float enemySpeed = (float)0.02;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyTransform = GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(enemyTransform.position, playerTransform.position, enemySpeed);
    }
}
