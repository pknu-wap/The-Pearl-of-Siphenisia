using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyButton : MonoBehaviour
{
    public void BackToLobby()
    {
        SaveManager.Instance.Save();
        GameUIManager.Instance.ResumeGame();
        SceneLoader.Instance.LoadScene("Lobby");
    }
}
