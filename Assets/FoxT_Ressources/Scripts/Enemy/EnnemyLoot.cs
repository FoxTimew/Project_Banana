using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyLoot : MonoBehaviour
{
	public CollectibleDescription gold;
	public CollectibleDescription cristal;
	public CollectibleDescription maudite;

	public GameObject bourseObject, healPotion;

	public bool mLootCharm;

	Transform self;

	public bool charmLoot;
	public int value;

	Core core;
	float healCharmProba { get { return core.heamCharmProbability; } }
	bool healPotionCharm {get { return core.healCharm; } }
	private void Awake()
	{
		core = GameObject.Find("Core").GetComponent<Core>();
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
				GameObject.Find("Bourse(Clone)").GetComponent<CollectibleSys>().gold = gold.value[i].bourse;
				if (mLootCharm)
				{
					GameObject.Find("Bourse(Clone)").GetComponent<CollectibleSys>().gold += Mathf.RoundToInt(gold.value[i].bourse * .5f);
				}
				if (charmLoot)
				{
					chance = Random.Range(1, 101);
					if (chance <= value) GameObject.Find("Bourse(Clone)").GetComponent<CollectibleSys>().gold++;
				}
				i = gold.value.Length;
			}
		}
		chance = Random.Range(1, 101);
		for (int i = 1; i < cristal.value.Length; i++)
		{
			if (chance <= cristal.value[i].probability)
			{
				GameObject.Find("Bourse(Clone)").GetComponent<CollectibleSys>().cristal = cristal.value[i].bourse;
				if (mLootCharm)
				{
					GameObject.Find("Bourse(Clone)").GetComponent<CollectibleSys>().cristal += Mathf.RoundToInt(cristal.value[i].bourse* .5f);
				}
				if (charmLoot)
				{
					chance = Random.Range(1, 101);
					if (chance <= value) GameObject.Find("Bourse(Clone)").GetComponent<CollectibleSys>().cristal++;
				}
				i = cristal.value.Length;
			}
		}
		chance = Random.Range(1, 101);
		for (int i = 1; i < maudite.value.Length; i++)
		{
			if (chance <= maudite.value[i].probability)
			{
				GameObject.Find("Bourse(Clone)").GetComponent<CollectibleSys>().maudite = maudite.value[i].bourse;
				if (mLootCharm)
				{
					GameObject.Find("Bourse(Clone)").GetComponent<CollectibleSys>().maudite++;
				}
				i = maudite.value[i].probability;
			}
		}

		if (healPotionCharm)
		{
			chance = Random.Range(1, 101);
			if (chance < healCharmProba) Object.Instantiate(healPotion, new Vector2(transform.position.x - .25f, transform.position.y - .25f), transform.rotation);
		}
	}
}
