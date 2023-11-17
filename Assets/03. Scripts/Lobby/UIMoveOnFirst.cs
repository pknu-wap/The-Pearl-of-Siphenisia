using System.Collections;
using UnityEngine;
using DG.Tweening;

public class UIMoveOnFirst : MonoBehaviour
{
    [SerializeField]
    private Vector2 firstPosition;
    [SerializeField]
    private Vector2 targetPosition;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float startTime;

    private RectTransform rect;

    private void Start()
    {
        StartCoroutine(MoveWithStartTime(startTime));
    }

    IEnumerator MoveWithStartTime(float time)
    {
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = firstPosition;

        yield return new WaitForSeconds(time);

        rect.DOAnchorPos(targetPosition, duration)
            .SetEase(Ease.OutCubic);
    }
}
