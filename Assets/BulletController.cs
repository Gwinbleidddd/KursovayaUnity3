using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Weapon : MonoBehaviour
{
    public Camera playerCamera;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject BazukaBulletPrefab;
    [SerializeField] public float bulletForce = 20f;
    [SerializeField] public static float fireRate = 0.5f;
    public static int FireMode = 1;

    private float nextFire = 0.0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (FireMode == 1)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
            
                    // Use the player object's forward vector instead of the player camera's forward vector
                    Vector3 direction = transform.forward;
            
                    rb.AddForce(direction * bulletForce, ForceMode.Impulse);
            
                    Destroy(bullet, 10.0f);
        }
        else if (FireMode == 2)
        {
            for (int i = 0; i < 3; i++)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
            
                    // Use the player object's forward vector instead of the player camera's forward vector
                    Vector3 direction = transform.forward;
            
                    // Add a random offset to the bullet's position and direction
                    float randomOffset = Random.Range(-0.2f, 0.2f);
                    bullet.transform.position += direction * randomOffset;
                    direction = Quaternion.Euler(Random.Range(-10.0f, 10.0f),Random.Range(-10.0f, 10.0f), 0) * direction;
            
                    rb.AddForce(direction * bulletForce, ForceMode.Impulse);
            
                    Destroy(bullet, 10.0f);
                }
        }
        else if (FireMode == 3)
        {
            GameObject bullet = Instantiate(BazukaBulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            
            // Use the player object's forward vector instead of the player camera's forward vector
            Vector3 direction = transform.forward;
            
            rb.AddForce(direction * bulletForce, ForceMode.Impulse);
            
            Destroy(bullet, 5.0f);
        }
    }
    
}