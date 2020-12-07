using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class LevelGenerationInfo : ScriptableObject
{
	public int distanceBossMin;

	public int distanceBossMax;
	
	public int nombreDeSalleMin;
	
	public int nombreDeSalleMax;
	
	public int nombreCoffreMin;
	
	public int nombreCoffreMax;
	
	public int nombreMarchandMin;
	
	public int nombreMarchandMax;
	
	public int nombreMarchandMauditMin;
	
	public int nombreMarchandMauditMax;
	
	public int nombreDeSalleDeDefiOutBossMin;
	
	public int nombreDeSalleDeDefiOutBossMax;

	public int nombreDeSalleDeDefiForBossMin;

	public int nombreDeSalleDeDefiForBossMax;
}
