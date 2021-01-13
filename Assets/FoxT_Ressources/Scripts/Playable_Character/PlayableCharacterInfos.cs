using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Playable Character" ,menuName = "Playable Character")]
public class PlayableCharacterInfos : ScriptableObject
{
	public int HP;

	public int resitance;

	public int force;

	public float pousseeForce;
}
