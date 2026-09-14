using UnityEngine;
using TMPro;   // Only needed if you use TextMeshPro for score UI
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // Make this script globally accessible
    public static GameManager Instance;

    [Header("Score System")]
    public int score = 0;              // Player's score
    public TMP_Text scoreText;         // UI text that displays the score


    [Header("Coin Prefabs")]
    public GameObject goldenCoinPrefab;   // +score
    public GameObject silverCoinPrefab;   // -score

    [Header("Coin Spawning")]  //Coins spawn at random poisiton inside below box
    public Vector2 spawnMin = new Vector2(-5f, -3f);
    public Vector2 spawnMax = new Vector2(5f, 3f);

    [Header("Timers")]
    public float goldenInterval = 3f;     // Golden coin every 3 seconds
    public float silverInterval = 5f;     // Silver coin every 5 seconds

    private float goldenTimer;
    private float silverTimer;

    void Awake()
    {
        // Set the global instance
        Instance = this;
    }

    void Start()
    {
        // Golden coin waits 3 seconds before first spawn
        goldenTimer = goldenInterval;

        // Silver coin spawns immediately (timer starts at 0)
        silverTimer = 0f;

        // Update score UI at the start
        UpdateScoreUI();
    }

    void Update()
    {
        // Golden coin timer
        goldenTimer -= Time.deltaTime;
        if (goldenTimer <= 0f)
        {
            SpawnGoldenCoin();
            goldenTimer = goldenInterval;
        }

        // Silver coin timer (starts at 0)
        silverTimer += Time.deltaTime;
        if (silverTimer >= 5f)
        {
            SpawnSilverCoin();
            silverTimer = 0;
        }

        // Spawn coin when SPACE is pressed
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SpawnGoldenCoin();
            SpawnSilverCoin();
        }

        //Spawn coins by J and L keys
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            SpawnGoldenCoin();
        }

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            SpawnSilverCoin();
        }
    }

    // Increase score and update UI
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    // Update the score text on screen
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    Vector3 GetRandomSpawnPos()
    {
        float x = Random.Range(spawnMin.x, spawnMax.x);
        float y = Random.Range(spawnMin.y, spawnMax.y);
        return new Vector3(x, y, 0);
    }

    void SpawnGoldenCoin()
    {
        Instantiate(goldenCoinPrefab, GetRandomSpawnPos(), Quaternion.identity);
    }

    void SpawnSilverCoin()
    {
        Instantiate(silverCoinPrefab, GetRandomSpawnPos(), Quaternion.identity);
    }
}
