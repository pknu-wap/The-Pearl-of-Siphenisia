using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetter : MonoBehaviour
{
    [Header("옵션")]
    [SerializeField] private int targetFPS = 60;
    [SerializeField] private float soundVolume = 1;
    private Slider volumeSlider;

    Dictionary<int, int> frameDict;
    private void Awake()
    {
        AssignObjects();

        // TODO: SaveManager로부터 저장된 설정값 받아오기
        SetDictionaries();
        SetDefaultValues();
    }

    private void AssignObjects()
    {
        settingPanel = GameObject.Find("Setting Panel");
        volumeSlider = GameObject.Find("Sound Slider").GetComponent <Slider>();
    }

    #region 값 설정
    void SetDictionaries()
    {
        frameDict = new Dictionary<int, int> {
            { 0, 30 },
            { 1, 60 },
            { 2, 120 },
        };
    }

    private void SetDefaultValues()
    {

        Application.targetFrameRate = targetFPS;
    }

    public void SetVolume()
    {
        soundVolume = volumeSlider.value;
    }

    public void SetFrameRate(int value)
    {
        targetFPS = frameDict[value];
        Application.targetFrameRate = targetFPS;
    }
    #endregion 값 설정

    #region UI
    private GameObject settingPanel;

    private void ShowSettingPanel()
    {
        settingPanel.SetActive(true);
    }

    private void HideSettingPanel()
    {
        settingPanel.SetActive(false);
    }
    #endregion UI
}
