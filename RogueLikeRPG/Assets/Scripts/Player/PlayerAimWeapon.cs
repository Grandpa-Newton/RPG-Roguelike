using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerAimWeapon : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;


    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform bulletsContainer;


    [SerializeField] private float fireRateTime = 0.5f;
    private float timeToNextShot = 0;

    private Transform aimTransform;
    private Animator aimAnimator;
    private BulletSO bulletSO;

    private Queue<GameObject> aliveBullets = new Queue<GameObject>();

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimAnimator = aimTransform.GetComponent<Animator>();

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();
        Debug.Log(aliveBullets.Count);
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = +1f;
        }

        aimTransform.localScale = localScale;
    }


    IEnumerator BulletsContainerClear()
    {
        yield return new WaitUntil(() => aliveBullets.Count >= 20);
        while (aliveBullets.Count >= 20)
        {
            Destroy(aliveBullets.Dequeue());
        }
    }

    private void HandleShooting()
    {
        timeToNextShot += Time.deltaTime;
        if (_playerInputActions.Player.Attack.IsPressed() &&
            timeToNextShot > fireRateTime) // make with SO weapon.firaRateTime
        {
            aimAnimator.SetTrigger("Shoot");

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            if (bullet)
            {
                aliveBullets.Enqueue(bullet);
                bullet.transform.SetParent(bulletsContainer, true);
                bulletSO = bullet.GetComponent<Bullet>().GetBulletSO();
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(aimTransform.right * bulletSO.speed, ForceMode2D.Impulse);
            }

            StartCoroutine(BulletsContainerClear());
            timeToNextShot = 0f;
        }
    }

    private static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}