using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSys : MonoBehaviour
{
	public Data data;
	public int gold;
	public int cristal;
	public int maudite;

	public collectible collectible;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		data.gold += gold;
		data.cristal+= cristal;
		data.meaudite += maudite;
		Destroy(this.gameObject);
	}
}
