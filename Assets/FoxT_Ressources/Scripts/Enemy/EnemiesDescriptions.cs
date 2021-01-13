using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]

public class EnemiesDescriptions : ScriptableObject
{
	public enemies enemy;

	public Sprite sprite;

	public int hp;

	public int attack;

	public float range;

	public AnimationCurve poussee;

	public int resistance;

	public int hpArmor;

	public float[] stunReduction;

	public float vitesseDisplacement;

	public float vitesseAttack;
}
