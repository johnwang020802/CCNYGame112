using UnityEngine;

public class CoinG : MonoBehaviour

{
    //Run when another collider enters this coin's trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {

        // Check if the object touching the coin is the Player
        if (other.CompareTag("Player"))
        {
            // Add 1 point to the player's score using the GameManager
            GameManager.Instance.AddScore(1);

            //Remove the coin after it is collected
            if (this != null && gameObject != null) 
            {
                Destroy(gameObject);
            } 
        }
    }
}
