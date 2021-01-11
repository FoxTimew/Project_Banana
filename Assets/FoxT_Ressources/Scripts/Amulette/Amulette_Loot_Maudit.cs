using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Maudit Amulette", menuName = "Amulette/Loot Maudit")]
public class Amulette_Loot_Maudit : ScriptableObject
{
	public Sprite sprite;
	public readonly string GetName = "Loot Maudit";
	public int tauxSurCent;
	public LootMauditAmulette[] level;

	[System.Serializable]
	public class LootMauditAmulette
	{
		public bool pourcent;
		public float value;
		public int prix;
		public LootMauditAmulette(float _value, bool _pourcent, int _prix)
		{
			pourcent = _pourcent;
			value = _value;
			prix = _prix;
		}
		public int GetPrix() { return prix; }
	}
}
