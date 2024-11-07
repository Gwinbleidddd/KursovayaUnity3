using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform followPlayer;
    private Transform cameraTransform;

    public Vector3 playerIns;

    public float MoveSpeed = 10f;
    void Start()
    {
        cameraTransform = transform;
        followPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void LateUpdate()
    {
        if (followPlayer != null)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, followPlayer.position + playerIns,
                MoveSpeed * Time.deltaTime);
        }
    }
}
