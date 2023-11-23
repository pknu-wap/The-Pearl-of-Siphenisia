using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetter : MonoBehaviour
{
    [Header("그래픽")]
    [SerializeField] private int targetFPS = 60;
    [SerializeField] private int vSyncCount = 0;

    [Header("소리")]
    [SerializeField] private float soundVolume = 1;
    [SerializeField]  private Slider volumeSlider;
    [SerializeField] AudioSource audioSource;

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
        volumeSlider = GameObject.Find("Volume Slider").GetComponent <Slider>();
        audioSource = GameObject.Find("BGM").GetComponent<AudioSource>();
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
        audioSource.volume = 0;
        QualitySettings.vSyncCount = vSyncCount;
    }

    public void SetFrameRate(int value)
    {
        targetFPS = frameDict[value];
        Application.targetFrameRate = targetFPS;
    }

    public void SetVSyncCount(int value)
    {
        vSyncCount = value;
        QualitySettings.vSyncCount = vSyncCount;
    }

    public void SetVolume()
    {
        soundVolume = (float)volumeSlider.value / volumeSlider.maxValue;
        audioSource.volume = soundVolume;
    }
    #endregion 값 설정
}
