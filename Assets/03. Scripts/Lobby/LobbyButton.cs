using UnityEngine;

public class LobbyButton : MonoBehaviour
{
    public void BackToLobby()
    {
        SaveManager.Instance.Save();
        GameUIManager.Instance.ResumeGame();
        GameUIManager.Instance.HideGameClearUI();
        SceneLoader.Instance.LoadScene("Lobby");
    }

    public void ResumeGame()
    {
        GameUIManager.Instance.ResumeGame();
    }
}
