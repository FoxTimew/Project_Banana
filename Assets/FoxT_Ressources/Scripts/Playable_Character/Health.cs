using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public bool RefusDeLaMort, berzerkCharm, mLootCharm, godeMode;
    public float charmValue;

    public PlayableCharacterInfos stats;

    public int maxHealth;
    public int health;
    public Attack attack;
    Core core;
    InventoryManager UI_Life;

    public int resistance, resistanceBonus;

    Controler controler;
    bool isParrying { get { return controler.isParrying; } }

    private void Awake()
    {
        UI_Life = GameObject.Find("Main Camera").GetComponentInChildren<InventoryManager>();
        controler = GetComponent<Controler>();
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

    public void TakeDamage(int damage, Collider2D col)
    {
        if (godeMode) return;
        if (isParrying)
        {
            if (damage > 0)
            {
                if (col.tag == "Enemy") col.GetComponent<EnemySys>().TakeDamage(1, 0f);
                else if (col.tag == "BossHand" && (col.gameObject.name == "Main_1" || col.gameObject.name == "Main_0")) col.gameObject.GetComponent<PoinAttaque>().TakeDammage(attack.force);
                else if (col.tag == "BossHand" && (col.gameObject.name == "Main_2" || col.gameObject.name == "Main_3")) col.gameObject.GetComponent<SlapAttaque>().TakeDammage(attack.force);
                controler.ejectionCancel = true;
                StartCoroutine(controler.RepostAnimationDelay());
            }
            return;
        }
        int tempResistance = resistance + resistanceBonus;
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
                StartCoroutine(Rebirth(10));
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
        FindObjectOfType<AudioManager>().Play("SFX_PC_Die");
        StartCoroutine(DieTP());
    }

    IEnumerator Rebirth(float time)
    {
        yield return new WaitForSeconds(time);
        health = Mathf.RoundToInt(maxHealth * charmValue);
        RefusDeLaMort = false;
        core.data.niveauRefus = 0;
        charmValue = 0f;
        UI_Life.HealthDisplay(health);
        controler.refusDeLaMort = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator DieTP()
    {
        GetComponent<Animator>().SetBool("respawn", true);
        yield return new WaitForSeconds(1.9f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
