using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public PlayableCharacterInfos stats;

    public int health;

    public int resistance;
    void Start()
    {
        health = stats.HP;
        resistance = stats.resitance;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        int tempResistance = resistance;

        if (tempResistance > damage) tempResistance = damage;

        health -= (damage - tempResistance);

        //HUD update

        if (health <= 0) Die();
    }

    void Die()
    {
        //play animation
        //changer de tableau
        return;
    }
}
