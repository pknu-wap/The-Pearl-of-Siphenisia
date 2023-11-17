using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MoveOnFirst : MonoBehaviour
{
    [SerializeField]
    private Vector2 firstPosition;
    [SerializeField]
    private Vector2 targetPosition;
    [SerializeField]
    private float duration;

    private void Start()
    {
        transform.position = firstPosition;

        transform.DOMove(targetPosition, duration)
            .SetEase(Ease.OutBack);
    }
}
