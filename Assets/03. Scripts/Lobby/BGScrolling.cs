using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BGScrolling : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    // 스크롤에 사용될 BG 개수
    public float BGCount = 2;

    void Update()
    {
        MoveUpObject();
    }

    void MoveUpObject()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        // 화면 밖을 벗어나면 아래로 이동한다.
        if(transform.position.y >= transform.localScale.y)
        {
            transform.position += BGCount * transform.localScale.y * Vector3.down;
        }
    }
}
