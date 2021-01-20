using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMode : MonoBehaviour
{
	Core core;
	Health playerHealth;
	Attack playerAttack;

	int initialForce;

	private void Awake()
	{
		playerHealth = GameObject.Find("Playable_Character").GetComponent<Health>();
		playerAttack = GameObject.Find("Playable_Character").GetComponent<Attack>();
		core = GameObject.Find("Core").GetComponent<Core>();
	}

	public void GodeMode()
	{
		if (!playerHealth.godeMode) playerHealth.godeMode = true;
		else playerHealth.godeMode = false;
		gameObject.SetActive(false);
		GameObject.Find("Playable_Character").GetComponent<Controler>().isDebuging = false;
		Time.timeScale = 1;
	}

	public void OneShot()
	{
		if (initialForce == 0)
		{
			initialForce = playerAttack.damageCombos[0];
			playerAttack.damageCombos[0] = 99999;
		}
		else
		{
			playerAttack.damageCombos[0] = initialForce;
			initialForce = 0;
		}
		gameObject.SetActive(false);
		GameObject.Find("Playable_Character").GetComponent<Controler>().isDebuging = false;
		Time.timeScale = 1;
	}

	public void UnlockTP()
	{
		GameObject[] obj = GameObject.FindGameObjectsWithTag("Teleporteur");
		foreach (GameObject ob in obj)
		{
			if (ob.activeSelf)
			{
				Debug.Log("is active + " + ob.name);
				ob.GetComponent<Teleporteur>().isActive = true;
				ob.GetComponent<Teleporteur>().StartAnimation();
			}
		}
		gameObject.SetActive(false);
		GameObject.Find("Playable_Character").GetComponent<Controler>().isDebuging = false;
		Time.timeScale = 1;
	}

	public void Money()
	{
		core.data.gold = 9999;
		GetComponentInParent<InventoryManager>().MoneyUpdateAnimation();
		gameObject.SetActive(false);
		GameObject.Find("Playable_Character").GetComponent<Controler>().isDebuging = false;
		Time.timeScale = 1;
	}

	public void Cristal()
	{
		core.data.cristal = 9999;
		GetComponentInParent<InventoryManager>().MoneyUpdateAnimation();
		gameObject.SetActive(false);
		GameObject.Find("Playable_Character").GetComponent<Controler>().isDebuging = false;
		Time.timeScale = 1;
	}

	public void Maudite()
	{
		core.data.meaudite = 9999;
		GetComponentInParent<InventoryManager>().MoneyUpdateAnimation();
		gameObject.SetActive(false);
		GameObject.Find("Playable_Character").GetComponent<Controler>().isDebuging = false;
		Time.timeScale = 1;
	}
}
