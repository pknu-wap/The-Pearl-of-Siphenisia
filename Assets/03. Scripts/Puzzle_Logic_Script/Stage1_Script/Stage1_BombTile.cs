using System.Collections;
using UnityEngine;

public class Stage1_BombTile : MonoBehaviour
{
    Color color;
    Renderer wallRenderer;

    void Start()
    {
        color = GetComponent<Stage1_BombTile>().color;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            StartCoroutine(wallFadeOut());
        }
    }
}
