using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
	public Data data;

	private void Awake()
	{
		data = GameObject.Find("Data").GetComponent<Data>();
	}
}
