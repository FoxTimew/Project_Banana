using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
	public enemies type;
}

public enum enemies
{ 
	Guerrier,
	Dasher,
	Golem,
	Soufleur,
	Kamikaze,
	Armor_Guardian,
	Guardian,
	Spawner
}
