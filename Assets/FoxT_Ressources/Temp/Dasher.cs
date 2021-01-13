using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Dasher : MonoBehaviour
{
    Health pcHealth;

    public EnemiesDescriptions stats;

    public int health;

    public Sprite sprite;

    public int attack;

    public float range;

    public AnimationCurve poussee;

    public int resistance;

    public int armor;

    EnnemyLoot loot;

    bool pcDeteced { get { return GetComponentInChildren<EnemyVision>().pcDetected; } }

    bool closed, isAttacking;

    public Transform pcTransform, self, checkPoint;

    Vector3 orientation;

    public float closedDistance, unclosedDistance, vitesse;

    public int attackTimer;

    float currentDistance;

    public Rigidbody2D enemyRB;

    public LayerMask pcLayer;

    bool IsDashing = true;

    [SerializeField] float dashSpeed = 3000f;

    private LayerMask dashLayerMask;

    private Vector3 moveDir;
    void Start()
    {
        sprite = stats.sprite;
        health = stats.hp;
        attack = stats.attack;
        range = stats.range;
        poussee = stats.poussee;
        resistance = stats.resistance;
        armor = stats.hpArmor;

        loot = this.GetComponent<EnnemyLoot>();

        pcHealth = GameObject.Find("Playable_Character").GetComponent<Health>();
    }

    void Update()
    {
        if (pcDeteced)
        {
            PCFocus();
            if (currentDistance <= closedDistance) closed = true;
            if (currentDistance >= unclosedDistance) closed = false;
            if (closed) AttackTest();
            else MoveToPlayer();
        }
    }

    public void TakeDamage(int damage)
    {
        if (armor <= 0)
        {
            if (damage - resistance <= 0) damage = 0;
            else damage -= resistance;
            health -= damage;
        }
        else
            armor -= damage / 2;

        if (health <= 0) Die();
    }

    void Die()
    {
        // play animation
        loot.Loot();
        Destroy(this.gameObject);
    }

    void PCFocus()
    {
        currentDistance = Mathf.Sqrt(Mathf.Pow(pcTransform.position.x - self.position.x, 2) + Mathf.Pow(pcTransform.position.y - self.position.y, 2));
        orientation = Vector3.Normalize((pcTransform.position - self.position) * 10);
        checkPoint.localPosition = orientation;
        float dist = Vector3.Distance(pcTransform.position, self.position);
        Debug.Log(dist);
        if (dist <= 3)
        {
            Debug.Log("dash");
            StartCoroutine(DashCoroutine());
        }
        orientation = Vector3.Normalize((pcTransform.position - self.position) * 10);
    }

    IEnumerator DashCoroutine()
    {
        Debug.Log("0.5");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("dashhhh");
        IsDashing = true;
        DashUpdate();

    }

    void DashUpdate()
    {
        enemyRB.velocity = moveDir * vitesse;
        Vector3 dashPosition = pcTransform.position + moveDir * dashSpeed;
        RaycastHit2D raycasthit2d = Physics2D.Raycast(pcTransform.position, moveDir, dashSpeed, dashLayerMask);

        if (raycasthit2d.collider != null)
        {
            dashPosition = raycasthit2d.point;
        }

        enemyRB.MovePosition(pcTransform.position + moveDir * dashSpeed);

        IsDashing = false;
    }

    void CoupDroit()
    {
        Collider2D[] pc = Physics2D.OverlapCircleAll(checkPoint.position, range, pcLayer);

        if (pc == null) Debug.Log("Rien n'est detecte.");
        foreach (Collider2D pcTouche in pc)
        {
            pcHealth.TakeDamage(attack);
        }
    }

    void MoveToPlayer()
    {
        enemyRB.velocity = orientation * vitesse * Time.deltaTime;
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
        StartCoroutine(AttackUpdate(attackTimer));
    }

    IEnumerator AttackUpdate(int time)
    {
        enemyRB.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(time);
        CoupDroit();
        isAttacking = false;
    }
}
