using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour
{
	Core core;
	float healCharmValue {get { return core.heamCharmValue; } }

	private void Awake()
	{
		core = GameObject.Find("Core").GetComponent<Core>();	
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if (collision.GetComponent<Health>().health != collision.GetComponent<Health>().maxHealth)
			{
				collision.GetComponent<Health>().Heal(Mathf.RoundToInt(healCharmValue), false);
				Destroy(this.gameObject);
			}
		}
	}
}
