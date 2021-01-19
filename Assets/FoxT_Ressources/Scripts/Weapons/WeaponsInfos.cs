using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponsInfos : ScriptableObject
{
	public weapon weapon;

	public int[] damageCombos;

	public AnimationCurve[] pousseeCombos;

	public float[] stunTime;

	public int damageSpecial;

	public AnimationCurve pousseeSpecial;

	public float range;

	public float vitesse;

	public float annulerCombosTemps;

	public float poids;
}
