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
    }

    public void Win()
    {
        titles[1].SetActive(true);
        GetAnalytics();
    }

    void GetAnalytics()
    {
        titles[2].SetActive(true);
        float gameTime = GameManager.instance.gameTime;
        int minute = Mathf.FloorToInt(gameTime / 60);
        int second = Mathf.FloorToInt(gameTime % 60);
        analytics[0].GetComponent<Text>().text = string.Format("Survive for {0:D2}:{1:D2}", minute, second);

        analytics[1].GetComponent<Text>().text = string.Format("Kill {0:F0}", GameManager.instance.kill);
    }
}