using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSys : MonoBehaviour
{
	public Data data;

	public collectible collectible;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collectible == collectible.gold) data.gold++;
		else if (collectible == collectible.cristal) data.cristal++;
		else data.meaudite++;
		Destroy(this.gameObject);
	}
}
