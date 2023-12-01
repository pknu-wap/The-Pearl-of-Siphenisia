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

    public int minValue = -40;
    public int wrapNumber = 40 / 10;

    public void MixerControl()
    {
        mixer.SetFloat("master", master);
        mixer.SetFloat(nameof(bgm), bgm);
        mixer.SetFloat(nameof(sfx), sfx);
    }

    public void SetMasterVolume(float value)
    {
        master = wrapNumber * value;
        GameSetter.instance.SetMasterVolume(master);
        MixerControl();
    }

    public void SetBGMVolume(float value)
    {
        bgm = wrapNumber * value;
        GameSetter.instance.SetMasterVolume(bgm);
        MixerControl();
    }

    public void SetSFXVolume(float value)
    {
        sfx = wrapNumber * value;
        GameSetter.instance.SetMasterVolume(sfx);
        MixerControl();
    }
}
