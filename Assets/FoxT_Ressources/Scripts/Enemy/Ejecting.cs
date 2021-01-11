using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ejecting : MonoBehaviour
{
    public AnimationCurve pousseForce;
    public Vector2 direction;
    public float timeElapsed, bonusForce, forcePoussee, damage;
    Rigidbody2D rb;
    BoxCollider2D selfCol;
    public bool pousseeCharmEnable;
    [SerializeField]
    LayerMask enemyLayer;
	private void Start()
	{
        rb = this.GetComponent<Rigidbody2D>();
        selfCol = this.GetComponent<BoxCollider2D>();
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
        rb.AddForce(direction * (pousseForce.Evaluate(timeElapsed) + forcePoussee + bonusForce) * Time.deltaTime);
        timeElapsed += Time.deltaTime;
        if (timeElapsed > pousseForce.keys[pousseForce.keys.Length - 1].time) this.GetComponent<Ejecting>().enabled = false;
        Debug.Log(direction);

        if (!pousseeCharmEnable) return;
        Collider2D[] enemyCharm = Physics2D.OverlapBoxAll(this.transform.position,selfCol.size, 0f, enemyLayer);
        if (enemyCharm == null) return;
        foreach (Collider2D obj in enemyCharm)
        {
           if (obj.gameObject != this.gameObject) obj.gameObject.GetComponent<EnemySys>().TakeDamage(Mathf.RoundToInt(damage), 0f);
        }
    }
}
