using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAlpha : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sprite;

    float transition = 3f;

    List<Collider2D> col = new List<Collider2D>();

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" && !col.Contains(collision))
        {
            col.Add(collision);
            StartCoroutine(AlphaTransitionOn());
        }
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") && col.Contains(collision))
        {
            col.Remove(collision);
            if (col.Count == 0)
            {
                StopAllCoroutines();
                StartCoroutine(AlphaTransitionOff());
            }
        }
    }

	IEnumerator AlphaTransitionOn()
    {
        yield return new WaitForEndOfFrame();
        float alpha = sprite.color.a;
        if (alpha >= 0.39f)
        {
            sprite.color -= new Color(0f, 0f, 0f, transition * Time.deltaTime);
            StartCoroutine(AlphaTransitionOn());
        }
    }

    IEnumerator AlphaTransitionOff()
    {
        yield return new WaitForEndOfFrame();
        float alpha = sprite.color.a;
        if (alpha <= 1)
        {
            sprite.color += new Color(0f, 0f, 0f, transition * Time.deltaTime);
            StartCoroutine(AlphaTransitionOff());
        }
    }
}
