﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomDetector : MonoBehaviour
{
	[SerializeField]
	Core core;
	bool playerIn;

	[SerializeField]
	List<GameObject> teleporteur = new List<GameObject>();

	private void Awake()
	{
		core = GameObject.Find("Core").GetComponent<Core>();
	}

	List<Collider2D> enemyInRoom = new List<Collider2D>();
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			playerIn = true;
			core.EnemyPresent = enemyInRoom;
		}
		else if(collision.gameObject.tag == "Enemy")
		{
			enemyInRoom.Add(collision);
			if (playerIn)
			{
				core.EnemyPresent = new List<Collider2D>();
				foreach (Collider2D obj in enemyInRoom)
				{
					core.EnemyPresent.Add(obj);

				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			playerIn = false;
		}
		else if(collision.gameObject.tag == "Enemy")
		{
			enemyInRoom.Remove(collision);
			core.EnemyPresent = enemyInRoom;
		}
		if (enemyInRoom.Count == 0)
		{
			UnlockDoor();
		}
	}

	void UnlockDoor()
	{
		if (!GetComponent<SellerDetector>().playerIsHere) return;
		Debug.Log("Teleporteur");
		foreach (GameObject obj in teleporteur)
		{
			if (obj != null)
			{
				obj.GetComponent<Teleporteur>().isActive = true;
				obj.GetComponent<Teleporteur>().StartAnimation();
			}
		}
	}
}
