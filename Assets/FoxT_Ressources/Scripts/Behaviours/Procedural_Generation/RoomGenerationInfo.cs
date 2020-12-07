using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room", menuName = "Room")]
public class RoomGenerationInfo : ScriptableObject
{
	public GameObject[] start;

	public GameObject[] enemy;
	
	public GameObject[] seller;
	
	public GameObject[] blackSeller;
	
	public GameObject[] chest;
	
	public GameObject[] challenge;
	
	public GameObject[] boss;
}
