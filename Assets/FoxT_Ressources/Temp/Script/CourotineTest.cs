using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourotineTest : MonoBehaviour
{
    public KeyCode on = KeyCode.A, off = KeyCode.B;

    private Coroutine test;

    public float time;

    public bool isEnabled;
    void Start()
    {
        isEnabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(on))
        {
            test = StartCoroutine(TheTest(time));
            Debug.Log("START");
        }
        else if (Input.GetKeyDown(off))
        { 
            StopCoroutine(test);
            Debug.Log("STOP");
        }
    }

    IEnumerator TheTest(float temps)
    {
        isEnabled = true;
        yield return new WaitForSeconds(temps);
        isEnabled = false;
    }
}
