using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettingButton : MonoBehaviour
{
    private GameObject settingPanel;

    private void Awake()
    {
        settingPanel = GameObject.Find("Setting Panel");
    }

    public void ShowSettingPanel()
    {
        settingPanel.SetActive(true);
    }
}
