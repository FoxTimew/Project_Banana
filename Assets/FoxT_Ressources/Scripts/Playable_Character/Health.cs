using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool RefusDeLaMort, berzerkCharm, mLootCharm;
    public float charmValue;

    public PlayableCharacterInfos stats;

    public int maxHealth;
    public int health;
    public Attack attack;
    Core core;
    InventoryManager UI_Life;

    public int resistance;

	private void Awake()
	{
        UI_Life = GameObject.Find("Main Camera").GetComponentInChildren<InventoryManager>();
	}

	void Start()
    {
        maxHealth = stats.HP;
        health = maxHealth;
        UI_Life.HealthDisplay(health);
        resistance = stats.resitance;
        attack = this.GetComponent<Attack>();
        core = GameObject.Find("Core").GetComponent<Core>();
        attack.force = stats.force;
        attack.pousseeForce = stats.pousseeForce;
    }

    public void TakeDamage(int damage)
    {
        int tempResistance = resistance;
        if (mLootCharm)
        {
            int gold = core.data.gold;
            gold -= 10;
            while (gold <= 0)
            {
                tempResistance--;
                gold -= 10;
            }
        }

        if (tempResistance > damage) tempResistance = damage;

        health -= (damage - tempResistance);
        this.GetComponent<Controler>().isTouched = true;
        BerzerkUpdate();
        //HUD update

        if (health <= 0)
        {
            //Tout bloquer
            health = 0;
            if (!RefusDeLaMort) Die();
            else
            {
                this.GetComponent<Controler>().refusDeLaMort = true;
                this.GetComponent<Controler>().isDie = true;
                //StartCoroutine(Rebirth(10)); 
            }
        }
        if (berzerkCharm) BerzerkUpdate();
        UI_Life.HealthDisplay(health);
    }

    public void Heal(float value, bool pourcent)
    {
        if (pourcent) health += Mathf.RoundToInt(health * value);
        else health += Mathf.RoundToInt(value);

        if (health > maxHealth) health = maxHealth;
        UI_Life.HealthDisplay(health);
    }

    public void BerzerkUpdate()
    {
        attack.berzerkValue = Mathf.RoundToInt((((maxHealth - health) * 100) / maxHealth) / 100);
    }

    void Die()
    {
        this.GetComponent<Controler>().isDie = true;
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
