using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public GameObject[] titles;
    public GameObject[] analytics;

    public void Lose()
    {
        titles[0].SetActive(true);
        GetAnalytics();
        Record(false);
    }

    public void Win()
    {
        titles[1].SetActive(true);
        GetAnalytics();
        Record(true);
    }

    public void Record(bool isWin)
    {
        if (!PlayerPrefs.HasKey("MyAnalytics"))
        {
            PlayerPrefs.SetInt("MyAnalytics", 1);
            PlayerPrefs.SetFloat("GameTime", 0);
            PlayerPrefs.SetFloat("LongestSurvive", 0);
            PlayerPrefs.SetInt("KillCount", 0);
            PlayerPrefs.SetInt("SurvivedCount", 0);
            PlayerPrefs.SetInt("DeadCount", 0);
        }

        float gameTime = GameManager.instance.gameTime;

        float recordLongestSurvive = PlayerPrefs.GetFloat("LongestSurvive");
        if (gameTime > recordLongestSurvive)
        {
            PlayerPrefs.SetFloat("LongestSurvive", gameTime);
        }

        float recordGameTime = PlayerPrefs.GetFloat("GameTime");
        PlayerPrefs.SetFloat("GameTime", recordGameTime + gameTime);

        int recordKillCount = PlayerPrefs.GetInt("KillCount");
        int killCount = GameManager.instance.kill;
        PlayerPrefs.SetInt("KillCount", recordKillCount + killCount);

        int recordSurvivedCount = PlayerPrefs.GetInt("SurvivedCount");
        int recordDeadCount = PlayerPrefs.GetInt("DeadCount");

        if (isWin)
        {
            PlayerPrefs.SetInt("SurvivedCount", recordSurvivedCount + 1);
        }
        else
        {
            PlayerPrefs.SetInt("DeadCount", recordDeadCount + 1);
        }
    }

    public void GetRecordAnalytics()
    {
        if (!PlayerPrefs.HasKey("MyAnalytics"))
        {
            analytics[0].GetComponent<Text>().text = "Total Game Time: 00:00";
            analytics[1].GetComponent<Text>().text = "Longest Survive: 00:00";
            analytics[2].GetComponent<Text>().text = "Kill: 00";
            analytics[3].GetComponent<Text>().text = "Survived: 00";
            analytics[4].GetComponent<Text>().text = "Dead: 00";
            return;
        }

        float recordGameTime = PlayerPrefs.GetFloat("GameTime");
        int recordGameMinute = Mathf.FloorToInt(recordGameTime / 60);
        int recordGameSecond = Mathf.FloorToInt(recordGameTime % 60);
        analytics[0].GetComponent<Text>().text = string.Format("Total Game Time: {0:D2}:{1:D2}", recordGameMinute, recordGameSecond);

        float recordLongestSurvive = PlayerPrefs.GetFloat("LongestSurvive");
        int recordLongestMinute = Mathf.FloorToInt(recordLongestSurvive / 60);
        int recordLongestSecond = Mathf.FloorToInt(recordLongestSurvive % 60);
        analytics[1].GetComponent<Text>().text = string.Format("Longest Survive: {0:D2}:{1:D2}", recordLongestMinute, recordLongestSecond);

        int recordKillCount = PlayerPrefs.GetInt("KillCount");
        analytics[2].GetComponent<Text>().text = string.Format("Kill: {0:F0}", recordKillCount);

        int recordSurvivedCount = PlayerPrefs.GetInt("SurvivedCount");
        analytics[3].GetComponent<Text>().text = string.Format("Survived: {0:F0}", recordSurvivedCount);

        int recordDeadCount = PlayerPrefs.GetInt("DeadCount");
        analytics[4].GetComponent<Text>().text = string.Format("Dead: {0:F0}", recordDeadCount);
    }

    void GetAnalytics()
    {
        titles[2].SetActive(true);
        float gameTime = GameManager.instance.gameTime;
        int minute = Mathf.FloorToInt(gameTime / 60);
        int second = Mathf.FloorToInt(gameTime % 60);
        analytics[0].GetComponent<Text>().text = string.Format("Survived for {0:D2}:{1:D2}", minute, second);

        analytics[1].GetComponent<Text>().text = string.Format("Kill {0:F0}", GameManager.instance.kill);
    }
}