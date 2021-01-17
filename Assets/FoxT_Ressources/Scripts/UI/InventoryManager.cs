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
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			MoneyUpdateAnimation(1, 0, 0);
		}
	}
	public void InventoryUpdate()
	{
		if (howManyObject == cadre.Length - 1) return;
		howManyObject++;
		cadre[howManyObject].GetComponentInChildren<SpriteRenderer>().sprite = sprite[howManyObject];
		cadre[howManyObject].SetActive(true);
	}

	public void MoneyUpdateAnimation(int gold, int cristal, int maudite)
	{
		//jouer animation
		if (gold != 0 || cristal != 0 || maudite != 0)
		{
			text[0].text = data.gold.ToString();
			text[1].text = data.cristal.ToString();
			text[2].text = data.meaudite.ToString();

			anim.SetFloat("direction", 1);
			anim.speed = 1;
			StopAllCoroutines();
			coroutine = StartCoroutine(animationUpdate());
		}
	}

	public void HealthDisplay(int health)
	{
		text[3].text = health.ToString();
	}

	IEnumerator animationUpdate()
	{
		yield return new WaitForSeconds(2);
		anim.SetFloat("direction", -1);
		anim.speed = 1;
	}
}
