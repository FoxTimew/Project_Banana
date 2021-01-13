using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Max HP Amulette", menuName = "Amulette/Max HP")]
public class Amulette_Max_HP : ScriptableObject
{
	public Sprite sprite;
	public readonly string GetName = "Max HP";
	public int tauxSurCent;
	public Max_HP_Amulette[] level;

	[System.Serializable]
	public class Max_HP_Amulette
	{
		public float pourcent;
		public float value;
		public int prix;
		public Max_HP_Amulette(float _value, float _pourcent, int _prix)
		{
			pourcent = _pourcent;
			value = _value;
			prix = _prix;
		}
		public int GetPrix() { return prix; }
	}
}
