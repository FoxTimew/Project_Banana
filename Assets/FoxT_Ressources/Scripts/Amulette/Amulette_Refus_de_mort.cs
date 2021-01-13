using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Refus De La Mort", menuName = "Amulette/Refus de la mort")]
public class Amulette_Refus_de_mort : ScriptableObject
{
	public Sprite sprite;
	public readonly string GetName = "Refus de la mort";
	public int tauxSurCent;
	public RefusDeLaMort[] level;

	[System.Serializable]
	public class RefusDeLaMort
	{
		public float pourcentOfMaxHP;
		public int prix;
		public RefusDeLaMort(float pourcent, int _prix)
			
		{
			pourcentOfMaxHP = pourcent;
			prix = _prix;
		}
		public int GetPrix() { return prix; }
	}
}
