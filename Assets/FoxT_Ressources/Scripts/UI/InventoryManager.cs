using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
	[SerializeField]
	GameObject[] cadre;

	[SerializeField]
	Animator anim;

	[SerializeField]
	string stateName;

	[SerializeField]
	Data data;

	[SerializeField]
	Text[] text;

	public Sprite[] sprite;

	int howManyObject = -1;

	Coroutine coroutine;
	private void Awake()
	{
		foreach (GameObject obj in cadre)
		{
			obj.SetActive(false);
		}
		MoneyUpdateAnimation();
	}

	public void InventoryUpdate()
	{
		if (howManyObject == cadre.Length - 1) return;
		howManyObject++;
		cadre[howManyObject].GetComponentInChildren<SpriteRenderer>().sprite = sprite[howManyObject];
		cadre[howManyObject].SetActive(true);
	}

	public void MoneyUpdateAnimation()
	{
		text[0].text = data.gold.ToString();
		text[1].text = data.cristal.ToString();
	}

	public void HealthDisplay(int health)
	{
		text[2].text = health.ToString();
	}
}
