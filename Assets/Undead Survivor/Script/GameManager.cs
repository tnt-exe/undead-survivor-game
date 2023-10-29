using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime;

    [Header("# Player info")]
    public int playerId;
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 7, 15, 30, 50, 80, 120, 170, 230, 300 };
    public int reviveTime;

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiGameResult;
    public Menu uiPauseMenu;
    public GameObject enemyCleaner;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Application.targetFrameRate = 60;
        if (Menu.gameMode == 1) reviveTime = 3;
    }

    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;

        maxGameTime = (Menu.gameMode == 0) ? 300 : 90;

        player.gameObject.SetActive(true);

        //default weapon
        uiLevelUp.Select(playerId % 2);
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        uiGameResult.gameObject.SetActive(true);
        uiGameResult.Lose();
        Pause();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameEnd()
    {
        StartCoroutine(GameEndRoutine());
    }

    IEnumerator GameEndRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        uiGameResult.gameObject.SetActive(true);
        uiGameResult.Win();
        Pause();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        if (!isLive)
            return;

        if (isLive && Input.GetKeyDown(KeyCode.Escape))
        {
            uiPauseMenu.Show();
            return;
        }

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameEnd();
        }
    }

    public void GetExp(int expDrop)
    {
        if (!isLive)
            return;

        exp += expDrop;

        int lvCheck = level;

        for (int index = level; index < nextExp.Length; index++)
        {
            if (exp >= nextExp[index])
            {
                level = index + 1;
                exp -= nextExp[index];
            }
            else
            {
                break;
            }
        }

        if (lvCheck != level && Menu.gameMode == 0)
        {
            uiLevelUp.Show();
        }
    }

    public void Pause()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}