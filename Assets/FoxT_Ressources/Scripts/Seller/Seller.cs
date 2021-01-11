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

	string objetChoisi;

	Core core;

	public GameObject[] enemies;

	private void OnEnable()
	{
		Debug.Log("ok");
		if (marchandMaudit) RandomAmulette(amulettesMaudit, randomValue);
		else RandomAmulette(amulettes, randomValue);
		EndReset();
		this.GetComponent<Seller>().enabled = false;
	}

	private void Awake()
	{
		prix = new int[howManyToSell];
		//Ajouter toutes les amulettes possibles
		if (marchandMaudit) randomValue = new int[5] {0,
			amulette.amuletteMDegatInfligee.tauxSurCent,
				/*amulette.amuletteMDegatInfligee.tauxSurCent + */amulette.amuletteMLoot.tauxSurCent,
					/*amulette.amuletteMDegatInfligee.tauxSurCent + amulette.amuletteMLoot.tauxSurCent + */amulette.amuletteMPoussee.tauxSurCent,
						/*amulette.amuletteMDegatInfligee.tauxSurCent + amulette.amuletteMLoot.tauxSurCent + amulette.amuletteMPoussee.tauxSurCent + */amulette.amuletteVolDeVie.tauxSurCent};
		else randomValue = new int[11] {0,
			amulette.amuletteCombos.tauxSurCent,
			/*amulette.amuletteCombos.tauxSurCent + */amulette.amuletteDash.tauxSurCent,
				/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + */amulette.amuletteDegatInfligee.tauxSurCent,
					/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + amulette.amuletteDegatInfligee.tauxSurCent + */amulette.amuletteExplosion.tauxSurCent,
						/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + amulette.amuletteDegatInfligee.tauxSurCent + amulette.amuletteExplosion.tauxSurCent + */amulette.amuletteLoot.tauxSurCent,
							/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + amulette.amuletteDegatInfligee.tauxSurCent + amulette.amuletteExplosion.tauxSurCent + amulette.amuletteLoot.tauxSurCent + */amulette.amuletteMaxHP.tauxSurCent,
								/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + amulette.amuletteDegatInfligee.tauxSurCent + amulette.amuletteExplosion.tauxSurCent + amulette.amuletteLoot.tauxSurCent + amulette.amuletteMaxHP.tauxSurCent + */amulette.amulettePoussee.tauxSurCent,
									/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + amulette.amuletteDegatInfligee.tauxSurCent + amulette.amuletteExplosion.tauxSurCent + amulette.amuletteLoot.tauxSurCent + amulette.amuletteMaxHP.tauxSurCent + amulette.amulettePoussee.tauxSurCent + */amulette.amuletteRefusDeLaMort.tauxSurCent,
										/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + amulette.amuletteDegatInfligee.tauxSurCent + amulette.amuletteExplosion.tauxSurCent + amulette.amuletteLoot.tauxSurCent + amulette.amuletteMaxHP.tauxSurCent + amulette.amulettePoussee.tauxSurCent + amulette.amuletteRefusDeLaMort.tauxSurCent + */amulette.amuletteResistance.tauxSurCent,
											/*amulette.amuletteCombos.tauxSurCent + amulette.amuletteDash.tauxSurCent + amulette.amuletteDegatInfligee.tauxSurCent + amulette.amuletteExplosion.tauxSurCent + amulette.amuletteLoot.tauxSurCent + amulette.amuletteMaxHP.tauxSurCent + amulette.amulettePoussee.tauxSurCent + amulette.amuletteRefusDeLaMort.tauxSurCent + amulette.amuletteResistance.tauxSurCent +*/ amulette.amuletteVulnerabilite.tauxSurCent};

		amulettes.Add(amulette.amuletteCombos.GetName);
		amulettes.Add(amulette.amuletteDash.GetName);
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
		for (int i = 0; i < howManyToSell; i++)
		{
			int []value = new int[_random.Count];
			for (int x = 1; x < _random.Count; x++)
			{
				value[x] = _random[x] + value[x - 1];
			}

			int nombreChoisi = Random.Range(1, value[value.Length - 1] + 1);
			for (int x = 1; x <= value.Length; x++)
			{
				if (nombreChoisi > value[x - 1] && nombreChoisi <= value[x])
				{
					aVendre.Add(list[x - 1]);
					list.Remove(list[x - 1]);
					_random.Remove(_random[x]);
					x = value.Length;
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
				prix[i] = amulette.amuletteVulnerabilite.level[core.data.niveauVulnerabilite].GetPrix();
				Debug.Log("Vulnerabilite");
				break;
			case "Combos":
				prix[i] = amulette.amuletteCombos.level[core.data.niveauCombos].GetPrix();
				Debug.Log("Combos");
				break;
			case "Dash":
				prix[i] = amulette.amuletteDash.level[core.data.niveauDash].GetPrix();
				Debug.Log("Dash");
				break;
			case "Degat inflige":
				prix[i] = amulette.amuletteDegatInfligee.level[core.data.niveauDegatInflige].GetPrix();
				Debug.Log("Degat inflige");
				break;
			case "Degat inflige maudit":
				prix[i] = amulette.amuletteMDegatInfligee.level[core.data.niveauMDegatInflige].GetPrix();
				Debug.Log("Degat inflige maudit");
				break;
			case "Explosion":
				prix[i] = amulette.amuletteExplosion.level[core.data.niveauExplosion].GetPrix();
				Debug.Log("Explosion");
				break;
			case "Loot":
				prix[i] = amulette.amuletteLoot.level[core.data.niveauLoot].GetPrix();
				Debug.Log("Loot");
				break;
			case "Loot Maudit":
				prix[i] = amulette.amuletteMLoot.level[core.data.niveauMLoot].GetPrix();
				Debug.Log("Loot Maudit");
				break;
			case "Max HP":
				prix[i] = amulette.amuletteMaxHP.level[core.data.niveauMaxHP].GetPrix();
				Debug.Log("Max HP");
				break;
			case "Poussee":
				prix[i] = amulette.amulettePoussee.level[core.data.niveauPoussee].GetPrix();
				Debug.Log("Poussee");
				break;
			case "Poussee Maudit":
				prix[i] = amulette.amuletteMPoussee.level[core.data.niveauMPoussee].GetPrix();
				Debug.Log("Poussee Maudit");
				break;
			case "Refus de la mort":
				prix[i] = amulette.amuletteRefusDeLaMort.level[core.data.niveauRefus].GetPrix();
				Debug.Log("Refus de la mort");
				break;
			case "Vol de vie":
				prix[i] = amulette.amuletteVolDeVie.level[core.data.niveauMVoldeVie].GetPrix();
				Debug.Log("Vol de vie");
				break;
			case "Resistance":
				prix[i] = amulette.amuletteResistance.level[core.data.niveauResistance].GetPrix();
				Debug.Log("Resistance");
				break;
			default:
				break;
		}
		}
	}

	void ValueUpdate()
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
				core.data.niveauDash++;

				Debug.Log("Dash");
				break;
			case "Degat inflige":
				GameObject.Find("Playable_Character").GetComponent<Attack>().BonusUpdate(amulette.amuletteDegatInfligee.level[core.data.niveauDegatInflige].value, amulette.amuletteDegatInfligee.level[core.data.niveauDegatInflige].tempsEntreCombos);
				GameObject.Find("Playable_Character").GetComponent<Attack>().charmCombosEnable = true;
				core.data.niveauDegatInflige++;

				Debug.Log("Degat inflige");
				break;
			case "Degat inflige maudit":
				core.data.niveauMDegatInflige++;

				Debug.Log("Degat inflige maudit");
				break;
			case "Explosion":
				core.data.niveauExplosion++;

				Debug.Log("Explosion");
				break;
			case "Loot":
				if (!charmActive.Contains("Loot")) charmActive.Add("Loot");
				core.data.niveauLoot++;

				Debug.Log("Loot");
				break;
			case "Loot Maudit":
				core.data.niveauMLoot++;

				Debug.Log("Loot Maudit");
				break;
			case "Max HP":
				/*float value = 0f;
				if (amulette.amuletteMaxHP.level[core.data.niveauMaxHP].pourcent)
				{
					value = amulette.amuletteMaxHP.level[core.data.niveauMaxHP].value / 100;
					GameObject.Find("Playable_Character").GetComponent<Health>().maxHealth += Mathf.RoundToInt(GameObject.Find("Playable_Character").GetComponent<Health>().maxHealth * value);
					GameObject.Find("Playable_Character").GetComponent<Health>().Heal(value, true);
				}
				else
				{
					value = amulette.amuletteMaxHP.level[core.data.niveauMaxHP].value;
					GameObject.Find("Playable_Character").GetComponent<Health>().maxHealth += Mathf.RoundToInt(value);
					GameObject.Find("Playable_Character").GetComponent<Health>().Heal(value, false);
				}*/

				core.data.niveauMaxHP++;

				Debug.Log("Max HP");
				break;
			case "Poussee":
				if (!charmActive.Contains("Poussee")) charmActive.Add("Poussee");
				core.data.niveauPoussee++;

				Debug.Log("Poussee");
				break;
			case "Poussee Maudit":

				core.data.niveauMPoussee++;

				Debug.Log("Poussee Maudit");
				break;
			case "Refus de la mort":
				GameObject.Find("Playable_Character").GetComponent<Health>().charmValue = amulette.amuletteRefusDeLaMort.level[core.data.niveauRefus].pourcentOfMaxHP / 100;
				core.data.niveauRefus++;

				Debug.Log("Refus de la mort");
				break;
			case "Vol de vie":
				core.data.niveauMVoldeVie++;

				Debug.Log("Vol de vie");
				break;
			case "Resistance":
				GameObject.Find("Playable_Character").GetComponent<Attack>().charmCausticRadius = amulette.amuletteResistance.level[core.data.niveauResistance].radius;
				GameObject.Find("Playable_Character").GetComponent<Attack>().charmCausticValue = amulette.amuletteResistance.level[core.data.niveauResistance].value;
				GameObject.Find("Playable_Character").GetComponent<Attack>().causticCharmPourcent = amulette.amuletteResistance.level[core.data.niveauResistance].pourcent;
				GameObject.Find("Playable_Character").GetComponent<Attack>().causticCharmEnable = true;

				core.data.niveauResistance++;

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
				default:
					break;
			}
		}
	}

	void EndReset()
	{
		aVendre = new List<string>();
	}
}
