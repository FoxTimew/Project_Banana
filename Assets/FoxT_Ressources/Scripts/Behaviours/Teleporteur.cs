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

	public bool receive, isActive, seller;

	[SerializeField]
	DisableCanvas transition;

	Animator anim;

	private void Start()
	{
		transition = GameObject.Find("SceneTransition").GetComponent<DisableCanvas>();
		anim = GetComponent<Animator>();


		if (seller)
		{
			StartAnimation();
		}
		StartCoroutine(ERASE());
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			if (!receive && isActive)
			{
				linkedTeleport.GetComponent<Teleporteur>().receive = true;
				//collision.GetComponent<Controler>().isTeleporting = true;
				StartCoroutine(TransitionOn(collision));
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			if (receive) receive = false;
			if (seller) StartAnimation();
		}
	}

	IEnumerator TransitionOn(Collider2D col)
	{
		anim.Play(animationUse);
		GameObject.Find("Playable_Character").GetComponent<Controler>().isTeleporting = true;
		yield return new WaitForSeconds(.2f);
		transition.TPTransition();
		yield return new WaitForSeconds(1.2f);
		col.transform.position = linkedTeleport.position;
		GameObject.Find("Playable_Character").GetComponent<Controler>().isTeleporting = false;
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

	IEnumerator ERASE()
	{
		yield return new WaitForSeconds(.1f);
		if (linkedTeleport == null) Destroy(this.gameObject);
	}
}
