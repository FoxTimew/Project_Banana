using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Explosion Amulette", menuName = "Amulette/Explosion")]
public class Amulette_Explosion : ScriptableObject
{
	public Sprite sprite;
	public readonly string GetName = "Explosion";
	public int tauxSurCent;
	public ExplosionAmulette[] level;

	[System.Serializable]
	public class ExplosionAmulette
	{
		public bool pourcent;
		public float value;
		public int prix;
		public ExplosionAmulette(float _value, bool _pourcent, int _prix)
		{
			pourcent = _pourcent;
			value = _value;
			prix = _prix;
		}
		public int GetPrix() { return prix; }
	}
}
