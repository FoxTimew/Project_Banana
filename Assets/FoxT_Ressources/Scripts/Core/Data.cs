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
}
