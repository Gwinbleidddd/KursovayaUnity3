using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMessenger : MonoBehaviour
{
    public Material playerMaterial;
    public float blinkTime = 0.1f;
    
    void Start()
    {
        playerMaterial = GetComponent<Renderer>().material;
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