﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool RefusDeLaMort;
    public float charmValue;

    public PlayableCharacterInfos stats;

    public int maxHealth;
    public int health;
    public Attack attack;
    Core core;

    public int resistance;
    void Start()
    {
        maxHealth = stats.HP;
        health = maxHealth; 
        resistance = stats.resitance;
        attack = this.GetComponent<Attack>();
        core = GameObject.Find("Core").GetComponent<Core>();
        attack.force = stats.force;
        attack.pousseeForce = stats.pousseeForce;
    }

    public void TakeDamage(int damage)
    {
        int tempResistance = resistance;

        if (tempResistance > damage) tempResistance = damage;

        health -= (damage - tempResistance);

        //HUD update

        if (health <= 0)
        {
            //Tout bloquer
            if (!RefusDeLaMort) Die();
            else
            {
                StartCoroutine(Rebirth(10));
                //AnimationRefusDeLaMort

            }
        }
    }

    public void Heal(float value, bool pourcent)
    {
        if (pourcent) health += Mathf.RoundToInt(health * value);
        else health += Mathf.RoundToInt(value);

        if (health > maxHealth) health = maxHealth;
    }

    void Die()
    {
        //play animation
        //changer de tableau
        return;
    }

    IEnumerator Rebirth(float time)
    {
        yield return new WaitForSeconds(time);
        health = Mathf.RoundToInt(maxHealth * charmValue);
        RefusDeLaMort = false;
        core.data.niveauRefus = 0;
        charmValue = 0f;
    }
}
