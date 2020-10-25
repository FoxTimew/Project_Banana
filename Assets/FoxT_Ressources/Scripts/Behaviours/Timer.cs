using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
	bool finish;

	public bool delay(int time)
	{
		if (finish)
		{
			finish = false;
			return true;
		}
		else StartCoroutine(TimerUpdate(time));
		return false;
	}
	public IEnumerator TimerUpdate(int time)
	{
		yield return new WaitForSeconds(time);
		finish = true;
	}
}
