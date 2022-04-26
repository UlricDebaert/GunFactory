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

    public ParticleSystem bulletShellEffect;

    //specific to fire mods
    bool barrelEmpty;

    void Start()
    {
        gunSprite = GetComponent<SpriteRenderer>();
        canShoot = false;
        currentAmmoCount = gunStats.magazine;
        UpdateAmmoCount();
        if (AmmoCountPannel.instance != null && AmmoCountPannel.instance.ammoLoadingIcon != null)
            AmmoCountPannel.instance.ammoLoadingIcon.fillAmount = 0.0f;
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
        switch (gunStats.fireMode)
        {
            case GunSO.shootType.fullAuto:
                if (Input.GetButton("Fire1") && canShoot && currentAmmoCount > 0)
                {
                    Fire();
                    bulletShellEffect.Play();
                }
                break;

            case GunSO.shootType.semiAuto:
                if (Input.GetButtonDown("Fire1") && canShoot && currentAmmoCount > 0)
                {
                    Fire();
                    bulletShellEffect.Play();
                }
                break;

            case GunSO.shootType.pump:
                if (Input.GetButtonDown("Fire1") && canShoot && currentAmmoCount > 0)
                {
                    Fire();
                }
                break;

            case GunSO.shootType.charge:
                if (Input.GetButton("Fire1") && canShoot && currentAmmoCount > 0)
                {
                    Fire();
                    bulletShellEffect.Play();
                }
                break;


        }
    }
    //    if (Input.GetButton("Fire1") && canShoot && currentAmmoCount>0)
    //    {
    //        Fire();
    //    }
    //}

    void Fire()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            GameObject bullet = Instantiate(gunStats.bulletPrefab, firePoints[i].position, gameObject.transform.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-gunStats.bulletAngleShift, gunStats.bulletAngleShift)));
            bullet.GetComponent<Rigidbody2D>().AddForce(gunStats.bulletSpeed * bullet.transform.right.normalized, ForceMode2D.Impulse);
            bullet.GetComponent<Bullet>().bulletDamage = gunStats.bulletDamage;
            bullet.GetComponent<Bullet>().maxTargetsPenetration = gunStats.maxTargetsPenetration;
            bullet.GetComponent<Bullet>().penetrationMultiplier = gunStats.penetrationMultiplier;
        }

        canShoot = false;
        fireRateTimer = gunStats.fireRate;
        currentAmmoCount--;

        if (currentAmmoCount <= 0)
        {
            reloadTimer = gunStats.reloadTime;
        }
                
        gameObject.GetComponentInParent<Rigidbody2D>().AddForce(
            new Vector2(Mathf.Sign(-(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x)), 0) * gunStats.knockbackOnPlayer, 
            ForceMode2D.Impulse);

        UpdateAmmoCount();
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

            if (AmmoCountPannel.instance != null && AmmoCountPannel.instance.ammoLoadingIcon != null && reloadTimer > 0.0f) 
                AmmoCountPannel.instance.ammoLoadingIcon.fillAmount = 1-reloadTimer/gunStats.reloadTime;

            if (reloadTimer <= 0.0f)
            {
                currentAmmoCount = gunStats.magazine;

                if (AmmoCountPannel.instance != null && AmmoCountPannel.instance.ammoLoadingIcon != null) 
                    AmmoCountPannel.instance.ammoLoadingIcon.fillAmount = 0.0f;

                UpdateAmmoCount();
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
            bulletShellEffect.gameObject.transform.localEulerAngles = new Vector3(90, bulletShellEffect.gameObject.transform.eulerAngles.y, bulletShellEffect.gameObject.transform.eulerAngles.z);
        }
        else
        {
            gunSprite.flipY = false;
            bulletShellEffect.gameObject.transform.localEulerAngles = new Vector3(-90, bulletShellEffect.gameObject.transform.eulerAngles.y, bulletShellEffect.gameObject.transform.eulerAngles.z);
        }
    }

    void UpdateAmmoCount()
    {
        if (AmmoCountPannel.instance != null && AmmoCountPannel.instance.ammoCounterText != null)
        {
            AmmoCountPannel.instance.ammoCounterText.text = currentAmmoCount.ToString() + " / " + gunStats.magazine.ToString();
        }
    }
}