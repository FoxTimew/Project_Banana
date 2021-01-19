using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCanvas : MonoBehaviour
{
	[SerializeField]
	GameObject[] obj;

	[SerializeField]
	float dureeAnimationTP;

	[SerializeField]
	string nameState;

	public bool Test, second;

	private void Update()
	{
		if (Test) TPTransition();
		else if (second) EndTransition();
	}

	private void Start()
	{
		StartCoroutine(TPTransitionDelay(1.5f));
	}

	public void TPTransition()
	{
		Test = false;
		obj[0].SetActive(true);
		obj[0].GetComponent<Animator>().SetBool("Transition", true);
		StartCoroutine(TPTransitionDelay(dureeAnimationTP));
	}

	IEnumerator TPTransitionDelay(float time)
	{
		yield return new WaitForSeconds(time);
		obj[0].GetComponent<Animator>().SetBool("Transition", false);
		yield return new WaitForSeconds(1.5f);
		obj[0].SetActive(false);
	}

	public void EndTransition()
	{
		second = false;
		obj[1].SetActive(true);
		obj[1].GetComponent<Animator>().Play(nameState);
	}
}
