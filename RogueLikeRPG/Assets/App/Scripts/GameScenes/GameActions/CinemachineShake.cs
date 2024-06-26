using App.Scripts.GameScenes.Weapon;
using Cinemachine;
using UnityEngine;

namespace App.Scripts.GameScenes.GameActions
{
    public class CinemachineShake : MonoBehaviour
    {
        public static CinemachineShake Instance { get; private set; }
    
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private float shakeTimer;
        private float startIntensity;
        private float shakeTimerTotal;

        private void Awake()
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

            WeaponItemSO currentPlayerWeapon = PlayerCurrentWeapon.Instance.CurrentPlayerWeapon;
        
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = currentPlayerWeapon.shakeIntensity;
            startIntensity = currentPlayerWeapon.shakeIntensity;
            shakeTimerTotal = currentPlayerWeapon.shakeTime;
            shakeTimer = currentPlayerWeapon.shakeTime;
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
}