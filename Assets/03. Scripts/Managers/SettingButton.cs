using UnityEngine;

public class SettingButton : MonoBehaviour
{
    public void SetFrameRate(int value)
    {
        GameSetter.Instance.SetFrameRate(value);
    }

    public void SetVSyncCount(int value)
    {
        GameSetter.Instance.SetVSyncCount(value);
    }

    public void SetMasterVolume(float value)
    {
        SoundManager.Instance.SetMasterVolume(value);
    }

    public void SetBGMVolume(float value)
    {
        SoundManager.Instance.SetBGMVolume(value);
    }

    public void SetSFXVolume(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
    }
}
