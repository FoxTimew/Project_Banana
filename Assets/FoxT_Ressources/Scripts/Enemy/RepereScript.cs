using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepereScript : MonoBehaviour
{
    void Update()
    {
        this.transform.position = new Vector2(this.transform.position.x, GameObject.Find("Playable_Character").transform.position.y);
    }
}
