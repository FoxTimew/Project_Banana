using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boutique : MonoBehaviour
{
    public BoxCollider2D collider;
    bool Here = false;
    bool Open = false;
    public Animator anim;
    public GameObject menuPause;
    public GameObject boutique;
    public GameObject press;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("té la");
        Here = true;
        //press.SetActive(true);
        anim.SetBool("here", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("té po la");
        Here = false;
        //press.SetActive(false);
        anim.SetBool("here", false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Here == true)
        {
            boutique.SetActive(false);
            Open = false;
            Time.timeScale = 1f;
            //menuPause.GetComponent<PauseMenu>().Open = false;
        }

        if(Here == true && Input.GetKeyDown(KeyCode.M) && Open == false)
        {
            boutique.SetActive(true);
            Open = true;
            Time.timeScale = 0f;
            //menuPause.GetComponent<PauseMenu>().Open = true;
        } 
    }
}
