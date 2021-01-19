using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnDetector : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy") TrakerOn(collision);
	}


	void TrakerOn(Collider2D col)
	{
		col.GetComponentInChildren<EnemyVision>().pcDetected = true;
	}
}
