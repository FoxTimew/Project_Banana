using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public Transform attackPoint;

    public float radius;

    public LayerMask enemyLayer;
    public void AttackSysteme()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.localPosition, radius, enemyLayer);

        foreach (Collider2D enemyTouche in enemy)
        {
            Debug.Log(enemyTouche.name + " touche");
        }
    }

	private void OnDrawGizmosSelected()
	{
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.localPosition, radius);
	}
}
