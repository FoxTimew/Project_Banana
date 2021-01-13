using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRebound : MonoBehaviour
{
    [SerializeField] string wallPosition, wallUp;
    [SerializeField] float marge;
    public Animation test;
    string currentState;
    Animator anim;
    List<Collider2D> obj = new List<Collider2D>();
    /*List<Collider2D> oldObj = new List<Collider2D>();
    Collider2D thisCol;
    public LayerMask layer;*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") && !obj.Contains(collision))
        {
            obj.Add(collision);
            /*if (collision.GetComponent<Controler>().isEjected)
            {
                DirectionAtribution(collision);
            }*/
        }
        if (obj.Count != 0) ChangeAnimatorState(wallUp);
        else if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<Ejecting>().enabled == true)
            {
                DirectionAtribution(collision);
                obj.Add(collision);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") && obj.Contains(collision))
        {
            obj.Remove(collision);
        }
        if (obj.Count == 0)
        {
            
        }
    }

	private void OnEnable()
	{
        test[wallUp].speed = 0.5f;
        test.Play(wallUp);
    }

	void Awake()
    {
        anim = GetComponentInParent<Animator>();
        anim.speed = 0;
        //thisCol = this.GetComponent<Collider2D>();
        //Lancer animation
    }

    void Update()
    {
        //EnemiDetector();
        //if (obj.Count != 0) DirectionAtribution();
    }

    /*void EnemiDetector(Collider2D enemy)
    {
        Collider2D[] enemy = Physics2D.OverlapBoxAll(this.transform.position, thisCol.bounds.size, 0f, layer);

        if (enemy.Length != 0)
        {
            foreach (Collider2D col in enemy)
            {
                if (!oldObj.Contains(col)) obj.Add(col);
                else AdjustPosition(col.transform);
            }
            oldObj = new List<Collider2D>();
            foreach (Collider2D col in enemy)
            {
                oldObj.Add(col);
            }
        }
        else oldObj = new List<Collider2D>();
    }*/

    void DirectionAtribution(Collider2D col)
    {
        switch (wallPosition)
        {
            case "N":
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
                break;
            case "E":
                if (col.gameObject.GetComponent<Ejecting>().direction == new Vector2(1, 1).normalized)
                {
                    col.gameObject.GetComponent<Ejecting>().direction = new Vector2(-1, 1).normalized;
                }
                else if (col.gameObject.GetComponent<Ejecting>().direction == new Vector2(1, -1).normalized)
                {
                    col.gameObject.GetComponent<Ejecting>().direction = new Vector2(-1, -1).normalized;
                }
                else
                {
                    col.gameObject.GetComponent<Ejecting>().direction = Vector2.left;
                }
                break;
            case "S":
                if (col.gameObject.GetComponent<Ejecting>().direction == new Vector2(1, -1).normalized)
                {
                    col.gameObject.GetComponent<Ejecting>().direction = new Vector2(1, 1).normalized;
                }
                else if (col.gameObject.GetComponent<Ejecting>().direction == new Vector2(-1, -1).normalized)
                {
                    col.gameObject.GetComponent<Ejecting>().direction = new Vector2(-1, 1).normalized;
                }
                else
                {
                    col.gameObject.GetComponent<Ejecting>().direction = Vector2.up;
                }
                break;
            default:
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
                break;
        }
        //obj = new List<Collider2D>();
    }

    void ChangeAnimatorState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);
        currentState = newState;
    }
}

