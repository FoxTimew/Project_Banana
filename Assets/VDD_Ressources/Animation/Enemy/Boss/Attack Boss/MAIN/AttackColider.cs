using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColider : MonoBehaviour
{
	public LayerMask pcLayer;
	public Collider2D col;

	public int dammage;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player") col = collision;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		col = null;
	}

	private void OnEnable()
	{
		if (col != null) col.gameObject.GetComponent<Health>().TakeDamage(dammage, col);
	}
}
