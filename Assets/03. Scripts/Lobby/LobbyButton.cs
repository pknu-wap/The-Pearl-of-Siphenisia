using UnityEngine;

public class LobbyButton : MonoBehaviour
{
    public string lobby;

    public void BackToLobby()
    {
        SaveManager.Instance.Save();
        GameUIManager.Instance.ResumeGame();
        GameUIManager.Instance.HideGameClearUI();
        SceneLoader.Instance.LoadScene(lobby);
    }

    public void ResumeGame()
    {
        GameUIManager.Instance.ResumeGame();
    }
}
