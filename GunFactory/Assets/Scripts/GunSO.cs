using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Custom Inspector
using UnityEditor;


[CreateAssetMenu]
public class GunSO : ScriptableObject
{
    public enum shootType { fullAuto, semiAuto, pump }
    [Header("Shoot")]
    public shootType fireMode;
    public GameObject bulletPrefab;
    public int bulletDamage;
    public float fireRate;
    public float bulletSpeed;
    public float bulletAngleShift;

    [Header("Knockback")]
    public float knockbackOnPlayer;
    //public float knockbackOnTarget;

    [Header("Muzzleflash")]
    public GameObject[] muzzleflashPrefabs;
    public float muzzleflashLifeTime;

    [Header("Bullet Penetration")]
    public int maxTargetsPenetration;
    public float penetrationMultiplier;

    [Header("Reload")]
    public int magazine;
    public float reloadTime;

    [Header("Audio")]
    public AudioClip shootAudio;
    public float pitchBase;
    public float pitchVariation;

    [Header("Sprite")]
    public AnimationClip idleAnimation;
    public AnimationClip shootAnimation;
    public AnimationClip emptyMagazineAnimation;
    [HideInInspector] public AnimationClip cockingAnimation;
}


[CustomEditor(typeof(GunSO))]
public class GunSO_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        GunSO script = (GunSO)target;

        if(script.fireMode == GunSO.shootType.pump)
        {
            script.cockingAnimation = EditorGUILayout.ObjectField("Cocking Animation", script.cockingAnimation, typeof(AnimationClip), true) as AnimationClip;
        }
    }
}
