using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{

	public bool pcDetected;

	public LayerMask pcLayer;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 8) pcDetected = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 8) pcDetected = false;
	}
}
