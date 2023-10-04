using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : Singleton<LobbyUIManager>
{
    GameObject fadeObject;
    [SerializeField]
    Image fadeBG;

    private void Awake()
    {
        AssignObjects();

        fadeObject.SetActive(false);
    }

    void AssignObjects()
    {
        fadeObject = GameObject.Find("FadeBG");
        fadeBG = fadeObject.GetComponent<Image>();
    }

    public IEnumerator FadeIn(float seconds)
    {
        fadeObject.SetActive(true);

        float timer = 0f;
        Color color = new Color(0, 0, 0, 1);

        while (timer <= seconds)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(1, 0, timer);
            fadeBG.color = color;
        }

        fadeObject.SetActive(false);
    }

    public IEnumerator FadeOut(float seconds)
    {
        fadeObject.SetActive(true);

        float timer = 0f;
        Color color = new Color(0, 0, 0, 0);

        while (timer <= seconds)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(0, 1, timer);
            fadeBG.color = color;
        }
    }
}
