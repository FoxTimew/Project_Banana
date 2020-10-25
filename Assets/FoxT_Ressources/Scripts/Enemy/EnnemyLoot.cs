using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyLoot : MonoBehaviour
{
	public CollectibleDescription gold;
	public CollectibleDescription cristal;

	public GameObject goldObject, cristalObject;
	
	int goldChance, cristalChance;

	Transform self;

	private void Start()
	{
		goldChance = gold.chance;
		cristalChance = cristal.chance;
		self = this.GetComponent<Transform>();
	}

	int chance;
	public void Loot()
	{
		chance = Random.Range(1, 101);

		if (chance >= 1 && chance <= 100 - cristalChance)
		{
			Object.Instantiate(goldObject, transform.position, transform.rotation);
		}
		else Object.Instantiate(cristalObject, transform.position, transform.rotation);
	}
}
