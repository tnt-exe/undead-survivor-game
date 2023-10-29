using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public static int gameMode;

    [Header("Revive")]
    public GameObject reviveCount;

    public void Show()
    {
        menu.gameObject.SetActive(true);

        if (reviveCount != null)
        {
            reviveCount.GetComponent<Text>().text = string.Format("{0} left", GameManager.instance.reviveTime);
        }

        GameManager.instance.Pause();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.FilterBgm(true);
    }

    public void Hide()
    {
        menu.gameObject.SetActive(false);
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.FilterBgm(false);
    }

    public void PlayGame(int mode)
    {
        gameMode = mode;
        SceneManager.LoadScene(1);
    }

    public void ResumeGame()
    {
        menu.gameObject.SetActive(false);
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.FilterBgm(false);
    }

    public void Revive(bool isRevive)
    {
        Hide();
        if (isRevive)
        {
            Player.instance.Revive();
            return;
        }
        Player.instance.Dead();
    }

    public void BackToMain()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.FilterBgm(false);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}