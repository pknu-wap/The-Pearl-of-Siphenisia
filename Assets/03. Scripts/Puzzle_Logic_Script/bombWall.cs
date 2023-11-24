using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

// 벽의 부속들이 사용할 함수, event를 공부 후 로직 수정 에정
public class bombWall : MonoBehaviour
{
    Rigidbody2D rb;
    Color color;
    Renderer wallRenderer;
    public UnityEvent onBombed;
    public float power;

    Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        color = GetComponent<bombWall>().color;
        wallRenderer = GetComponent<Renderer>();

        onBombed.AddListener(wallBreakDown);
    }

    // OnCollisionEnter2D 등의 함수를 이용하여 벽이 폭탄 데미지를 받으면 wallBreakDown함수 발동하는 로직 추가 예정

    private IEnumerator wallFadeOut()
    {
        Color color = wallRenderer.material.color;

        while (color.a > 0)
        {
            color.a -= 0.5f * Time.deltaTime;
            wallRenderer.material.color = color;
            yield return null;
        }

        Destroy(gameObject);
    }

    public void wallBreakDown()
    {
        int tmp = Random.Range(-90, 90);
        Quaternion targetRotation = Quaternion.Euler(0,0,tmp);
        StartCoroutine(BreakObject(targetRotation));
    }

    private IEnumerator BreakObject(Quaternion targetRotation)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(direction * power);

        for (int i = 0; i < 100; i++)
        {
            transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, 0.02f);
            yield return null;
        }

        StartCoroutine(wallFadeOut());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            direction = transform.position - collision.transform.position;
            direction.Normalize();
            wallBreakDown();
        }
    }
}
