using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerDetector : MonoBehaviour
{
	public bool playerIsHere;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(collision.gameObject.tag);
		if (collision.gameObject.tag == "Seller")
		{
			GameObject.Find("Core").GetComponent<Core>().seller.Add(collision.gameObject);
			Debug.Log("OUFF");
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
}
