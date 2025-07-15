using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalItemsToClean = 5; // 총 아이템 수
    private int cleanedItemCount = 0;

    public Text progressText;
    public Text timerText;

    private float currentTime = 0f;
    private bool timerRunning = true;

    private HashSet<string> cleanedItems = new HashSet<string>();

    void Awake()
    {
        // 싱글턴
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ResetGameStateIfNeeded();
        UpdateProgressUI();
    }

    void Update()
    {
        if (timerRunning)
        {
            currentTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            ResetGameStateIfNeeded();
        }
    }

    private void ResetGameStateIfNeeded()
    {
        cleanedItemCount = 0;
        currentTime = 0f;
        timerRunning = true;

        cleanedItems.Clear();

        // 메인씬에서 새로 시작할 경우, UI 찾아서 다시 연결
        progressText = GameObject.Find("ProgressText")?.GetComponent<Text>();
        timerText = GameObject.Find("TimerText")?.GetComponent<Text>();

        UpdateProgressUI();
        UpdateTimerUI();

        CheckManager.Instance?.ResetChecklistUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }

    private void UpdateProgressUI()
    {
        if (progressText != null)
        {
            progressText.text = $"{cleanedItemCount} / {totalItemsToClean}";
        }
    }

    private void SaveTimeAndGoToEnding()
    {
        // 현재 기록 저장
        PlayerPrefs.SetFloat("CurrentTime", currentTime);

        // 최단 기록 비교 후 저장
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        if (currentTime < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", currentTime);
        }

        PlayerPrefs.Save();
        SceneManager.LoadScene("Ending");
    }

    public float GetCurrentTime() => currentTime;

    public void MarkItemAsCleaned(string itemName)
    {
        if (!cleanedItems.Contains(itemName))
        {
            cleanedItems.Add(itemName);
            CheckManager.Instance?.UpdateChecklist(itemName);
        }

        cleanedItemCount++;
        UpdateProgressUI();

        if (cleanedItemCount >= totalItemsToClean)
        {
            timerRunning = false;
            SaveTimeAndGoToEnding();
        }
    }
}