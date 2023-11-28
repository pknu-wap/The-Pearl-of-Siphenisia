using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Spear : MonoBehaviour
{
    public GameObject player;
    public PlayerCollision playerCollision;
    public Rigidbody2D rig2d;
    public Transform playerTransform;
    public Transform soldierTransform;
    public float lifeTime = 3.0f;
    public float spearSpeed = 0.01f;

    private void OnEnable()
    {
        Fire();
    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerCollision = player.GetComponent<PlayerCollision>();

        rig2d = GetComponent<Rigidbody2D>();
        playerTransform = player.transform;
        Debug.Log(playerTransform);
        Debug.Log(transform);
    }

    public void Fire()
    {
        Vector2 direction = new(
            (playerTransform.position.x - transform.position.x) * spearSpeed,
            (playerTransform.position.y - transform.position.y) * spearSpeed
        );

        rig2d.AddForce(direction, ForceMode2D.Force);

        StartCoroutine(Timer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollision.PlayerDamaged();
            gameObject.SetActive(false);
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(lifeTime);
        gameObject.SetActive(false);
    }
}
