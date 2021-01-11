using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combos Amulette", menuName = "Amulette/Combos")]
public class Amulette_Combos : ScriptableObject
{
	public Sprite sprite;
	public readonly string GetName = "Combos";
	public int tauxSurCent;
	public CombosAmulette[] level;

	[System.Serializable]
	public class CombosAmulette
	{
		public float value;
		public int prix;
		public CombosAmulette(float _value, int _prix)
		{
			value = _value;
			prix = _prix;
		}
		public int GetPrix() { return prix; }
	}
}
