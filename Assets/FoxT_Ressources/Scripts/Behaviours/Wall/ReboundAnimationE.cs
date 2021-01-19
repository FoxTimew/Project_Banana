using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboundAnimationE : MonoBehaviour
{
    [SerializeField] string wallUp;
    Animator anim;
    List<Collider2D> obj = new List<Collider2D>();

    /*if(((collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(1, -1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(-1, -1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == Vector2.down) && wallPosition == "S") ||
                    ((collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(-1, 1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(-1, -1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == Vector2.left) && wallPosition == "W") ||
                    ((collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(1, 1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(1, -1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == Vector2.right) && wallPosition == "E") ||
                    ((collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(1, 1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(-1, 1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == Vector2.up) && wallPosition == "N") ||)*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !obj.Contains(collision))
        {
            if (collision.gameObject.GetComponent<Controler>().isEjected)
            {
                if (collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(1, -1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(1, 1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == Vector2.right)
                {
                    obj.Add(collision);
                }
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<Ejecting>().enabled == true)
            {
                if (collision.gameObject.GetComponent<Ejecting>().direction == new Vector2(1, 1).normalized || collision.gameObject.GetComponent<Ejecting>().direction == new Vector2(1, -1).normalized || collision.gameObject.GetComponent<Ejecting>().direction == Vector2.right)
                {
                    obj.Add(collision);
                }
            }
        }
        else if (collision.tag == "Barril")
        {
            if (collision.GetComponentInParent<Barril_Sys>().isEjected)
            {
                if (collision.GetComponentInParent<Barril_Sys>().direction == new Vector2(1, 1).normalized || collision.gameObject.GetComponentInParent<Barril_Sys>().direction == new Vector2(1, -1).normalized || collision.gameObject.GetComponentInParent<Barril_Sys>().direction == Vector2.right)
                {
                    obj.Add(collision);
                }
            }
        }

        //-----------------------------------
        //Jouer animation
        //-----------------------------------
        if (obj.Count != 0)
        {
            anim.SetFloat("direction", 1);
            anim.speed = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") && obj.Contains(collision))
        {
            obj.Remove(collision);
            if (obj.Count == 0)
            {
                StopAllCoroutines();
                StartCoroutine(animationDelay());
            }
        }
    }

    void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    public void UpdateAnimation()
    {
        if (obj.Count != 0)
        {
            anim.SetFloat("direction", 1);
            anim.speed = 1;
        }
    }

    IEnumerator animationDelay()
    {
        yield return new WaitForSeconds(1);
        if (obj.Count == 0)
        {
            anim.SetFloat("direction", -1);
            anim.speed = 1;
        }
        else StartCoroutine(animationDelay());
    }
}

