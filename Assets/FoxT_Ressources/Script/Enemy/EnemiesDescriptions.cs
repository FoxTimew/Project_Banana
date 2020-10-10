using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class EnemiesDescriptions : ScriptableObject
{
	public struct EnemyDesciption
	{
		public Enemies enemy;
		public int hp;
		public float attack;
		public float poussee;
		public float resistance;
		public bool armor;
		public float hpArmor;
	}


}
