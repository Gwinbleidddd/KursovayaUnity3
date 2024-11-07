using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyAI2 : MonoBehaviour
{
    [SerializeField] public GameObject TargetDestination;
    [SerializeField] private float speed;
    public GameObject player;
    public Rigidbody rg;
    public Material enemyMaterial;
    public float blinkTime = 0.1f;
    public int EnemyLifes = 2;
    
    [SerializeField] public float bulletForce = 20f;
    [SerializeField] public float fireRate = 0.5f;
    
    public Transform firePoint;
    
    public float chaseDistance = 10f;
    public float shootingDistance = 15f;
    public float stoppingDistance = 2f;
    private float distanceToPlayer = 0f;
    private NavMeshAgent agent;
    private float timeToFire = 0f;

    public GameObject bulletPrefab;
    private float nextFire = 0.0f;
    
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        firePoint = this.gameObject.transform;
        enemyMaterial = GetComponent<Renderer>().material;
    }
    public void Update()
    {
        GetComponent<NavMeshAgent>().destination = player.transform.position;
        
        distanceToPlayer = Vector3.Distance(player.transform.position, this.gameObject.transform.position);

        if (distanceToPlayer <= chaseDistance)
        {
            GetComponent<NavMeshAgent>().destination = player.transform.position;
            GetComponent<NavMeshAgent>().stoppingDistance = stoppingDistance;

            if (distanceToPlayer <= shootingDistance)
            {
                timeToFire -= Time.deltaTime;
                if (timeToFire <= 0)
                {
                    timeToFire = 1f / fireRate;
                    ShootAtPlayer();
                }
            }
        }
    }
    void ShootAtPlayer()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Use the player object's forward vector instead of the player camera's forward vector
        Vector3 direction = transform.forward;

        rb.AddForce(direction * bulletForce, ForceMode.Impulse);

        Destroy(bullet, 10.0f);
    }   

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet")) 
        {
            if (EnemyLifes == 1)
            {
                EnemyAI1.Points++;
                Destroy(this.gameObject);
            }
            else
            {
                ReceiveLesson();
                EnemyLifes--;
            }
        }
    }
    public void ReceiveLesson()
    {
        StartCoroutine(Blink());
    }
    public IEnumerator Blink()
    {
        for (int i = 0; i < 5; i++)
        {
            enemyMaterial.SetColor("_Color", Color.red);
            yield return new WaitForSeconds(blinkTime);
            enemyMaterial.SetColor("_Color", Color.yellow);
            yield return new WaitForSeconds(blinkTime);
        }
    }
}
