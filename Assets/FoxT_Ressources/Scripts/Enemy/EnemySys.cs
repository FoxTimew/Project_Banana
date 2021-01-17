﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySys : MonoBehaviour
{
    Health pcHealth;
    Attack attackPC;

    public EnemiesDescriptions stats;

    public int health;

    public Sprite sprite;

    public int attack;

    public float range, timeBeforeDePop;

    public AnimationCurve poussee;

    public int resistance;

    public int armor;

    public List<float> stunReduction = new List<float>();

    public bool pause;

    EnnemyLoot loot;

    [SerializeField]
    Unit displacement;

    bool pcDeteced { get { return GetComponentInChildren<EnemyVision>().pcDetected; } }

    public bool closed, isAttacking, stuned, EnemyExplosionCharm;

    public Transform pcTransform, self, checkPoint;

    Vector3 orientation;

    public float closedDistance, unclosedDistance, vitesse, dammageBonus;

    float currentDistance;

    public Rigidbody2D enemyRB;

    public LayerMask pcLayer, kamikazeLayer;

    private Coroutine stunCoroutine;

    public string currentState, lastDirectionState;

    public float attackDelay, explosionRadius;

    public Vector3 VectorDirector;

    public int maxHealth, explosionCharmDamage;

    Animator anim;

    public bool dying, isEjected;

    Controler controler;

    bool playerisPoussing {get { return controler.isEjected; } }

    [SerializeField]
    bool isArmorGuardian, isSpawner, isKamikaze, spawnDone, dyingdone;

    [SerializeField]
    GameObject[] enemyToSpawn;
    [SerializeField]
    GameObject Spawner;

    //------------------------------------------
    //Anim 
    //------------------------------------------
    public string idleLeft, idleRight, runLeft, runRight, attackLeft, attackRight, deathLeft, deathRight;

	private void OnEnable()
	{
        pcTransform = GameObject.Find("Playable_Character").transform;
        controler = pcTransform.GetComponent<Controler>();
	}

	void Start()
    {
        anim = GetComponent<Animator>();
        displacement = this.GetComponentInChildren<Unit>();
        displacement.enabled = false;
        sprite = stats.sprite;
        maxHealth = stats.hp;
        health = maxHealth;
        attack = stats.attack;
        range = stats.range;
        poussee = stats.poussee;
        resistance = stats.resistance;
        armor = stats.hpArmor;
        vitesse = stats.vitesseDisplacement;
        attackDelay = stats.vitesseAttack;

        for (int i = 0; i < stats.stunReduction.Length; i++)
        {
            stunReduction.Add(stats.stunReduction[i]);
        }

        loot = this.GetComponent<EnnemyLoot>();

        pcHealth = GameObject.Find("Playable_Character").GetComponent<Health>();
        attackPC = GameObject.Find("Playable_Character").GetComponent<Attack>();
    }

    void Update()
    {
        if (!dying)
        {
            if (stuned) return;
            if (pcDeteced || isSpawner)
            {
                displacement.enabled = true;
                PCFocus();
                if (currentDistance <= closedDistance) closed = true;
                if (currentDistance >= unclosedDistance) closed = false;
                if ((closed || isSpawner) && !playerisPoussing)
                {
                    AttackTest();
                    if (lastDirectionState == runLeft || lastDirectionState == idleLeft) ChangeAnimationState(idleLeft);
                    else ChangeAnimationState(idleRight);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!dying && !closed && !isEjected && !isSpawner)
        {
            MoveToPlayer();
        }
        else enemyRB.velocity = Vector2.zero;
    }

	private void LateUpdate()
	{
        if (spawnDone)
        {
            GameObject.Find("Core").GetComponent<Core>().updateEnemyVision = true;
            spawnDone = false;
        }
	}

	public void TakeDamage(int damage, float stunTime)
    {
        if (armor <= 0)
        {
            if (damage - resistance <= 0) damage = 0;
            else damage -= resistance;
            health -= damage;
        }
        else
            armor -= damage / 2;

        if (health <= 0)
        {
            Die();
            return;
        }
        else
        {
            if (stunCoroutine != null) StopCoroutine(stunCoroutine);
            stunCoroutine = StartCoroutine(StunUpdate(stunTime - stunReduction[0]));
        }
    }

    void Die()
    {
        if (EnemyExplosionCharm)
        {
            //jouer autre animation
            Collider2D[] enemi = Physics2D.OverlapCircleAll(this.transform.position, explosionRadius);
            foreach (Collider2D col in enemi)
            {
                col.GetComponent<EnemySys>().TakeDamage(explosionCharmDamage, 0f);
            }
        }
        else
        {
            if (lastDirectionState == runLeft || lastDirectionState == idleLeft) ChangeAnimationState(deathLeft);
            else ChangeAnimationState(deathRight);
        }
        dying = true;
        displacement.enabled = false;
        this.GetComponent<Ejecting>().enabled = false;
        StartCoroutine(Destroyed());
        attackPC.UpgradeCharmCombos();
        loot.Loot();
    }

    IEnumerator Destroyed()
    {
        yield return new WaitForSeconds(timeBeforeDePop);
        Destroy(this.gameObject);
        if (isArmorGuardian) this.GetComponent<GuardianInvok>().enabled = true;
        dyingdone = true;
    }

    void PCFocus()
    {
        currentDistance = Mathf.Sqrt(Mathf.Pow(pcTransform.position.x - self.position.x, 2) + Mathf.Pow(pcTransform.position.y - self.position.y, 2));
        orientation = Vector3.Normalize((pcTransform.position - self.position) * 10);
        checkPoint.localPosition = orientation;
    }

    void CoupDroit()
    {
        Debug.Log("test");
        if (isKamikaze)
        {
            StartCoroutine(Explosion());
            return;
        }
        if (!isSpawner)
        {
            Collider2D[] pc = Physics2D.OverlapCircleAll(checkPoint.position, range, pcLayer);
            if (pc == null) Debug.Log("Rien n'est detecte.");
            foreach (Collider2D pcTouche in pc)
            {
                pcHealth.TakeDamage(attack + Mathf.RoundToInt(dammageBonus));
            }
        }
        else
        {
            if (GameObject.Find("Core").GetComponent<Core>().EnemyPresent.Count <= 6)
            {
                Object.Instantiate(enemyToSpawn[Random.Range(0, enemyToSpawn.Length)], Spawner.transform.position, transform.rotation);
                spawnDone = true;
            }
        }
        if (lastDirectionState == runLeft || lastDirectionState == idleLeft)
        {
            ChangeAnimationState(attackLeft);
            currentState = idleLeft;
        }
        else
        {
            ChangeAnimationState(attackRight);
            currentState = idleRight;
        }
        if (poussee != null && pcHealth.health > 0)
        {
            pcHealth.GetComponent<Controler>().knockBackForce = poussee;
            pcHealth.GetComponent<Controler>().pousseeDirection = VectorDirector;
            pcHealth.GetComponent<Controler>().Ejecting();
        }
    }

    void MoveToPlayer()
    {
        enemyRB.velocity = VectorDirector.normalized * vitesse * Time.fixedDeltaTime;
        if (enemyRB.velocity.x > 0) ChangeAnimationState(runRight);
        else if (enemyRB.velocity.x < 0) ChangeAnimationState(runLeft);
        else if (enemyRB.velocity.x == 0 && currentState == runLeft) ChangeAnimationState(idleLeft);
        else if (enemyRB.velocity.x == 0 && currentState == runRight) ChangeAnimationState(idleRight);
    }

    private void OnDrawGizmosSelected()
    {
        if (checkPoint == null) return;

        Gizmos.DrawWireSphere(checkPoint.position, range);
    }

    void AttackTest()
    {
        if (dying) return;
        if (isAttacking) return;
        AttackStart();
    }

    void AttackStart()
    {
        Debug.Log(closed);
        isAttacking = true;
        StartCoroutine(AttackUpdate(attackDelay));
    }

    public void Heal(float value, bool pourcent)
    {
        if (pourcent) health += Mathf.RoundToInt(health * value);
        else health += Mathf.RoundToInt(value);

        if (health > maxHealth) health = maxHealth;
    }

    void ChangeAnimationState(string newState)
    {
        if (dying) return;
        if (currentState == newState) return;
        if (newState == runLeft || newState == idleRight || newState == runRight || newState == idleLeft) lastDirectionState = newState;
        anim.Play(newState);
        currentState = newState;
    }

    IEnumerator AttackUpdate(float time)
    {
        enemyRB.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(time);
        CoupDroit();
        isAttacking = false;
    }

    IEnumerator StunUpdate(float time)
    {
        stuned = true;
        yield return new WaitForSeconds(time);
        stuned = false;
    }

    IEnumerator Explosion()
    {
        //Anim chargement
        yield return new WaitForSeconds(3f);
        //AnimaExplosion

        Collider2D[] pc = Physics2D.OverlapCircleAll(checkPoint.position, range, kamikazeLayer);
        if (pc == null) Debug.Log("Rien n'est detecte.");
        foreach (Collider2D pcTouche in pc)
        {
            if (pcTouche.gameObject.tag == "Player") pcHealth.TakeDamage(attack + Mathf.RoundToInt(dammageBonus));
            else pcTouche.GetComponent<EnemySys>().TakeDamage(attack + Mathf.RoundToInt(dammageBonus), 2f);
        }
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(checkPoint.position, range);
    }
}
