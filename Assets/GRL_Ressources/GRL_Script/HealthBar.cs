using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Health health;

    int curentHealth;

    public Slider slider;

	private void Awake()
	{
        health = GameObject.Find("Playable_Character").GetComponent<Health>();
	}
	void Start()
    {
        curentHealth = health.health;
    }

    // Update is called once per frame
    void Update()
    {
        curentHealth = Mathf.RoundToInt((health.health * 100) / health.maxHealth);
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
