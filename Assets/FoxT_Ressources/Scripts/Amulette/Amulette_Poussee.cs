using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pousée Amulette", menuName = "Amulette/Poussée")]
public class Amulette_Poussee : ScriptableObject
{
	public Sprite sprite;
	public readonly string GetName = "Poussee";
	public int tauxSurCent;
	public PousseeAmulette[] level;

	[System.Serializable]
	public class PousseeAmulette
	{
		public float value;
		public int prix;
		public PousseeAmulette(float _value, int _prix)
		{
			value = _value;
			prix = _prix;
		}
		public int GetPrix() { return prix; }
	}
}
