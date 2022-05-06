using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoCountPannel : MonoBehaviour
{
    public TMP_Text ammoCounterText;
    public Image ammoLoadingIcon;

    public static AmmoCountPannel instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
