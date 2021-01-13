using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resistance Amulette", menuName = "Amulette/Resistance")]
public class Amulette_Resistance : ScriptableObject
{
	public Sprite sprite;
	public readonly string GetName = "Resistance";
	public int tauxSurCent;
	public ResistanceAmulette[] level;

	[System.Serializable]
	public class ResistanceAmulette
	{
		public bool pourcent;
		public float value;
		public float radius;
		public int prix;
		public ResistanceAmulette(float _value, float _radius, bool _pourcent, int _prix)
		{
			pourcent = _pourcent;
			value = _value;
			radius = _radius;
			prix = _prix;
		}
		public int GetPrix() { return prix; }
	}
}
