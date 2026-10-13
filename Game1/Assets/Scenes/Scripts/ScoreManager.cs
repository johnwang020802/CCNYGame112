using UnityEngine;
using TMPro; // Required to use TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 
    
    public int currentScore = 0;
    
    // A slot for UI Text
    public TextMeshProUGUI scoreText; 

    void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }

    void Start()
    {
        // Make sure the UI shows 0 when the game starts
        UpdateScoreUI(); 
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
        
    }

    void UpdateScoreUI()
    {
        // Updates the text on the screen
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }
}