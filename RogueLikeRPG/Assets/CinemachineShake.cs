using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using App.Scripts.GameScenes.Weapon;
using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    [SerializeField] private WeaponItemSO currentWeaponSO;
    
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private float shakeTimer;
    private float startIntensity;
    private float shakeTimerTotal;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Instance is not null!");
        }

        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = currentWeaponSO.shakeIntensity;
        startIntensity = currentWeaponSO.shakeIntensity;
        shakeTimerTotal = currentWeaponSO.shakeTime;
        shakeTimer = currentWeaponSO.shakeTime;
    }

    public void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                float ampitudeGain = Mathf.Lerp(startIntensity, 0f, (1 - (shakeTimer / shakeTimerTotal)));
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = ampitudeGain;
            }
        }
    }
}