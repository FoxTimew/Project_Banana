using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pic : MonoBehaviour
{
	List<Collider2D> thing = new List<Collider2D>();
	[SerializeField]
	float damage;

	Core core;
	float charmDamageBackup {get { return core.luckyCharmValue; }}

	private void Awake()
	{
		core = GameObject.Find("Core").GetComponent<Core>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") thing.Add(collision);
		StartCoroutine(AttackStupideThing(5, collision));
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") && thing.Contains(collision)) thing.Remove(collision);
	}

	IEnumerator AttackStupideThing(float time, Collider2D obj)
	{

		yield return new WaitForSeconds(time); //durer animation;
		if (thing.Contains(obj))
		{
			int charmDamage = 0;
			if (core.luckyCharm) charmDamage = Mathf.RoundToInt(charmDamageBackup);
			if (obj.gameObject.tag == "Player") obj.GetComponent<Health>().TakeDamage(Mathf.RoundToInt(damage));
			else obj.GetComponent<EnemySys>().TakeDamage(Mathf.RoundToInt(damage) + Mathf.RoundToInt(damage * charmDamage), 0f);
			StartCoroutine(AttackStupideThing(time, obj));
		}
	}
}
