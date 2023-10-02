using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    private Image fillBar;
    void Start()
    {
        fillBar = GetComponent<Image>();
    }
    
    void Update()
    {
        fillBar.fillAmount = PlayerController.instance.oxygenPoint / 100;
    }
}
