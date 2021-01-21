using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallReboundN : MonoBehaviour
{
    /*if(((collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(1, -1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(-1, -1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == Vector2.down) && wallPosition == "S") ||
                    ((collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(-1, 1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(-1, -1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == Vector2.left) && wallPosition == "W") ||
                    ((collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(1, 1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(1, -1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == Vector2.right) && wallPosition == "E") ||
                    ((collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(1, 1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(-1, 1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == Vector2.up) && wallPosition == "N") ||)*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Controler>().isEjected)
            {
                if (collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(1, 1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(-1, 1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == Vector2.up)
                {
                    collision.GetComponent<Health>().TakeDamage(5, null);
                    DirectionAtribution(collision, 1);
                }
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<Ejecting>().enabled == true)
            {
                if (collision.gameObject.GetComponent<Ejecting>().direction == new Vector2(1, 1).normalized || collision.gameObject.GetComponent<Ejecting>().direction == new Vector2(-1, 1).normalized || collision.gameObject.GetComponent<Ejecting>().direction == Vector2.up)
                {
                    collision.GetComponent<EnemySys>().TakeDamage(5, 0f);
                    DirectionAtribution(collision, 2);
                }
            }
        }
        else if (collision.tag == "Barril")
        {
            if (collision.GetComponentInParent<Barril_Sys>().isEjected)
            {
                if (collision.GetComponentInParent<Barril_Sys>().direction == new Vector2(1, 1).normalized || collision.gameObject.GetComponentInParent<Barril_Sys>().direction == new Vector2(-1, 1).normalized || collision.gameObject.GetComponentInParent<Barril_Sys>().direction == Vector2.up)
                {
                    DirectionAtribution(collision, 3);
                }
            }
        }
    }

    void DirectionAtribution(Collider2D col, int player)
    {
        if (player == 1)
        {
            if (col.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(1, 1).normalized)
            {
                col.gameObject.GetComponent<Controler>().pousseeDirection = new Vector2(1, -1).normalized;
            }
            else if (col.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(-1, 1).normalized)
            {
                col.gameObject.GetComponent<Controler>().pousseeDirection = new Vector2(-1, -1).normalized;
            }
            else
            {
                col.gameObject.GetComponent<Controler>().pousseeDirection = Vector2.down;
            }
        }
        else if (player == 2)
        {
            if (col.gameObject.GetComponent<Ejecting>().direction == new Vector2(1, 1).normalized)
            {
                col.gameObject.GetComponent<Ejecting>().direction = new Vector2(1, -1).normalized;
            }
            else if (col.gameObject.GetComponent<Ejecting>().direction == new Vector2(-1, 1).normalized)
            {
                col.gameObject.GetComponent<Ejecting>().direction = new Vector2(-1, -1).normalized;
            }
            else
            {
                col.gameObject.GetComponent<Ejecting>().direction = Vector2.down;
            }
        }
        else
        {
            if (col.gameObject.GetComponentInParent<Barril_Sys>().direction == new Vector2(1, 1).normalized)
            {
                col.gameObject.GetComponentInParent<Barril_Sys>().direction = new Vector2(1, -1).normalized;
            }
            else if (col.gameObject.GetComponentInParent<Barril_Sys>().direction == new Vector2(-1, 1).normalized)
            {
                col.gameObject.GetComponentInParent<Barril_Sys>().direction = new Vector2(-1, -1).normalized;
            }
            else
            {
                col.gameObject.GetComponentInParent<Barril_Sys>().direction = Vector2.down;
            }
        }
    }
}

