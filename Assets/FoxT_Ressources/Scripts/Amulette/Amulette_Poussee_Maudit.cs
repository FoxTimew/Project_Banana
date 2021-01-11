using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Poussee Maudit Amulette", menuName = "Amulette/Poussee Maudit")]
public class Amulette_Poussee_Maudit : ScriptableObject
{
	public Sprite sprite;
	public readonly string GetName = "Poussee Maudit";
	public int tauxSurCent;
	public PousseeMauditAmulette[] level;

	[System.Serializable]
	public class PousseeMauditAmulette
	{
		public bool pourcent;
		public float value;
		public int prix;
		public PousseeMauditAmulette(float _value, bool _pourcent, int _prix)
		{
			pourcent = _pourcent;
			value = _value;
			prix = _prix;
		}
		public int GetPrix() { return prix; }
	}
}
