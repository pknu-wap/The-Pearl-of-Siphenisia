using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetter : Singleton<GameSetter>
{
    #region 변수
    [Header("그래픽")]
    [SerializeField] private int targetFPS = 60;
    [SerializeField] private int vSyncCount = 0;

    [Header("소리")]
    [SerializeField] private float masterValue = 1;
    [SerializeField] private float bgmValue = 1;
    [SerializeField] private float sfxValue = 1;

    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    Dictionary<int, int> frameDict;
    #endregion 변수

    #region 초기 설정
    protected override void Awake()
    {
        base.Awake();

        AssignObjects();

    }

    private void Start()
    {
        SetDictionaries();

        SceneManager.activeSceneChanged += OnSceneChanged;
        LoadSetting();
        SetAllValues();
        AddEvents();
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        AssignObjects();
        //SetDictionaries();
        //LoadSetting();
        SetAllValues();
    }

    private void AssignObjects()
    {
        masterSlider = GameObject.Find("Master Volume Slider").GetComponent<Slider>();
        bgmSlider = GameObject.Find("BGM Volume Slider").GetComponent<Slider>();
        sfxSlider = GameObject.Find("SFX Volume Slider").GetComponent<Slider>();
    }

    private void AddEvents()
    {
        SaveManager.Instance.SaveAll.AddListener(SaveSetting);
    }
    #endregion 초기 설정

    #region 값 설정
    void SetDictionaries()
    {
        frameDict = new Dictionary<int, int> {
            { 0, 30 },
            { 1, 60 },
            { 2, 120 },
        };
    }

    private void SetAllValues()
    {
        Application.targetFrameRate = targetFPS;
        QualitySettings.vSyncCount = vSyncCount;
        masterSlider.value = masterValue;
        bgmSlider.value = bgmValue;
        sfxSlider.value = sfxValue;
        SaveSetting();
    }

    public void SetFrameRate(int value)
    {
        targetFPS = frameDict[value];
        Application.targetFrameRate = targetFPS;
        SaveSetting();
    }

    public void SetVSyncCount(int value)
    {
        vSyncCount = value;
        QualitySettings.vSyncCount = vSyncCount;
        SaveSetting();
    }

    public void SetMasterVolume(float master)
    {
        masterValue = master;
        SaveSetting();
    }

    public void SetBGMVolume(float bgm)
    {
        bgmValue = bgm;
        SaveSetting();
    }

    public void SetSFXVolume(float sfx)
    {
        sfxValue = sfx;
        SaveSetting();
    }
    #endregion 값 설정

    #region 저장
    public void SaveSetting()
    {
        SaveManager.instance.SaveTargetFPS(targetFPS);
        SaveManager.instance.SaveVSyncCount(vSyncCount);
        SaveManager.instance.SaveMasterValue(masterValue);
        SaveManager.instance.SaveBGMValue(bgmValue);
        SaveManager.instance.SaveSFXValue(sfxValue);
    }

    public void LoadSetting()
    {
        targetFPS = SaveManager.instance.LoadTargetFPS();
        vSyncCount = SaveManager.instance.LoadVSyncCount();
        masterValue = SaveManager.instance.LoadMasterValue();
        bgmValue = SaveManager.instance.LoadBGMValue();
        sfxValue = SaveManager.instance.LoadSFXValue();
    }
    #endregion 저장
}
