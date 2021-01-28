using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seller : MonoBehaviour
{
	[SerializeField]
	Amulette amulette;

	[SerializeField]
	int howManyToSell = 3;
	//Variable ObjetProposé
	List<string> amulettes = new List<string>();
	List<string> amulettesMaudit = new List<string>();
	List<string> aVendre = new List<string>();
	List<string> charmActive = new List<string>();

	int[] randomValue, prix;

	[SerializeField]
	bool marchandMaudit = false;

	public string objetChoisi;

	Core core;

	public GameObject[] enemies;

	[SerializeField]
	GameObject[] emplacements;

	/*private void OnEnable()
	{
		EndReset();
		this.GetComponent<Seller>().enabled = false;
	}*/

	private void Awake()
	{
		amulette.amuletteCombos.tauxSurCent = 10;
		amulette.amuletteDegatInfligee.tauxSurCent = 10;
		amulette.amuletteExplosion.tauxSurCent = 10;
		amulette.amuletteLoot.tauxSurCent = 10;
		amulette.amuletteMaxHP.tauxSurCent = 10;
		amulette.amulettePoussee.tauxSurCent = 10;
		amulette.amuletteRefusDeLaMort.tauxSurCent = 10;
		amulette.amuletteResistance.tauxSurCent = 10;
		amulette.amuletteVulnerabilite.tauxSurCent = 10;
	}

	public void RandomAwake()
	{
		prix = new int[howManyToSell];
		//Ajouter toutes les amulettes possibles
		if (marchandMaudit) randomValue = new int[5] {0,
			amulette.amuletteMDegatInfligee.tauxSurCent,
				/*amulette.amuletteMDegatInfligee.tauxSurCent + */amulette.amuletteMLoot.tauxSurCent,
					/*amulette.amuletteMDegatInfligee.tauxSurCent + amulette.amuletteMLoot.tauxSurCent + */amulette.amuletteMPoussee.tauxSurCent,
						/*amulette.amuletteMDegatInfligee.tauxSurCent + amulette.amuletteMLoot.tauxSurCent + amulette.amuletteMPoussee.tauxSurCent + */amulette.amuletteVolDeVie.tauxSurCent};
		else randomValue = new int[10] {0,
			amulette.amuletteCombos.tauxSurCent,
				/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + */amulette.amuletteDegatInfligee.tauxSurCent,
					/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + amulette.amuletteDegatInfligee.tauxSurCent + */amulette.amuletteExplosion.tauxSurCent,
						/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + amulette.amuletteDegatInfligee.tauxSurCent + amulette.amuletteExplosion.tauxSurCent + */amulette.amuletteLoot.tauxSurCent,
							/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + amulette.amuletteDegatInfligee.tauxSurCent + amulette.amuletteExplosion.tauxSurCent + amulette.amuletteLoot.tauxSurCent + */amulette.amuletteMaxHP.tauxSurCent,
								/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + amulette.amuletteDegatInfligee.tauxSurCent + amulette.amuletteExplosion.tauxSurCent + amulette.amuletteLoot.tauxSurCent + amulette.amuletteMaxHP.tauxSurCent + */amulette.amulettePoussee.tauxSurCent,
									/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + amulette.amuletteDegatInfligee.tauxSurCent + amulette.amuletteExplosion.tauxSurCent + amulette.amuletteLoot.tauxSurCent + amulette.amuletteMaxHP.tauxSurCent + amulette.amulettePoussee.tauxSurCent + */amulette.amuletteRefusDeLaMort.tauxSurCent,
										/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + amulette.amuletteDegatInfligee.tauxSurCent + amulette.amuletteExplosion.tauxSurCent + amulette.amuletteLoot.tauxSurCent + amulette.amuletteMaxHP.tauxSurCent + amulette.amulettePoussee.tauxSurCent + amulette.amuletteRefusDeLaMort.tauxSurCent + */amulette.amuletteResistance.tauxSurCent,
											/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + amulette.amuletteDegatInfligee.tauxSurCent + amulette.amuletteExplosion.tauxSurCent + amulette.amuletteLoot.tauxSurCent + amulette.amuletteMaxHP.tauxSurCent + amulette.amulettePoussee.tauxSurCent + amulette.amuletteRefusDeLaMort.tauxSurCent + amulette.amuletteResistance.tauxSurCent +*/ amulette.amuletteVulnerabilite.tauxSurCent};

		amulettes.Add(amulette.amuletteCombos.GetName);
		amulettes.Add(amulette.amuletteDegatInfligee.GetName);
		amulettes.Add(amulette.amuletteExplosion.GetName);
		amulettes.Add(amulette.amuletteLoot.GetName);
		amulettes.Add(amulette.amuletteMaxHP.GetName);
		amulettesMaudit.Add(amulette.amuletteMDegatInfligee.GetName);
		amulettesMaudit.Add(amulette.amuletteMLoot.GetName);
		amulettesMaudit.Add(amulette.amuletteMPoussee.GetName);
		amulettes.Add(amulette.amulettePoussee.GetName);
		amulettes.Add(amulette.amuletteRefusDeLaMort.GetName);
		amulettes.Add(amulette.amuletteResistance.GetName);
		amulettesMaudit.Add(amulette.amuletteVolDeVie.GetName);
		amulettes.Add(amulette.amuletteVulnerabilite.GetName);

		core = GameObject.Find("Core").GetComponent<Core>();

		if (marchandMaudit) RandomAmulette(amulettesMaudit, randomValue);
		else RandomAmulette(amulettes, randomValue);
	}

	//Selection des items au hasards selon leurs taux d'apparission
	void RandomAmulette(List<string> _list, int[] random)
	{
		List<string> list = new List<string>();
		foreach (string t in _list)
		{
			list.Add(t);
		}
		List<int> _random = new List<int>();
		foreach (int i in random)
		{
			_random.Add(i);
		}
		for (int i = list.Count - 1; i > -1; i--)
		{
			if (_random[i + 1] == 0)
			{
				Debug.Log(i);
				list.Remove(list[i]);
				_random.Remove(_random[i+1]);
			}
		}
		if (list.Count <= 3 && list.Count > 0)
		{
			foreach (string name in list)
			{
				aVendre.Add(name);
			}
		}
		else if (list.Count == 0)
		{
			return;
		}
		else
		{
			for (int i = 0; i < howManyToSell; i++)
			{
				int[] value = new int[_random.Count];
				for (int x = 1; x < _random.Count; x++)
				{
					value[x] = _random[x] + value[x - 1];
				}

				int nombreChoisi = Random.Range(1, value[value.Length - 1] + 1);
				for (int x = 1; x < value.Length; x++)
				{
					if (nombreChoisi <= value[x]  /*&& nombreChoisi <= value[x]*/)
					{
						aVendre.Add(list[x - 1]);
						list.Remove(list[x - 1]);
						_random.Remove(_random[x]);
						x = value.Length;
					}
				}
			}
		}
		PrixUpdate();
	}

	//Affiche dans les sprites aux endroits prévue

	//Action en cas d'achat
	public void Purchase(int number)
	{
		objetChoisi = aVendre[number];
		if (core.data.gold > prix[number])
		{
			core.data.gold -= prix[number];
			ValueUpdate();
		}
		//Sinon Animation refus
	}
	//Update les valeurs des bonus et l'inventaire

	void PrixUpdate()
	{
		for (int i = 0; i < aVendre.Count; i++)
		{ 
			switch (aVendre[i])
		{
			case "Vulnerabilite":
				//emplacements[i].GetComponent<SpriteRenderer>().sprite = amulette.amuletteVulnerabilite.sprite;
				core.data.combienVulnerabilite++;
				prix[core.data.niveauVulnerabilite] = amulette.amuletteVulnerabilite.level[core.data.niveauVulnerabilite].GetPrix();
				Debug.Log("Vulnerabilite");
				break;
			case "Combos":
				//emplacements[i].GetComponent<SpriteRenderer>().sprite = amulette.amuletteCombos.sprite;
				core.data.combienCombos++;
				prix[core.data.niveauCombos] = amulette.amuletteCombos.level[core.data.niveauCombos].GetPrix();
				Debug.Log("Combos");
				break;
			case "Degat inflige":
				//emplacements[i].GetComponent<SpriteRenderer>().sprite = amulette.amuletteDegatInfligee.sprite;
				core.data.combienDegatInflige++;
				prix[core.data.niveauDegatInflige] = amulette.amuletteDegatInfligee.level[core.data.niveauDegatInflige].GetPrix();
				Debug.Log("Degat inflige");
				break;
			case "Degat inflige maudit":
				//emplacements[i].GetComponent<SpriteRenderer>().sprite = amulette.amuletteMDegatInfligee.sprite;
				core.data.combienMDegatInfligee++;
				prix[core.data.niveauMDegatInflige] = amulette.amuletteMDegatInfligee.level[core.data.niveauMDegatInflige].GetPrix();
				Debug.Log("Degat inflige maudit");
				break;
			case "Explosion":
				//emplacements[i].GetComponent<SpriteRenderer>().sprite = amulette.amuletteExplosion.sprite;
				core.data.combienExplosion++;
				prix[core.data.niveauExplosion] = amulette.amuletteExplosion.level[core.data.niveauExplosion].GetPrix();
				Debug.Log("Explosion");
				break;
			case "Loot":
				//emplacements[i].GetComponent<SpriteRenderer>().sprite = amulette.amuletteLoot.sprite;
				core.data.combienLoot++;
				prix[core.data.niveauLoot] = amulette.amuletteLoot.level[core.data.niveauLoot].GetPrix();
				Debug.Log("Loot");
				break;
			case "Loot Maudit":
				//emplacements[i].GetComponent<SpriteRenderer>().sprite = amulette.amuletteMLoot.sprite;
				core.data.combienMLoot++;
				prix[core.data.niveauMLoot] = amulette.amuletteMLoot.level[core.data.niveauMLoot].GetPrix();
				Debug.Log("Loot Maudit");
				break;
			case "Max HP":
				//emplacements[i].GetComponent<SpriteRenderer>().sprite = amulette.amuletteMaxHP.sprite;
				core.data.combienMaxHP++;
				prix[core.data.niveauMaxHP] = amulette.amuletteMaxHP.level[core.data.niveauMaxHP].GetPrix();
				Debug.Log("Max HP");
				break;
			case "Poussee":
				//emplacements[i].GetComponent<SpriteRenderer>().sprite = amulette.amulettePoussee.sprite;
				core.data.combienPoussee++;
				prix[core.data.niveauPoussee] = amulette.amulettePoussee.level[core.data.niveauPoussee].GetPrix();
				Debug.Log("Poussee");
				break;
			case "Poussee Maudit":
				//emplacements[i].GetComponent<SpriteRenderer>().sprite = amulette.amuletteMPoussee.sprite;
				core.data.combienMPoussee++;
				prix[core.data.niveauMPoussee] = amulette.amuletteMPoussee.level[core.data.niveauMPoussee].GetPrix();
				Debug.Log("Poussee Maudit");
				break;
			case "Refus de la mort":
				//emplacements[i].GetComponent<SpriteRenderer>().sprite = amulette.amuletteRefusDeLaMort.sprite;
				core.data.combienRefus++;
				prix[core.data.niveauRefus] = amulette.amuletteRefusDeLaMort.level[core.data.niveauRefus].GetPrix();
				Debug.Log("Refus de la mort");
				break;
			case "Vol de vie":
				//emplacements[i].GetComponent<SpriteRenderer>().sprite = amulette.amuletteVolDeVie.sprite;
				core.data.combienVolDeVie++;
				prix[core.data.niveauMVoldeVie] = amulette.amuletteVolDeVie.level[core.data.niveauMVoldeVie].GetPrix();
				Debug.Log("Vol de vie");
				break;
			case "Resistance":
				//emplacements[i].GetComponent<SpriteRenderer>().sprite = amulette.amuletteResistance.sprite;
				core.data.combienResistance++;
				prix[core.data.niveauResistance] = amulette.amuletteResistance.level[core.data.niveauResistance].GetPrix();
				Debug.Log("Resistance");
				break;
			default:
				break;
			}
			END();
		}
	}

	public void ValueUpdate()
	{
		switch (objetChoisi)
		{
			case "Vulnerabilite":
				GameObject.Find("Playable_Character").GetComponent<Attack>().combosCharmEnable = true;
				GameObject.Find("Playable_Character").GetComponent<Attack>().charmCombosDamage = amulette.amuletteCombos.level[core.data.niveauCombos].value;
				core.data.niveauVulnerabilite++;

				Debug.Log("Vulnerabilite");
				break;
			case "Combos":
				core.data.niveauCombos++;

				Debug.Log("Combos");
				break;
			case "Dash":
				core.luckyCharm = true;
				core.luckyCharmValue = amulette.amuletteDash.level[core.data.niveauDash].value / 100; 
				core.data.niveauDash++;

				Debug.Log("Dash");
				break;
			case "Degat inflige":
				GameObject.Find("Playable_Character").GetComponent<Attack>().BonusUpdate(10);
				GameObject.Find("Playable_Character").GetComponent<Attack>().charmCombosEnable = true;

				Debug.Log("Degat inflige");
				break;
			case "Degat inflige maudit":
				GameObject.Find("Playable_Character").GetComponent<Health>().berzerkCharm = true;
				GameObject.Find("Playable_Character").GetComponent<Health>().BerzerkUpdate();

				Debug.Log("Degat inflige maudit");
				break;
			case "Explosion":
				if (!charmActive.Contains("Explosion")) charmActive.Add("Explosion");

				Debug.Log("Explosion");
				break;
			case "Loot":
				if (!charmActive.Contains("Loot")) charmActive.Add("Loot");

				Debug.Log("Loot");
				break;
			case "Loot Maudit":
				if (!charmActive.Contains("Loot Maudit")) charmActive.Add("Loot Maudit");
				GameObject.Find("Playable_Character").GetComponent<Health>().mLootCharm = true;

				Debug.Log("Loot Maudit");
				break;
			case "Max HP":
				GameObject.Find("Playable_Character").GetComponent<Health>().Heal(50, true);

				Debug.Log("Max HP");
				break;
			case "Poussee":
				if (!charmActive.Contains("Poussee")) charmActive.Add("Poussee");
				core.data.niveauPoussee++;

				Debug.Log("Poussee");
				break;
			case "Poussee Maudit":
				if (!charmActive.Contains("Poussee Maudit")) charmActive.Add("Poussee Maudit");
				GameObject.Find("Playable_Character").GetComponent<Attack>().mCharmPousseeValue = amulette.amuletteMPoussee.level[core.data.niveauMPoussee].valueJoueurPourcent;
				core.data.niveauMPoussee++;

				Debug.Log("Poussee Maudit");
				break;
			case "Refus de la mort":
				GameObject.Find("Playable_Character").GetComponent<Health>().charmValue = amulette.amuletteRefusDeLaMort.level[core.data.niveauRefus].pourcentOfMaxHP / 100;
				GameObject.Find("Playable_Character").GetComponent<Health>().RefusDeLaMort = true;
				core.data.niveauRefus++;

				Debug.Log("Refus de la mort");
				break;
			case "Vol de vie":
				GameObject.Find("Playable_Character").GetComponent<Attack>().volDeVieValue = amulette.amuletteVolDeVie.level[core.data.niveauMVoldeVie].value;
				GameObject.Find("Playable_Character").GetComponent<Attack>().volDeVieCharm = true;

				Debug.Log("Vol de vie");
				break;
			case "Resistance":
				GameObject.Find("Playable_Character").GetComponent<Health>().resistanceBonus = 5;

				Debug.Log("Resistance");
				break;
			default:
				break;
		}
	}

	void EnemiesUpdtae()
	{
		foreach (string charm in charmActive)
		{
			switch (charm)
			{
				case "Poussee":
					foreach (GameObject obj in enemies)
					{
						obj.GetComponent<Ejecting>().pousseeCharmEnable = true;
						obj.GetComponent<Ejecting>().damage = amulette.amulettePoussee.level[core.data.niveauPoussee - 1].value;
					}
					break;
				case "Loot":
					foreach (GameObject obj in enemies)
					{
						obj.GetComponent<EnnemyLoot>().value = Mathf.RoundToInt(amulette.amuletteLoot.level[core.data.niveauLoot - 1].value);
						obj.GetComponent<EnnemyLoot>().charmLoot = true;
					}
					break;
				case "Explosion":
					foreach (GameObject obj in enemies)
					{
						obj.GetComponent<EnemySys>().EnemyExplosionCharm = true;
						obj.GetComponent<EnemySys>().explosionCharmDamage = Mathf.RoundToInt(amulette.amuletteExplosion.level[core.data.niveauExplosion - 1].value);
					}
					break;
				case "Poussee Maudit":
					foreach (GameObject obj in enemies)
					{
						//AjouterPoussee
					}
					break;
				case "Loot Maudit":
					foreach (GameObject obj in enemies)
					{
						obj.GetComponent<EnnemyLoot>().mLootCharm = true;
					}
					break;
				default:
					break;
			}
		}
	}

	void EndReset()
	{
		aVendre = new List<string>();
	}

	void END()
	{
		if (core.data.combienCombos >= 3) amulette.amuletteCombos.tauxSurCent = 0;
		if (core.data.combienDegatInflige >= 3) amulette.amuletteDegatInfligee.tauxSurCent = 0;
		if (core.data.combienExplosion >= 3) amulette.amuletteExplosion.tauxSurCent = 0;
		if (core.data.combienLoot >= 3) amulette.amuletteLoot.tauxSurCent = 0;
		if (core.data.combienMaxHP >= 3) amulette.amuletteMaxHP.tauxSurCent = 0;
		if (core.data.combienPoussee >= 3) amulette.amulettePoussee.tauxSurCent = 0;
		if (core.data.combienRefus >= 3) amulette.amuletteRefusDeLaMort.tauxSurCent = 0;
		if (core.data.combienResistance >= 3) amulette.amuletteResistance.tauxSurCent = 0;
		if (core.data.combienVulnerabilite >= 3) amulette.amuletteVulnerabilite.tauxSurCent = 0;
	}
}
