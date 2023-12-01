using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField]
    private string loadSceneName;
    float fadeTime = 2f;
    private GameObject fadeObject;
    private GameObject loadingUI;
    private Image progressBar;
    
    [SerializeField]
    private Image fadeBG;

    protected override void Awake()
    {
        base.Awake();

        AssignObjects();
        fadeObject.SetActive(false);
        loadingUI.SetActive(false);
    }

    void AssignObjects()
    {
        fadeObject = transform.GetChild(0).gameObject;
        fadeBG = fadeObject.GetComponent<Image>();
        loadingUI = transform.GetChild(1).gameObject;
        progressBar = loadingUI.transform.GetChild(1).GetComponent<Image>();
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
        progressBar.fillAmount = 0f;

        while (!operation.isDone)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
            progressBar.fillAmount = operation.progress;

            if(timer < 1f)
            {
                continue;
            }

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
        loadingUI.SetActive(false);

        fadeBG.color = new(0, 0, 0, 1);

        var tween = fadeBG.DOFade(0.0f, seconds);
        yield return tween.WaitForCompletion();

        fadeObject.SetActive(false);
    }

    public IEnumerator FadeOut(float seconds)
    {
        fadeObject.SetActive(true);

        fadeBG.color = new(0, 0, 0, 0);

        var tween = fadeBG.DOFade(1.0f, seconds);
        yield return tween.WaitForCompletion();

        loadingUI.SetActive(true);
    }
}
