using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Boutique : MonoBehaviour
{
    public BoxCollider2D collider;
    bool Here = false;
    bool Open = false, closed;
    public Animator anim;
    public GameObject menuPause;
    public GameObject boutique;
    public GameObject press;
    public GameObject canvas;
    public GameObject firstOne;

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
        if (closed)
        {
            closed = false;
            return;
        }
        if (Here == true && Input.GetButtonDown("Interaction") && Open == false)
        {
            boutique.SetActive(true);
            Open = true;
            canvas.SetActive(true);
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstOne);
        }
        else if (Input.GetButton("Cancel") && Here == true && Open == true)
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
        closed = true;
        boutique.SetActive(false);
        Open = false;
        canvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
