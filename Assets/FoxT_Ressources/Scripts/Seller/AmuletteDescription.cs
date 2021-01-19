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

		BuyStart();
	}

	void DesciptionUpDate()
	{
		if (this.GetComponent<SpriteRenderer>().sprite.name == "vol de vie") obj = GameObject.Find("DescriptionVolDeVie");
		else if (this.GetComponent<SpriteRenderer>().sprite.name == "effet combo") obj = GameObject.Find("DescriptionCombos");
		else if (this.GetComponent<SpriteRenderer>().sprite.name == "augm degat") obj = GameObject.Find("DescriptionDegatInflige");
		else if (this.GetComponent<SpriteRenderer>().sprite.name == "bombe") obj = GameObject.Find("DescriptionExplosion");
		else if (this.GetComponent<SpriteRenderer>().sprite.name == "parade") obj = GameObject.Find("DescriptionLoot");
		else if (this.GetComponent<SpriteRenderer>().sprite.name == "augm vie") obj = GameObject.Find("DescriptionMaxHP");
		else if (this.GetComponent<SpriteRenderer>().sprite.name == "poussé") obj = GameObject.Find("DescriptionPoussee");
		else if (this.GetComponent<SpriteRenderer>().sprite.name == "refus de la mort") obj = GameObject.Find("DescriptionRefusDeLaMort");
		else if (this.GetComponent<SpriteRenderer>().sprite.name == "augmentation de la resistance") obj = GameObject.Find("DescriptionResistance");
		else if (this.GetComponent<SpriteRenderer>().sprite.name == "vulnérabilité des ennemis") obj = GameObject.Find("DescriptionVulnerabilite");
		else if (this.GetComponent<SpriteRenderer>().sprite.name == "parade") obj = GameObject.Find("DescriptionMDegatInflige");
		else if (this.GetComponent<SpriteRenderer>().sprite.name == "augm poussé du spé") obj = GameObject.Find("DescriptionMPoussee");
		else obj = GameObject.Find("DescriptionMLoot");

		obj.GetComponent<InventoryUIManager>().seller = this.GetComponentInParent<Seller>();
		anim = obj.GetComponent<Animator>();
	}

	void BuyStart()
	{
		Debug.Log("buy !! ");
	}
}
