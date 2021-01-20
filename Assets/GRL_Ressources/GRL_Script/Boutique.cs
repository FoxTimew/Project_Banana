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
    public GameObject canvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("té la");
        Here = true;
        anim.SetBool("Here", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("té po la");
        Here = false;
        anim.SetBool("Here", false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Here == true && Input.GetKeyDown(KeyCode.M) && Open == false)
        {
            boutique.SetActive(true);
            Open = true;
            canvas.SetActive(true);
            Time.timeScale = 0f;
        } 

        else if ( Input.GetKeyDown(KeyCode.Escape) && Here == true && Open == true)
        {
            boutique.SetActive(false);
            Open = false;
            canvas.SetActive(false);
            Time.timeScale = 1f;
        }

        if (Open == true)
        {
            canvas.SetActive(false);
            menuPause.SetActive(false);
        }

        if (Open == false)
        {
            canvas.SetActive(true);
            menuPause.SetActive(true);
        }
    }

    public void Exit()
    {
        boutique.SetActive(false);
        Open = false;
        canvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
