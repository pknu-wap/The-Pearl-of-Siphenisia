using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    public AudioMixer mixer;

    [Range(-80, 0)]
    public float master = 0;

    [Range(-80, 0)]
    public float bgm = 0;

    [Range(-80, 0)]
    public float sfx = 0;

    public int wrapNumber = 40 / 10;

    public void MixerControl()
    {
        mixer.SetFloat(nameof(master), master);
        mixer.SetFloat(nameof(bgm), bgm);
        mixer.SetFloat(nameof(sfx), sfx);
    }

    public void SetMasterVolume(float value)
    {
        GameSetter.instance.SetMasterVolume(value);
        master = wrapNumber * value;
        MixerControl();
    }

    public void SetBGMVolume(float value)
    {
        GameSetter.instance.SetBGMVolume(value);
        bgm = wrapNumber * value;
        MixerControl();
    }

    public void SetSFXVolume(float value)
    {
        GameSetter.instance.SetSFXVolume(value);
        sfx = wrapNumber * value;
        MixerControl();
    }
}
