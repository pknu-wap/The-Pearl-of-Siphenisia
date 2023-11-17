using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

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
        // 벽이 보다 역동적으로 무너지는 로직 추가 예정
        rb.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(wallFadeOut());
    }
}
