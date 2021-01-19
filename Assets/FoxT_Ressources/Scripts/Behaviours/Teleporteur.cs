using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporteur : MonoBehaviour
{
	public Transform linkedTeleport;

	public string position;

	[SerializeField]
	string animationStart, animationUse;

	[SerializeField]
	Sprite sprite;

	public bool receive, isActive;

	[SerializeField]
	DisableCanvas transition;

	Animator anim;

	private void Awake()
	{
		transition = GameObject.Find("SceneTransition").GetComponent<DisableCanvas>();
		anim = GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			if (!receive && isActive)
			{
				linkedTeleport.GetComponent<Teleporteur>().receive = true;
				StartCoroutine(TransitionOn(collision));
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			if (receive) receive = false;
		}
	}

	IEnumerator TransitionOn(Collider2D col)
	{
		anim.Play(animationUse);
		yield return new WaitForSeconds(.2f);
		transition.TPTransition();
		yield return new WaitForSeconds(1.2f);
		col.transform.position = linkedTeleport.position;
	}

	public void StartAnimation()
	{
		if (linkedTeleport == null)
		{
			isActive = false;
			return;
		}
		anim.Play(animationStart);
	}
}
