using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whileTest : MonoBehaviour
{
    int i;
    Vector2 thisVector = new Vector2(1, 1);
    List<Vector2> test = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        while (i < 10)
        {
            test.Add(thisVector * i);
            i++;
        }
        Debug.Log(test[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (test.Contains(new Vector2(2, 2))) Debug.Log("TROUVER");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (test.Contains(new Vector2(9, 9))) Debug.Log("PAS TROUVER");
        }
    }
}
