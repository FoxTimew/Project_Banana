using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapAttaque : MonoBehaviour
{
	public Vector2 direction;

	[SerializeField]
	Transform repere, player, ombre;

	[SerializeField]
	Rigidbody2D rb;

	[SerializeField]
	float vitesse;

	public bool canMove = false, endAnimation = true, animationOn, isStun;

	[SerializeField]
	float dureeAnimation, durrefrappe, dureepause, dureeANimationMort;

	[SerializeField]
	string stateName, mortanimation;
	string currentState;

	[SerializeField]
	Animator anim;

	[SerializeField]
	public int health;

	[SerializeField]
	int number;

	[SerializeField]
	HandAnimationHit hitAnim;

	private void Start()
	{
		endAnimation = false;
		player = GameObject.Find("Playable_Character").GetComponent<Transform>();
	}

	private void FixedUpdate()
	{
		if (!animationOn) BackToRepere();
		else if (canMove) FollowPlayer();
	}

	void FollowPlayer()
	{
		if (canMove)
		{
			float step = vitesse * Time.deltaTime;
			ombre.position = Vector3.MoveTowards(ombre.position, player.position, step);
		}
	}

	public IEnumerator StartAnimation()
	{
		if (!animationOn)
		{
			canMove = true;
			animationOn = true;
			yield return new WaitForSeconds(10);
			ChangeAnimationState(stateName);
			canMove = false;
			endAnimation = false;
			Coroutine courotine = StartCoroutine(WaitForAttackAgain());
			yield return new WaitForSeconds(dureeAnimation);
			StopCoroutine(courotine);
			animationOn = false;
			ChangeAnimationState("void");
		}
		else yield return null;
	}

	void BackToRepere()
	{
		float step = vitesse * Time.deltaTime;
		ombre.position = Vector3.MoveTowards(ombre.position, repere.position, step);
		if (ombre.position == repere.position) endAnimation = true;
	}

	void ChangeAnimationState(string newState)
	{
		if (currentState == newState) return;

		currentState = newState;

		anim.Play(newState);
	}

	IEnumerator WaitForAttackAgain()
	{
		yield return new WaitForSeconds(durrefrappe);
		canMove = true;
		yield return new WaitForSeconds(dureepause);
		canMove = false;
		yield return new WaitForSeconds(durrefrappe);
		canMove = true;
		yield return new WaitForSeconds(dureepause);
		canMove = false;
		yield return new WaitForSeconds(10);
	}

	public void TakeDammage(int damage)
	{
		if (!isStun) return;
		health -= damage;

		if (health <= 0)
		{
			health = 0;
			StopAllCoroutines();
			Die();
		}
		else
		{
			StartCoroutine(hitAnim.hitAnimation());
		}
	}

	void Die()
	{
		GameObject.Find("BossManager").GetComponent<HandManager>().mainKilled++;
		GameObject.Find("BossManager").GetComponent<HandManager>().mainActive[number] = false;
		GameObject.Find("BossManager").GetComponent<HandManager>().hands[number] = null;
		ChangeAnimationState(mortanimation);
		StartCoroutine(DieAnimationDelay());
	}

	IEnumerator DieAnimationDelay()
	{
		yield return new WaitForSeconds(dureeANimationMort);
		Destroy(this.gameObject);
	}
}

