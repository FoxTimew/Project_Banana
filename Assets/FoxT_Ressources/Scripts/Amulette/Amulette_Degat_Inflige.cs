using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Amulette/Dégât Infligé")]
public class Amulette_Degat_Inflige : ScriptableObject
{
	public Sprite sprite;
	public readonly string GetName = "Degat inflige";
	public int tauxSurCent;
	public DegatInfligeAmulette[] level;

	[System.Serializable]
	public class DegatInfligeAmulette
	{
		public float value;
		public float tempsEntreCombos;
		public int prix;
		public DegatInfligeAmulette(float _value, float _tempsEntreCombos, int _prix)
		{
			value = _value;
			tempsEntreCombos = _tempsEntreCombos;
			prix = _prix;
		}
		public int GetPrix() { return prix; }
	}
}
