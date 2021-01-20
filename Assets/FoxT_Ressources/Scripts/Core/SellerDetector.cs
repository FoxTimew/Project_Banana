using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerDetector : MonoBehaviour
{
	[SerializeField] GameObject Enemy;
	public bool playerIsHere;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Seller")
		{
			GameObject.Find("Core").GetComponent<Core>().seller.Add(collision.gameObject);
		}
		else if (collision.gameObject.tag == "Player")
		{
			playerIsHere = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			playerIsHere = false;
		}
	}

	private void Update()
	{
		if (Enemy == null) return;
		if (!playerIsHere) Enemy.SetActive(false);
		else Enemy.SetActive(true);
	}
}
