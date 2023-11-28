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

    public void SetVolume()
    {
        GameSetter.Instance.SetVolume();
    }
}
