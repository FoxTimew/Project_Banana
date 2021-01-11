﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Degat Maudit Amulette", menuName = "Amulette/Dégât Infligé Maudit")]
public class Amulette_Degat_Inflige_Maudit : ScriptableObject
{
	public Sprite sprite;
	public readonly string GetName = "Degat inflige maudit";
	public int tauxSurCent;
	public DegatMauditInfligeAmulette[] level;

	[System.Serializable]
	public class DegatMauditInfligeAmulette
	{
		public bool pourcent;
		public float value;
		public int prix;
		public DegatMauditInfligeAmulette(float _value, bool _pourcent, int _prix)
		{
			pourcent = _pourcent;
			value = _value;
			prix = _prix;
		}
		public int GetPrix() { return prix; }
	}
}
