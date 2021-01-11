using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyLoot : MonoBehaviour
{
	public CollectibleDescription gold;
	public CollectibleDescription cristal;
	public CollectibleDescription maudite;

	public GameObject bourseObject;

	Transform self;

	public bool charmLoot;
	public int value;

	private void Awake()
	{
		self = this.GetComponent<Transform>();
	}

	int chance;
	public void Loot()
	{
		Object.Instantiate(bourseObject, transform.position, transform.rotation);
		
		chance = Random.Range(1, 101);
		for (int i = 1; i < gold.value.Length; i++)
		{
			if (chance <= gold.value[i].probability)
			{
				GameObject.Find("Bourse").GetComponent<CollectibleSys>().gold = gold.value[i].bourse;
				if (charmLoot)
				{
					chance = Random.Range(1, 101);
					if (chance <= value) GameObject.Find("Bourse").GetComponent<CollectibleSys>().gold++;
				}
				i = gold.value.Length;
			}
		}
		chance = Random.Range(1, 101);
		for (int i = 1; i < cristal.value.Length; i++)
		{
			if (chance <= cristal.value[i].probability)
			{
				GameObject.Find("Bourse").GetComponent<CollectibleSys>().cristal = cristal.value[i].bourse;
				if (charmLoot)
				{
					chance = Random.Range(1, 101);
					if (chance <= value) GameObject.Find("Bourse").GetComponent<CollectibleSys>().cristal++;
				}
				i = cristal.value.Length;
			}
		}
		chance = Random.Range(1, 101);
		for (int i = 1; i < maudite.value.Length; i++)
		{
			if (chance <= maudite.value[i].probability)
			{
				GameObject.Find("Bourse").GetComponent<CollectibleSys>().maudite = maudite.value[i].bourse;
				i = maudite.value[i].probability;
			}
		}
	}
}
