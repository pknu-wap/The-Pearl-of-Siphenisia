using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform enemyTransform;
    Transform playerTransform;
    public float enemySpeed = 0.02f;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyTransform = GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        followPlayer();
        Debug.Log("Stay");
    }

    public void followPlayer()
    {
        transform.position = Vector3.MoveTowards(enemyTransform.position, playerTransform.position, enemySpeed);
    }
}