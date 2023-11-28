using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    public void StartGame(string sceneName)
    {
        SceneLoader.Instance.LoadScene(sceneName);
    }
}
