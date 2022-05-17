using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyHP : MonoBehaviour
{
    [Header("Health Stats")]
    public int totalHP;
    [HideInInspector] public int currentHP;
    [HideInInspector] public bool isDead;

    [Header("Health Bar")]
    public GameObject healthBarUIParent;
    public Image healthBarUIFill;
    public Gradient healthBarColor;

    [Header("Graphics")]
    public Animator anim;
    public SpriteRenderer sprite;
    public Material enemyFlashMat;
    Material defaultMat;


    void Start()
    {
        defaultMat = sprite.material;

        isDead = false;
        currentHP = totalHP;
        if (healthBarUIFill != null) UpdateHealthBar();

        anim.SetBool("isDead", isDead);
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            sprite.material = enemyFlashMat;
            Invoke("ResetMaterial", 0.05f);
        }

        currentHP -= damage;

        if (healthBarUIFill != null) UpdateHealthBar();
        if (currentHP <= 0) Death();
    }

    public void Death()
    {
        isDead = true;
        //Destroy(gameObject);

        anim.SetBool("isDead", isDead);
        healthBarUIParent.SetActive(false);
    }

    void UpdateHealthBar()
    {
        healthBarUIFill.fillAmount = (float) currentHP / totalHP;
        healthBarUIFill.color = healthBarColor.Evaluate((float)currentHP / totalHP);
    }

    void ResetMaterial()
    {
        sprite.material = defaultMat;
    }
}
