using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager instance;

    public void clearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}