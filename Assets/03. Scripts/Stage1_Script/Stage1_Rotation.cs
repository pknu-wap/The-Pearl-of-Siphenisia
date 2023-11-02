using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Rotation : MonoBehaviour
{
    public float targetRotation = 30f; // 목표 회전 각도
    public float totalRotation = 0f;   // 변한 회전 각도의 총합
    public float rotationSpeed = 5f;   // 회전 속도

    private Quaternion initialRotation;
    private Quaternion targetQuaternion;
    private bool isRotating = false;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    private IEnumerator RotateObject()  // 오브젝트 회전 함수
    {
        isRotating = true;  // 회전 시작시 회전중이란 뜻의 true로 전환
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * rotationSpeed;
            float t = elapsedTime / 1f;
            transform.rotation = Quaternion.Slerp(startRotation, targetQuaternion, t);
            yield return null;
        }

        isRotating = false; // 회전이 끝나면 false로 전환
    }

    public void RotateTerrain()
    {
        if (!isRotating)    // 회전중이 아닐때
        {
            totalRotation += targetRotation;    // 총 회전 각도 += 목표 회전 각도
            targetQuaternion = Quaternion.Euler(0, 0, totalRotation);   // 총 회전 각도를 이용하여 목표 각도 설정
            StartCoroutine(RotateObject());     // 오브젝트 회전
        }
    }
}
