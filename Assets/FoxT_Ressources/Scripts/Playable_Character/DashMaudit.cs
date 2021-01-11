using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMaudit : MonoBehaviour
{
    public int valueDammage;

    List<Collider2D> enemyCol = new List<Collider2D>();

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Enemy")
        {
            enemyCol.Add(collision);
            StartCoroutine(DashDammage(1, collision));
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Enemy" && enemyCol.Contains(collision))
        {
            enemyCol.Remove(collision);
        }
	}

	private void OnEnable()
	{
        StartCoroutine(DashAttack(4));
	}

    IEnumerator DashDammage(float time, Collider2D col)
    {
        col.GetComponent<EnemySys>().TakeDamage(valueDammage, 0f);
        yield return new WaitForSeconds(time);
        if (enemyCol.Contains(col)) StartCoroutine(DashDammage(valueDammage, col));
    }

    IEnumerator DashAttack(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
