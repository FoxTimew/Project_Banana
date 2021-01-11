using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vol De Vie Amulette", menuName = "Amulette/Vol de vie")]
public class Amulette_VolDeVie : ScriptableObject
{
	public Sprite sprite;
	public readonly string GetName = "Vol de vie";
	public int tauxSurCent;
	public VolDeVieAmulette[] level;

	[System.Serializable]
	public class VolDeVieAmulette
	{
		public bool pourcent;
		public float value;
		public int prix;
		public VolDeVieAmulette(float _value, bool _pourcent, int _prix)
		{
			pourcent = _pourcent;
			value = _value;
			prix = _prix;
		}
		public int GetPrix() { return prix; }
	}
}
