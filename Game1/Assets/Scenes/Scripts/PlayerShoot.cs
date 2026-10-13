using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject ballPrefab;     
    public Transform cam;             
    public Transform firePoint;       
    public float shootForce = 20f;

    // Auto-fire variables
    public float fireRate = 0.2f;     // How fast it shoots (0.2 seconds between shots)
    private float nextFireTime = 0f;  // Tracks when the player is allowed to shoot again

    void Update()
    {
        // Check if the button is held down AND enough time has passed since the last shot
        if (Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextFireTime)
        {
            // Set the time for when the next bullet can be fired
            nextFireTime = Time.time + fireRate; 
            ShootAtCrosshair(); 
        }
    }

    void ShootAtCrosshair()
{
    Ray ray = new Ray(cam.position, cam.forward);
    Vector3 targetPoint;

    if (Physics.Raycast(ray, out RaycastHit hit, 100f))
    {
        targetPoint = hit.point; 
        
        // MISSING CODE ADDED BACK: Check if we are looking at an Enemy
        Enemy targetHit = hit.collider.GetComponent<Enemy>();
        if (targetHit != null)
        {
            targetHit.TakeDamage();
        }
    }
    else
    {
        targetPoint = ray.GetPoint(100f); 
    }

    Vector3 directionToTarget = (targetPoint - firePoint.position).normalized;
    Quaternion projectileRotation = Quaternion.LookRotation(directionToTarget);

    GameObject ball = Instantiate(ballPrefab, firePoint.position, projectileRotation);
    Rigidbody rb = ball.GetComponent<Rigidbody>();
    rb.AddForce(directionToTarget * shootForce, ForceMode.Impulse);
}
}