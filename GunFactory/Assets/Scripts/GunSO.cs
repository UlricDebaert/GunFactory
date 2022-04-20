using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class GunSO : ScriptableObject
{
    [Header("Shoot")]
    public GameObject bulletPrefab;
    public float fireRate;
    public float bulletSpeed;

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
