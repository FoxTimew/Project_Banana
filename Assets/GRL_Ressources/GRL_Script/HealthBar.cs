using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health health;

    int curentHealth;

    public Slider slider;
    void Start()
    {
        curentHealth = health.health;
    }

    // Update is called once per frame
    void Update()
    {
        curentHealth = health.health;
        SetHealth(curentHealth);
    }

    public void SetMaxHealth(int curentHealth)
    {
        slider.maxValue = curentHealth;
        slider.value = curentHealth;
    }

    public void SetHealth(int curentHealth)
    {
        slider.value = curentHealth;
    }
}
