using System.Collections;
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

    public LayerMask pcLayer;

    private Coroutine stunCoroutine;

    public string currentState, lastDirectionState;

    public float attackDelay, explosionRadius;

    public Vector3 VectorDirector;

    public int maxHealth, explosionCharmDamage;

    Animator anim;

    public bool dying;


    //------------------------------------------
    //Anim 
    //------------------------------------------
    public string idleLeft, idleRight, runLeft, runRight, attackLeft, attackRight, deathLeft, deathRight;


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
            if (pcDeteced)
            {
                displacement.enabled = true;
                PCFocus();
                if (currentDistance <= closedDistance) closed = true;
                if (currentDistance >= unclosedDistance) closed = false;
                if (closed) AttackTest();
                //else 
            }
        }
    }

    private void FixedUpdate()
    {
        if (!dying) MoveToPlayer();
        else enemyRB.velocity = Vector2.zero;
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
            stunCoroutine = StartCoroutine(StunUpdate(stunTime));
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
    }

    void PCFocus()
    {
        currentDistance = Mathf.Sqrt(Mathf.Pow(pcTransform.position.x - self.position.x, 2) + Mathf.Pow(pcTransform.position.y - self.position.y, 2));
        orientation = Vector3.Normalize((pcTransform.position - self.position) * 10);
        checkPoint.localPosition = orientation;
    }

    void CoupDroit()
    {
        Collider2D[] pc = Physics2D.OverlapCircleAll(checkPoint.position, range, pcLayer);

        if (pc == null) Debug.Log("Rien n'est detecte.");
        foreach (Collider2D pcTouche in pc)
        {
            pcHealth.TakeDamage(attack + Mathf.RoundToInt(dammageBonus));
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
        if (isAttacking) return;
        AttackStart();
    }

    void AttackStart()
    {
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
}
