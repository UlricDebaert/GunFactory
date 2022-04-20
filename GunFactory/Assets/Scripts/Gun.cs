using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunSO gunStats;
    public Transform[] firePoints;

    SpriteRenderer gunSprite;

    int currentAmmoCount;
    float reloadTimer;

    bool canShoot;
    float fireRateTimer;


    void Start()
    {
        gunSprite = GetComponent<SpriteRenderer>();
        canShoot = false;
        currentAmmoCount = gunStats.magazine;
    }


    void Update()
    {
        LookToMousePosition();
        UpdateFireRate();
        UpdateGraphics();
        CheckInput();
        UpdateReloading();
    }

    void CheckInput()
    {
        if (Input.GetButton("Fire1") && canShoot && currentAmmoCount>0)
        {
            Fire();
        }
    }

    void Fire()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            GameObject bullet = Instantiate(gunStats.bulletPrefab, firePoints[i].position,gameObject.transform.localRotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(gunStats.bulletSpeed * transform.right.normalized, ForceMode2D.Impulse);
        }
        canShoot = false;
        fireRateTimer = gunStats.fireRate;
        currentAmmoCount--;
        if (currentAmmoCount <= 0)
        {
            reloadTimer = gunStats.reloadTime;
        }
    }

    void UpdateFireRate()
    {
        if (fireRateTimer>0.0f)
        {
            fireRateTimer -= Time.deltaTime;

        }
        else { canShoot = true; }
    }

    void UpdateReloading()
    {
        if (reloadTimer > 0.0f)
        {
            reloadTimer -= Time.deltaTime;

            if (reloadTimer <= 0.0f)
            {
                currentAmmoCount = gunStats.magazine;
            }
        }
    }

    void LookToMousePosition()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
    }

    void UpdateGraphics()
    {
        if(transform.eulerAngles.z >= 90 && transform.eulerAngles.z <= 260)
        {
            gunSprite.flipY = true;
        }
        else
        {
            gunSprite.flipY = false;
        }
    }
}