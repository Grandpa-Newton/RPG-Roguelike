using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    private Camera _camera;
    
    private void Awake()
    {
        //vcam = GetComponent<CinemachineVirtualCamera>();
        StartCoroutine(FollowPlayerWhenReady());
    }

    IEnumerator FollowPlayerWhenReady()
    {
        GameObject player = null;
        while (player == null)
        {
            player = GameObject.FindWithTag("Player");
            yield return null; // wait for next frame
        }
        vcam.LookAt = player.transform;
        vcam.Follow = player.transform;
    }
}