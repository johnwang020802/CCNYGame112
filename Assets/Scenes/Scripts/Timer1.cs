using UnityEngine;
using UnityEngine.SceneManagement;


public class Timer1 : MonoBehaviour
{
    public float timer = 3f;   // starting time in 3 seconds

    void Update()
    {
        // Step 2: Make the timer go down
        timer -= Time.deltaTime;

        // Step 3: Check when time is up
        if (timer <= 0f)
        {
            // Step 4: Run your time's-up code
            Debug.Log("Timer finished!");

            // stop timer from going negative
             timer = 0f;
           // SceneManager.LoadScene("Game Over");
        }
    }
}

