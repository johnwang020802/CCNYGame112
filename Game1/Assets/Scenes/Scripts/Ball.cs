using UnityEngine;

public class Ball : MonoBehaviour
{
    public int scoreValue = 10;
    
    private void OnCollisionEnter(Collision collision)
    {
    Renderer r = collision.gameObject.GetComponent<Renderer>();
        

        Destroy(gameObject);
    }
    
   void update()
   {
    
   }
}