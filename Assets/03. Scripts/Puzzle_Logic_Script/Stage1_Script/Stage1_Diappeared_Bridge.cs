using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Diappeared_Bridge : MonoBehaviour
{
    Renderer bridgeRenderer;
    public void DisapearBridge()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color color = bridgeRenderer.material.color;

        while (color.a > 0)
        {
            color.a -= 0.5f * Time.deltaTime;
            bridgeRenderer.material.color = color;
            yield return null;
        }

        Destroy(gameObject);
    }
}