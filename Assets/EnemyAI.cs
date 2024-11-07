using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyAI1 : MonoBehaviour
{
    [SerializeField] public GameObject TargetDestination;
    [SerializeField] private float speed;
    public GameObject player;
    public Rigidbody rg;
    [SerializeField] public static int Points;
    public Material enemyMaterial;
    public float blinkTime = 0.1f;
    public int EnemyLifes = 2;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyMaterial = GetComponent<Renderer>().material;
    }
    public void FixedUpdate()
    {
        GetComponent<NavMeshAgent>().destination = player.transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet")) 
        {
            if (EnemyLifes == 1)
            {
                Points++;
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
