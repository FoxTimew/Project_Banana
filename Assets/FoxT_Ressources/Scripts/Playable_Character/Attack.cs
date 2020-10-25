using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public WeaponsInfos stats;

    public List<int> damageCombos = new List<int>();

    public List<float> pousseeCombos = new List<float>();

    public int damageSpecial;

    public float pousseeSpecial;

    public float range;

    public Transform attackPoint;

    public LayerMask enemyLayer;

    Controler controler;

	private void OnEnable()
	{

    }

	private void Start()
	{
        controler = this.GetComponent<Controler>();

        for (int i = 0; i <= stats.damageCombos.Length - 1; i++)
        {
            damageCombos.Add(stats.damageCombos[i]);
        }

        for (int i = 0; i <= stats.pousseeCombos.Length - 1; i++)
        {
            pousseeCombos.Add(stats.pousseeCombos[i]);
        }

        damageSpecial = stats.damageSpecial;

        pousseeSpecial = stats.pousseeSpecial;

        range = stats.range;
    }

	public void AttackSysteme()
    {

        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer);

        if (enemy == null) Debug.Log("Rien n'est detecte.");
        foreach (Collider2D enemyTouche in enemy)
        {
            enemyTouche.gameObject.GetComponent<EnemySys>().TakeDamage(damageCombos[0]);
        }
        controler.isAttacking = false;


    }

	private void OnDrawGizmosSelected()
	{
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, range);
	}
}
