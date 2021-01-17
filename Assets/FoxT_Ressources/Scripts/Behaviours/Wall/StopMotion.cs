using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMotion : MonoBehaviour
{
    [SerializeField]
    Animator anim;

	private void OnDisable()
	{
		anim.speed = 0f;
	}
}
