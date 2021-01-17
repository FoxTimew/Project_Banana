using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallReboundW : MonoBehaviour
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
                if(collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(-1, -1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(-1, 1).normalized || collision.gameObject.GetComponent<Controler>().pousseeDirection == Vector2.left)
                {
                    DirectionAtribution(collision, true);
                }
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<Ejecting>().enabled == true)
            {
                if (collision.gameObject.GetComponent<Ejecting>().direction == new Vector2(-1, -1).normalized || collision.gameObject.GetComponent<Ejecting>().direction == new Vector2(-1, 1).normalized || collision.gameObject.GetComponent<Ejecting>().direction == Vector2.left)
                {
                    DirectionAtribution(collision, false);
                }
            }
        }
    }

    void DirectionAtribution(Collider2D col, bool player)
    {
        if (player)
        {
            if (col.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(-1, 1).normalized)
            {
                col.gameObject.GetComponent<Controler>().pousseeDirection = new Vector2(1, 1).normalized;
            }
            else if (col.gameObject.GetComponent<Controler>().pousseeDirection == new Vector2(-1, -1).normalized)
            {
                col.gameObject.GetComponent<Controler>().pousseeDirection = new Vector2(1, -1).normalized;
            }
            else
            {
                col.gameObject.GetComponent<Controler>().pousseeDirection = Vector2.right;
            }
        }
        else
        {
            if (col.gameObject.GetComponent<Ejecting>().direction == new Vector2(-1, 1).normalized)
            {
                col.gameObject.GetComponent<Ejecting>().direction = new Vector2(1, 1).normalized;
            }
            else if (col.gameObject.GetComponent<Ejecting>().direction == new Vector2(-1, -1).normalized)
            {
                col.gameObject.GetComponent<Ejecting>().direction = new Vector2(1, -1).normalized;
            }
            else
            {
                col.gameObject.GetComponent<Ejecting>().direction = Vector2.right;
            }
        }
    }
}

