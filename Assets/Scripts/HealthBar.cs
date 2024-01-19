using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Foreground ui element
    [SerializeField] Image healthBarSprite;

    public void UpdateHealthBar(float currentHealth, float MaxHealth)
    {
        healthBarSprite.fillAmount = currentHealth / MaxHealth;
    }
}
