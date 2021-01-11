using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vulnerabilite Amulette", menuName = "Amulette/Vulnerabilite")]
public class Amulette_Vulnerabilite : ScriptableObject
{
	public Sprite sprite;
	public readonly string GetName = "Vulnerabilite";
	public int tauxSurCent;
	public VulnerabiliteAmulette[] level;

	[System.Serializable]
	public class VulnerabiliteAmulette
	{
		public bool pourcent;
		public float value;
		public int prix;
		public VulnerabiliteAmulette(float _value, bool _pourcent, int _prix)
		{
			pourcent = _pourcent;
			value = _value;
			prix = _prix;
		}

		public int GetPrix() { return prix; }
	}
}
