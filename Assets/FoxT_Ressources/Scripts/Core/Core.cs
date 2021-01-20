using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
	public List<Collider2D> EnemyPresent = new List<Collider2D>();
	public Data data;

	int EnemyPresentnumber;
	public bool luckyCharm, healCharm, updateEnemyVision, frameUpdate;
	public float luckyCharmValue, heamCharmValue, heamCharmProbability;

	public List<GameObject> seller = new List<GameObject>();

	int[] amulettePropose = new int[9];

	private void Start()
	{
		data.gold = 0;
		AmuletteValueReset();
		StartCoroutine(Test());
	}

	public void LateUpdate()
	{
		if (updateEnemyVision)
		{
			if (frameUpdate)
			{
				EnemyPresent[EnemyPresent.Count - 1].GetComponentInChildren<EnemyVision>().pcDetected = true;
				EnemyPresent[EnemyPresent.Count - 1].GetComponentInChildren<Unit>().enabled = true;
				updateEnemyVision = false;
				frameUpdate = false;
			}
			frameUpdate = true;
		}
	}

	public void SellerStart()
	{
		foreach (GameObject obj in seller)
		{
			obj.GetComponentInParent<Seller>().RandomAwake();
		}
	}

	IEnumerator Test()
	{
		yield return new WaitForSeconds(.5f);
		SellerStart();
	}

	void AmuletteValueReset()
	{
		data.combienCombos = 0;
		data.combienDash = 0;
		data.combienDegatInflige = 0;
		data.combienExplosion = 0;
		data.combienLoot = 0;
		data.combienMaxHP = 0;
		data.combienMDegatInfligee = 0;
		data.combienMLoot = 0;
		data.combienMPoussee = 0;
		data.combienPoussee = 0;
		data.combienRefus = 0;
		data.combienResistance = 0;
		data.combienVolDeVie = 0;
		data.combienVulnerabilite = 0;
	}
}
