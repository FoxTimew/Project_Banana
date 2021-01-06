using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRebound : MonoBehaviour
{
    [SerializeField] string wallPosition;
    [SerializeField] float marge;
    List<Collider2D> obj = new List<Collider2D>();
    List<Collider2D> oldObj = new List<Collider2D>();
    Collider2D thisCol;
    public LayerMask layer;
    void Start()
    {
        thisCol = this.GetComponent<Collider2D>();
        //Lancer animation
    }

    void Update()
    {
        EnemiDetector();
        if (obj.Count != 0) DirectionAtribution();
    }

    void EnemiDetector()
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
    }

    void DirectionAtribution()
    {
        foreach (Collider2D col in obj)
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
        }
        obj = new List<Collider2D>();
    }

    void AdjustPosition(Transform obj)
    {
        switch (wallPosition)
        {
            case "N":
                obj.position -= new Vector3(0f, marge, 0f);
                break;
            case "E":
                obj.position -= new Vector3(marge, 0f, 0f);
                break;
            case "S":
                obj.position += new Vector3(0f, marge, 0f);
                break;
            default:
                obj.position += new Vector3(marge, 0f, 0f);
                break;
        }
    }
}

