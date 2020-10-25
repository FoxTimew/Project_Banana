using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponsInfos : ScriptableObject
{
	public weapon weapon;

	public int[] damageCombos;

	public float[] pousseeCombos;

	public int damageSpecial;

	public float pousseeSpecial;

	public float range;
}
