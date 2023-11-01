using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField]
    private string loadSceneName;
    float fadeTime = 2f;
    private GameObject fadeObject;
    
    [SerializeField]
    private Image fadeBG;

    private void Awake()
    {
        if (transform.parent != null)
        {
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        AssignObjects();
        fadeObject.SetActive(false);
    }

    void AssignObjects()
    {
        fadeObject = transform.GetChild(0).gameObject;
        fadeBG = fadeObject.GetComponent<Image>();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.sceneLoaded += LoadSceneEnd;
        loadSceneName = sceneName;
        StartCoroutine(Load(sceneName));
    }

    private IEnumerator Load(string sceneName)
    {
        yield return StartCoroutine(FadeOut(fadeTime));

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;

        while (!operation.isDone)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;

            if (operation.progress > 0.4f)
            {
                operation.allowSceneActivation = true;
                yield break;
            }
        }
    }

    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == loadSceneName)
        {
            StartCoroutine(FadeIn(fadeTime));
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
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