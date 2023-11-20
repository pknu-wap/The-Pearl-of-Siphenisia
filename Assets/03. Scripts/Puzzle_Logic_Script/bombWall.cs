using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 벽의 부속들이 사용할 함수, event를 공부 후 로직 수정 에정
public class bombWall : MonoBehaviour
{
    Rigidbody2D rb;
    Color color;
    Renderer wallRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        color = GetComponent<bombWall>().color;
        wallRenderer = GetComponent<Renderer>();
        wallBreakDown();
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
        int tmp = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(0,0,tmp);
        rb.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(wallFadeOut());
    }
}
