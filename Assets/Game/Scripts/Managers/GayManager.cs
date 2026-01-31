using UnityEngine;
using UnityEngine.SceneManagement;

public class GayManager : MonoBehaviour
{
    public static GayManager Instance { get; private set; }

    [Header("Game State")]
    public bool isGameActive = true;
    public int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        Debug.Log("Game Started!");
    }

    public void UpdateScore(int points)
    {
        if (isGameActive)
        {
            score += points;
            Debug.Log("Current Score: " + score);
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        Debug.Log("Game Over!");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}