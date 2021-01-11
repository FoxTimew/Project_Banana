using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dash Amulette", menuName = "Amulette/Dash")]
public class Amulette_Dash : ScriptableObject
{
	public Sprite sprite;
	public readonly string GetName = "Dash";
	public int tauxSurCent;
	public DashAmulette[] level;

	[System.Serializable]
	public class DashAmulette
	{
		public bool pourcent;
		public float value;
		public int prix;
		public DashAmulette(float _value, bool _pourcent, int _prix)
		{
			pourcent = _pourcent;
			value = _value;
			prix = _prix;
		}
		public int GetPrix() { return prix; }
	}
}
