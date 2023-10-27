using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager instance;

    public float volume;
    [Header("# BGM")]
    public bool bgmOff;

    [Header("# SFX")]
    public bool sfxOff;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void muteBgm()
    {
        bgmOff = true;
        AudioManager.instance.PlayBgm(!bgmOff);
    }

    public void muteSfx()
    {
        sfxOff = true;
    }

    public void changeVol(float vol)
    {

    }

    public void clearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}