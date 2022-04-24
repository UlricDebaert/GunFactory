using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyHP : MonoBehaviour
{
    [Header("Health Stats")]
    public int totalHP;
    [HideInInspector] public int currentHP;

    [Header("Health Bar")]
    public Image healthBarUI;
    public Gradient healthBarColor;

    void Start()
    {
        currentHP = totalHP;
        if (healthBarUI != null) UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (healthBarUI != null) UpdateHealthBar();
        if (currentHP <= 0) Death();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    void UpdateHealthBar()
    {
        healthBarUI.fillAmount = (float) currentHP / totalHP;
        healthBarUI.color = healthBarColor.Evaluate((float)currentHP / totalHP);
    }
}
