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
		public float valueJoueurPourcent;
		public float valueEnemiPourcent;
		public int prix;
		public PousseeMauditAmulette(float _valueJoueur, float _valueEnemi, int _prix)
		{
			valueJoueurPourcent = _valueJoueur;
			valueEnemiPourcent = _valueEnemi;
			prix = _prix;
		}
		public int GetPrix() { return prix; }
	}
}
