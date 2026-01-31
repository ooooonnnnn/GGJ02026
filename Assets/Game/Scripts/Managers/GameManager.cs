using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] public CanvasGroup fadeGroup;
    public float fadeDuration = 1.0f;
    [SerializeField] public TextMeshProUGUI scoreText;

    [Header("Game State")]
    public bool isGameActive = true;
    public int score = 0;

    // private void Awake()
    // {
    //     if (!Instance)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject); 
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }
    public void IncreaseScore()
    {
        score += 1;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }
    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        Debug.Log("Game Started!");
    }

    public void UpdateScore(int added_s)
    {
        if (isGameActive)
        {
            score += added_s;
            Debug.Log(score);
            UpdateScoreUI();
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        StartCoroutine(FadeAndRestart());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private IEnumerator FadeAndRestart()
    {
        float timer = 0;

        // Loop until the timer reaches the duration
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            // Gradually increase alpha from 0 to 1
            fadeGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }

        // Wait a tiny beat at total blackness
        yield return new WaitForSeconds(0.5f);

        // Reload the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void QuitGame() => Application.Quit();
    void OnEnable() { EnemyAI.OnEnemyKilled += UpdateScore; }
}