using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MamieDialogue : MonoBehaviour
{
    public GameObject image;
    bool ready, open;
    public GameObject text1, text2, text3, text4;
    bool openText1, openText2, openText3, openText4;
    [SerializeField]
    CinemachineVirtualCamera shopVirtualCam;
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Je suis la");
        ready = true;
        anim.SetBool("ici", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ready = false;
        anim.SetBool("ici", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            GameObject.Find("Playable_Character").GetComponent<Controler>().isInAnimation = true;
        }
        else 
            GameObject.Find("Playable_Character").GetComponent<Controler>().isInAnimation = false;
        if (Input.GetButtonDown("Interaction") && ready == true && open == false)
        {
            FindObjectOfType<AudioManager>().Play("SFX_UI_Speak");
            open = true;
            image.SetActive(true);
            text1.SetActive(true);
            openText1 = true;
        }

        else if (Input.GetButton("Interaction") && ready == true && open == true && openText1 == true)
        {
            text1.SetActive(false);
            FindObjectOfType<AudioManager>().Play("SFX_UI_Speak");
            text2.SetActive(true);
            openText1 = false;
            openText2 = true;
        }

        else if (Input.GetButtonDown("Interaction") && ready == true && open == true && openText2 == true)
        {
            FindObjectOfType<AudioManager>().Play("SFX_UI_Speak");
            text2.SetActive(false);
            FindObjectOfType<AudioManager>().Play("SFX_UI_Speak");
            text3.SetActive(true);
            openText2 = false;
            openText3 = true;
        }

        else if (Input.GetButtonDown("Interaction") && ready == true && open == true && openText3 == true)
        {
            
            text3.SetActive(false);
            FindObjectOfType<AudioManager>().Play("SFX_UI_Speak");
            text4.SetActive(true);
            openText3 = false;
            openText4 = true;
            shopVirtualCam.Priority = 11;
        }

        else if (Input.GetButtonDown("Interaction") && open == true && openText4 == true)
        {
            open = false;
            image.SetActive(false);
            text4.SetActive(false);
            shopVirtualCam.Priority = 0;
            openText4 = false;
        }
    }
}
