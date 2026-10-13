using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;
    public int scoreValue = 10;
    
    [Header("Glow Settings")]
    public float glowIntensity = 5f; // Increase this to make the glow brighter

    void Start()
    {
        Renderer r = GetComponent<Renderer>();
        if (r != null)
        {
            // Define the three colors
            Color[] colors = { Color.red, Color.green, Color.blue };
            
            // Pick a random color from the array
            Color chosenColor = colors[Random.Range(0, colors.Length)];

            // Apply the base color
            r.material.color = chosenColor;

            // Apply the glowing emission color (Color * Intensity)
            r.material.EnableKeyword("_EMISSION");
            r.material.SetColor("_EmissionColor", chosenColor * glowIntensity);
        }
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(scoreValue);
        }

        Destroy(gameObject);
    }
}