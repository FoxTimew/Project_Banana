using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianInvok : MonoBehaviour
{
	[SerializeField]
	GameObject GuardianObject;
	private void OnEnable()
	{
		Object.Instantiate(GuardianObject, transform.position, transform.rotation);
		GameObject.Find("Core").GetComponent<Core>().updateEnemyVision = true;
	}
}
