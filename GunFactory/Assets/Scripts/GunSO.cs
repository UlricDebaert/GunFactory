using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class GunSO : ScriptableObject
{
    public enum shootType { fullAuto, semiAuto, pump, charge }
    [Header("Shoot")]
    public shootType fireMode;
    public GameObject bulletPrefab;
    public float fireRate;
    public float bulletSpeed;
    public float bulletAngleShift;

    [Header("Reload")]
    public int magazine;
    public float reloadTime;

    [Header("Audio")]
    public AudioClip shootAudio;

    [Header("Sprite")]
    public Sprite baseSprite;
    public Sprite emptyMagazineSprite;
    public Animation reloadAnimation;
}
