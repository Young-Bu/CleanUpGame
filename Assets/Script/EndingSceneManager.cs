using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingSceneManager : MonoBehaviour
{
    public Text currentTimeText;
    public Text bestTimeText;

    void Start()
    {
        float currentTime = PlayerPrefs.GetFloat("CurrentTime", 0f);
        float bestTime = PlayerPrefs.GetFloat("BestTime", 0f);

        currentTimeText.text = $"현재 기록: {FormatTime(currentTime)}";
        bestTimeText.text = $"최단 기록: {FormatTime(bestTime)}";
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes:00}:{seconds:00}";
    }

    public void OnRetryButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }
}