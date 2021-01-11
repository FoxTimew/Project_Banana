using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Amulette", menuName = "Amulette/Loot")]
public class Amulette_Loot : ScriptableObject
{
	public Sprite sprite;
	public readonly string GetName = "Loot";
	public int tauxSurCent;
	public LootAmulette[] level;

	[System.Serializable]
	public class LootAmulette
	{
		public bool pourcent;
		public float value;
		public int prix;
		public LootAmulette(float _value, bool _pourcent, int _prix)
		{
			pourcent = _pourcent;
			value = _value;
			prix = _prix;
		}
		public int GetPrix() { return prix; }
	}
}
