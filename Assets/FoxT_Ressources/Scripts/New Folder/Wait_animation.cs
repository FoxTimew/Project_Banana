using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait_animation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimationDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AnimationDelay()
    {
        GetComponent<Controler>().isInAnimation = true;
        yield return new WaitForSeconds(1f);
        GetComponent<Controler>().isInAnimation = false;
    }
}
