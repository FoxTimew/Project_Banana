using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public WeaponsInfos stats;

    public List<int> damageCombos = new List<int>();

    public List<AnimationCurve> pousseeCombos = new List<AnimationCurve>();

    public List<float> stunTime = new List<float>();

    public int damageSpecial;

    public float pousseeSpecial;

    public float range;

    public Transform attackPoint;

    public LayerMask enemyLayer;

    public float annulerCombosTemps;

    public float vitesse;

    Controler controler;

    public int combos;

    private Coroutine combosCoroutine;

    private bool attackBlocked;

    private void Start()
    {
        controler = this.GetComponent<Controler>();

        for (int i = 0; i < stats.damageCombos.Length; i++)
        {
            damageCombos.Add(stats.damageCombos[i]);
        }

        for (int i = 0; i < stats.pousseeCombos.Length; i++)
        {
            pousseeCombos.Add(stats.pousseeCombos[i]);
        }

        for (int i = 0; i < stats.stunTime.Length; i++)
        {
            stunTime.Add(stats.stunTime[i]);
        }

        damageSpecial = stats.damageSpecial;

        pousseeSpecial = stats.pousseeSpecial;

        range = stats.range;

        annulerCombosTemps = stats.annulerCombosTemps;

        vitesse = stats.vitesse;
    }

	public void AttackSysteme()
    {
        if (attackBlocked) return;
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer);
        if (enemy == null) return;
        foreach (Collider2D enemyTouche in enemy)
        {
            enemyTouche.gameObject.GetComponent<EnemySys>().TakeDamage(damageCombos[combos], stunTime[combos]);
            PousseeEnnemi(pousseeCombos[combos], enemyTouche.gameObject, attackPoint);
        }

        if (combos == damageCombos.Count - 1)
        {
            if (combosCoroutine != null) StopCoroutine(combosCoroutine);
            combos = 0;
        }
        else
        {
            if (combosCoroutine != null) StopCoroutine(combosCoroutine);
            combosCoroutine = StartCoroutine(combosCount(annulerCombosTemps));
        }
        StartCoroutine(WaitForAttackAgain(vitesse));
        controler.isAttacking = false;
    }

    void PousseeEnnemi(AnimationCurve force, GameObject enemy, Transform attackDirection)
    {
        Debug.Log("Attack");
        float angle = CalculArcTangante(attackDirection);
        Vector2 direction;

        if ((angle > -22.5f && angle < 0.0f) || (angle >= 0.0f && angle < 22.5f))
        {
            direction = Vector2.up;
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            direction = new Vector2(1f, 1f).normalized;
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            direction = Vector2.right;
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            direction = new Vector2(1f, -1f).normalized;
        }
        else if ((angle >= 157.5 && angle <= 180) || (angle > -180 &&angle <-157.5))
        {
            direction = Vector2.down;
        }
        else if (angle >= -157.5f && angle < -112.5f)
        {
            direction = new Vector2(-1f, -1f).normalized;
        }
        else if (angle >= -112.5f && angle < -67.5f)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = new Vector2(-1f, 1f).normalized;
        }
        enemy.GetComponent<Ejecting>().direction = direction;
        enemy.GetComponent<Ejecting>().pousseForce = force;
        enemy.GetComponent<Ejecting>().timeElapsed = 0f;
        enemy.GetComponent<Ejecting>().enabled = true;

        //enemy.gameObject.GetComponent<Rigidbody2D>().velocity = direction * force.Evaluate();
    }

	private void OnDrawGizmosSelected()
	{
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, range);
	}

    IEnumerator combosCount(float time)
    {
        combos++;
        yield return new WaitForSeconds(time);
        combos = 0;
    }

    IEnumerator WaitForAttackAgain(float delay)
    {
        attackBlocked = true;
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    float CalculArcTangante(Transform position)
    {
        return Mathf.Atan2(position.localPosition.x, position.localPosition.y) * Mathf.Rad2Deg;
    }
}
