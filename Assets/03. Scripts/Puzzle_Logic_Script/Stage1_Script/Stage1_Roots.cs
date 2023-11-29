using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Roots : MonoBehaviour
{
    Renderer rootsRenderer;
    public void DisapearRoots()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color color = rootsRenderer.material.color;

        while (color.a > 0)
        {
            color.a -= 0.5f * Time.deltaTime;
            rootsRenderer.material.color = color;
            yield return null;
        }

        Destroy(gameObject);
    }
}
