using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Save Data File", menuName = "Data/Save Data")]
public class Data : ScriptableObject
{
	public int gold;

	public int cristal;

	public int meaudite;

	public readonly int initialMaxHealth = 5;

	//Amulette possédé
	public int niveauCombos;
	public int niveauDash;
	public int niveauDegatInflige;
	public int niveauMDegatInflige;
	public int niveauExplosion;
	public int niveauLoot;
	public int niveauMLoot;
	public int niveauMaxHP;
	public int niveauPoussee;
	public int niveauMPoussee;
	public int niveauRefus;
	public int niveauResistance;
	public int niveauMVoldeVie;
	public int niveauVulnerabilite;

	public int combienCombos;
	public int combienDash;
	public int combienDegatInflige;
	public int combienMDegatInfligee;
	public int combienExplosion;
	public int combienLoot;
	public int combienMLoot;
	public int combienMaxHP;
	public int combienPoussee;
	public int combienMPoussee;
	public int combienRefus;
	public int combienResistance;
	public int combienVolDeVie;
	public int combienVulnerabilite;
}
