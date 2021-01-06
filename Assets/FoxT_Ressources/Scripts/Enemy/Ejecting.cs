using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ejecting : MonoBehaviour
{
    public AnimationCurve pousseForce;
    public Vector2 direction;
    public float timeElapsed;
    Rigidbody2D rb;
	private void Start()
	{
        rb = this.GetComponent<Rigidbody2D>();

    }
	void Update()
    {
        if (pousseForce.Evaluate(timeElapsed) <= 0)
        {
            this.GetComponent<Ejecting>().enabled = false;
            return;
        }
        Poussee();
    }

    void Poussee()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * pousseForce.Evaluate(timeElapsed) * Time.deltaTime);
        timeElapsed += Time.deltaTime;
        if (timeElapsed > pousseForce.keys[pousseForce.keys.Length - 1].time) this.GetComponent<Ejecting>().enabled = false;
        Debug.Log(direction);
    }
}
