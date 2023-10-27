using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;

    public void Show()
    {
        menu.gameObject.SetActive(true);
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

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ResumeGame()
    {
        menu.gameObject.SetActive(false);
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.FilterBgm(false);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}