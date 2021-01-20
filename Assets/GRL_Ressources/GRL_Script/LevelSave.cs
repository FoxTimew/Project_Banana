using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new upgrade save", menuName ="upgrade")]
public class LevelSave : ScriptableObject
{
	public int niveauVie, damage, resistance, prix;
}
