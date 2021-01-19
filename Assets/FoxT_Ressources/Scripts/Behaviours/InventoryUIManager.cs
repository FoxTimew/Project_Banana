using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
	Core core;
	[SerializeField]
	GameObject[] itemZone, Item;

	[SerializeField]
	GameObject charm;

	[SerializeField]
	Vector2 scale;

	int niveau;

	[SerializeField]
	bool maudit;

	[SerializeField]
	GameObject button;

	[SerializeField]
	int[] price = new int[3];

	[SerializeField]
	Text priceText;

	[SerializeField]
	string charmNameCode;

	public Seller seller;

	private void Awake()
	{
		core = GameObject.Find("Core").GetComponent<Core>();
		priceText.text = price[niveau].ToString();
	}

	public void OnClick()
	{
		BuyStart();
	}

	void BuyStart()
	{
		if (core.data.gold >= price[niveau])
		{
			core.data.gold -= price[niveau];
			seller.objetChoisi = charmNameCode;
			seller.ValueUpdate();
			UpdateInventory();
		}
	}

	void UpdateInventory()
	{
		if (niveau == 0 || maudit)
		{
			for (int i = 0; i < itemZone.Length; i++)
			{
				if (itemZone[i].transform.childCount == 0)
				{
					var newObject = Instantiate(charm, Vector3.zero, this.transform.rotation);
					newObject.transform.parent = itemZone[i].transform;
					newObject.transform.localPosition = Vector3.zero;
					newObject.transform.localScale = scale;
					Item[i].SetActive(true);
					i = itemZone.Length;
				}
			}
			if (maudit) button.SetActive(false);
			niveau++;
			//update image and price;
			priceText.text = price[niveau].ToString();
		}
		else if (niveau == 1)
		{
			//update image and price
			niveau++;
			priceText.text = price[niveau].ToString();
		}
		else if (niveau == 2)
		{
			//update image and price
			button.SetActive(false);
			niveau++;
			priceText.enabled = false;
		}
	}





	
}
