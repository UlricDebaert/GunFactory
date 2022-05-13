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
    [Tooltip("Trigger type")] public shootType fireMode;
    [Tooltip("Bullet instantiate for each shot")] public GameObject bulletPrefab;
    [Tooltip("Bullet damage on target")] public int bulletDamage;
    [Tooltip("Time between each shot")] public float fireRate;
    [Tooltip("Force apply on bullet on shot")] public float bulletSpeed;
    [Tooltip("Reduce gun accuracy")]public float bulletAngleShift;
    [Tooltip("Bullet quantity instantiate on each shot")]public float bulletQuantityPerShootPoint = 1;

    [Header("Knockback")]
    [Tooltip("Knockback apply on player for each shot")] public float knockbackOnPlayer;
    //public float knockbackOnTarget;

    [Header("Muzzleflash")]
    [Tooltip("Randomized flash instantiate for each shot")] public GameObject[] muzzleflashPrefabs;
    [Tooltip("Time before flash destruction")] public float muzzleflashLifeTime;

    [Header("Bullet Penetration")]
    //public bool penetratingBullet;
    [Tooltip("Penetrated targets before destruction of bullet")] public int maxTargetsPenetration;
    [Tooltip("Damage multiplier for each target penetrated, multiply with number of targets penetrated")] public float penetrationMultiplier;

    [Header("Reload")]
    [Tooltip("Magazine size")] public int magazine;
    [Tooltip("Time to reload weapon")] public float reloadTime;

    [Header("Audio")]
    [Tooltip("Audio play for each shot")] public AudioClip shootAudio;
    [Tooltip("Base audio pitch")] public float pitchBase = 1;
    [Tooltip("Pitch variation for each shot")] public float pitchVariation = 0;

    [Header("Animation")]
    [Tooltip("Base animation")] public AnimationClip idleAnimation;
    [Tooltip("Animation play for each shot")] public AnimationClip shootAnimation;
    [Tooltip("Animation when magazine is empty")] public AnimationClip emptyMagazineAnimation;
    [HideInInspector] [Tooltip("Load bullet animation, specific to pump weapon")] public AnimationClip cockingAnimation;
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

        //if(script.penetratingBullet)
        //{
        //    script.maxTargetsPenetration = EditorGUILayout.IntField("Max Targets Penetration", script.maxTargetsPenetration);
        //    script.penetrationMultiplier = EditorGUILayout.FloatField("Penetration Multiplier", script.penetrationMultiplier);
        //}
    }
}
