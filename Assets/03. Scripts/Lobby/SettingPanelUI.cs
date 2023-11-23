using UnityEngine;

public class SettingPanelUI : MonoBehaviour
{
    #region UI
    private void Start()
    {
        HideSettingPanel();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            HideSettingPanel();
        }
    }

    public void ShowSettingPanel()
    {
        gameObject.SetActive(true);
    }

    public void HideSettingPanel()
    {
        gameObject.SetActive(false);
    }
    #endregion UI
}
