using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Death : MonoBehaviour
{
    [SerializeField] AudioSource deathFallSound;
    [SerializeField] public UnityEngine.UI.Text LifesText;
    [SerializeField] AudioSource deathKillSound;
    [SerializeField] public UnityEngine.UI.Text gameover;
    [SerializeField] int SpeedDuration = 3;
    [SerializeField] private int DurabilityDuration = 3;
    private bool dead = false;
    [SerializeField] public int Lifes = 3;
    [SerializeField] public float SpeeedBonusAmount = 10f;
    public float speedMod = PlayerControl.speed;
    private bool Immortality = true;
    public Material playerMaterial;
    public float blinkTime = 0.1f;
    [SerializeField] public UnityEngine.UI.Text PointsText;

    private float akAmount = 3f;
    private float akMod = Weapon.fireRate;

    public float AKDuration = 20f;

    void Start()
    {
        playerMaterial = GetComponent<Renderer>().material;
    }
    
    
    private void Update()
    {
        PointsText.text = "Points: " + EnemyAI1.Points;
        if (transform.position.y < -1f && !dead)
        {
            deathFallSound.Play();
            Die();
        }
        if (Lifes <= 0)
        {
            gameover.text = "GAME OVER";
            Die();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<PlayerControl>().enabled = false;


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy Body") && Immortality) // отнять жизнь за столкновение с машиной
        {
            Lifes--;
            LifesText.text = "Lifes: " + Lifes;
            ReceiveLesson();
            Destroy(GameObject.FindGameObjectWithTag("Enemy Body"));
        }
        if (other.gameObject.CompareTag("EnemyBullet") && Immortality) // отнять жизнь за столкновение с машиной
        {
            Lifes--;
            LifesText.text = "Lifes: " + Lifes;
            ReceiveLesson();
        }
        if (other.gameObject.CompareTag("DurabilityBonus")) // дать прочность за бонус
        {
            
            Destroy(GameObject.FindGameObjectWithTag("DurabilityBonus"));
            StartCoroutine(DurabilityBuff());
        }
        if (other.gameObject.CompareTag("SpeedBonus")) // дать скорость за бонус
        {
            Destroy(GameObject.FindGameObjectWithTag("SpeedBonus"));
            StartCoroutine(SpeedBuff());
        }
        if (other.gameObject.CompareTag("AK"))
        {
            Destroy(GameObject.FindGameObjectWithTag("AK"));
            StartCoroutine(AK());
        }
        if (other.gameObject.CompareTag("Drob"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Drob"));
            StartCoroutine(Drob());
        }
        if (other.gameObject.CompareTag("Bazuka"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Bazuka"));
            StartCoroutine(Bazuka());
        }
    }
    
    void Die()
    {
        dead = true;
        EnemyAI1.Points = 0;
        Invoke(nameof(ReloadLife),3.3f);
    }

    void ReloadLife()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator AK()
    {
        ActivateAK();
        yield return new WaitForSeconds(AKDuration);
        DeactivateAK();
    }

    void ActivateAK()
    {
        Weapon.fireRate += akAmount;
    }
    void DeactivateAK()
    {
        Weapon.fireRate = akMod;
    }
    
    
    IEnumerator Drob()
    {
        ActivateDrob();
        yield return new WaitForSeconds(AKDuration);
        DeactivateDrob();
    }

    void ActivateDrob()
    {
        Weapon.FireMode = 2;
    }
    void DeactivateDrob()
    {
        Weapon.FireMode = 1;
    }
    
    
    IEnumerator Bazuka()
    {
        ActivateBazuka();
        yield return new WaitForSeconds(AKDuration);
        DeactivateBazuka();
    }
    void ActivateBazuka()
    {
        Weapon.FireMode = 3;
    }
    void DeactivateBazuka()
    {
        Weapon.FireMode = 1;
    }
    
    
    
    IEnumerator SpeedBuff()
    {
        ActivateSpeedBonus();
        yield return new WaitForSeconds(SpeedDuration);
        DeactivateSpeedBonus();
    }
    void ActivateSpeedBonus()
    {
        PlayerControl.speed += SpeeedBonusAmount;
    }
    void DeactivateSpeedBonus()
    {
        PlayerControl.speed = speedMod;
    }

    IEnumerator DurabilityBuff()
    {
        ActivateDurabilityBonus();
        yield return new WaitForSeconds(DurabilityDuration);
        DeactivateDurabilityBonus();
    }
    void ActivateDurabilityBonus()
    {
        Immortality = false;
    }
    void DeactivateDurabilityBonus()
    {
        Immortality = true;
    }
    public void ReceiveLesson()
    {
        StartCoroutine(Blink());
    }
    IEnumerator Blink()
    {
        for (int i = 0; i < 5; i++)
        {
            playerMaterial.SetColor("_EmissionColor", Color.red);
            yield return new WaitForSeconds(blinkTime);
            playerMaterial.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(blinkTime);
        }
    }
}
