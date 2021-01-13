using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Amulette Data", menuName = "Data/Amulette Data")]
public class Amulette : ScriptableObject
{
	public Amulette_Combos amuletteCombos;
	public Amulette_Dash amuletteDash;
	public Amulette_Degat_Inflige amuletteDegatInfligee;
	public Amulette_Degat_Inflige_Maudit amuletteMDegatInfligee;
	public Amulette_Explosion amuletteExplosion;
	public Amulette_Loot amuletteLoot;
	public Amulette_Max_HP amuletteMaxHP;
	public Amulette_Poussee amulettePoussee;
	public Amulette_Refus_de_mort amuletteRefusDeLaMort;
	public Amulette_Resistance amuletteResistance;
	public Amulette_VolDeVie amuletteVolDeVie;
	public Amulette_Vulnerabilite amuletteVulnerabilite;
	public Amulette_Poussee_Maudit amuletteMPoussee;
	public Amulette_Loot_Maudit amuletteMLoot;
}
