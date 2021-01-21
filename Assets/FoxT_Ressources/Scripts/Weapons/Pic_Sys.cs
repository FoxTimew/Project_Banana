using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pic_Sys : MonoBehaviour
{
    List<Collider2D> obj = new List<Collider2D>();

    bool isActive;

    Animator anim;

    [SerializeField]
    float activationDelay, disableDelay;

    [SerializeField]
    int damage;

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Player" || collision.tag == "Enemy" || collision.tag == "Barril")
        {
            obj.Add(collision);
            if (isActive)
            {
                if (collision.tag == "Player")
                {
                    collision.GetComponent<Health>().TakeDamage(damage, null);
                    if (collision.GetComponent<Health>().health == 0) obj.Remove(collision);
                }
                else if (collision.tag == "Barril")
                {
                    obj.Remove(collision);
                    collision.GetComponentInParent<Barril_Sys>().Explosion();
                }
                else
                {
                    collision.GetComponent<EnemySys>().TakeDamage(damage, 0.5f);
                    if (collision.GetComponent<EnemySys>().health == 0) obj.Remove(collision);
                }
            }
        }

        if (obj.Count != 0 && !isActive)
        {
            StartCoroutine(ActivationDelay());
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            if (obj.Contains(collision)) obj.Remove(collision);
        }
	}

	// Start is called before the first frame update
	void Start()
    {
        anim = GetComponent<Animator>();
    }

    IEnumerator ActivationDelay()
    {
        for (int i = 0; i < obj.Count; i++)//(Collider2D col in obj)
        {
            if (obj[i].tag == "Player")
            {
                obj[i].GetComponent<Health>().TakeDamage(damage, null);
                if (obj[i].GetComponent<Health>().health == 0)
                {
                    obj.Remove(obj[i]);
                    i--;
                }
            }
            else if (obj[i].tag == "Barril")
            {
                obj[i].GetComponentInParent<Barril_Sys>().Explosion();   
                obj.Remove(obj[i]);
                i--;
            }
            else
            {
                obj[i].GetComponent<EnemySys>().TakeDamage(damage, 0.5f);
                if (obj[i].GetComponent<EnemySys>().health == 0)
                {
                    obj.Remove(obj[i]);
                    i--;
                }
            }
        }
        isActive = true;
        anim.SetBool("Active", true);
        yield return new WaitForSeconds(3);
        isActive = false;
        anim.SetBool("Active", false);
        if (obj.Count != 0)
        {
            StartCoroutine(ActivationDelay());
        }
    }
}
