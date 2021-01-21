using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmuletteDescription : MonoBehaviour
{
	Animator anim;
	Text textPrice;

	bool playerPresent;

	GameObject obj;

	public int salle;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			DesciptionUpDate();
			// lancer animation
			anim.SetFloat("direction", 1);
			anim.speed = 1;
			playerPresent = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			// stop animation
			anim.SetFloat("direction", -1);
			anim.speed = 1;
			playerPresent = false;
			//obj.SetActive(false);
		}
	}

	private void Update()
	{
		if (!playerPresent) return;
		if (!GameObject.Find("Playable_Character").GetComponent<Controler>().isInteracting) return;
	}

	private void LateUpdate()
	{
		if (obj == null) return;
		if (obj.GetComponent<InventoryUIManager>().Buying)
		{
			playerPresent = false;
			this.GetComponent<SpriteRenderer>().enabled = false;
			anim.SetFloat("direction", -1);
			Destroy(this.gameObject);
		}
	}

	void DesciptionUpDate()
	{
		if (salle == 1) obj = GameObject.Find("DescriptionDegatInflige");
		else if (salle == 2) obj = GameObject.Find("DescriptionMaxHP");
		else if (salle == 3) obj = GameObject.Find("DescriptionRefusDeLaMort");
		else if (salle == 4) obj = GameObject.Find("DescriptionResistance");
		else if (salle == 5) obj = GameObject.Find("DescriptionMDegatInflige");

		obj.GetComponent<InventoryUIManager>().seller = this.GetComponentInParent<Seller>();
		anim = obj.GetComponent<Animator>();
	}

}
