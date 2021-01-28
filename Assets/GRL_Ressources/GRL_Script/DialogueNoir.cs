using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueNoir : MonoBehaviour
{
    public GameObject DialogueBox;
    public Text DialogueText;
    public string Dialogue1, Dialogue2, Dialogue3, Dialogue4;

    public bool DialogueActive;
    public bool Parle = false;
    public int LettersPerSecond;
    public int Aupif;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            DialogueActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DialogueActive = false;
            DialogueBox.SetActive(false);
            Parle = false;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (DialogueActive == true && (Parle == false))
        {
            Aupif = Random.Range(1, 5);
            Parle = true;
            Debug.Log(Aupif);

            if (Aupif == 1)
            {

                if (DialogueBox.activeInHierarchy)
                {
                    DialogueBox.SetActive(false);


                }

                else
                {
                    FindObjectOfType<AudioManager>().Play("SFX_UI_Speak");
                    DialogueBox.SetActive(true);
                    DialogueText.text = Dialogue1;
                    StopAllCoroutines();
                    StartCoroutine(TypeSentence(Dialogue1));

                }
            }


            if (Aupif == 2)
            {
               

                if (DialogueBox.activeInHierarchy)
                {
                    DialogueBox.SetActive(false);
                }

                else
                {
                    FindObjectOfType<AudioManager>().Play("SFX_UI_Speak");
                    DialogueBox.SetActive(true);
                    DialogueText.text = Dialogue1;
                    StopAllCoroutines();
                    StartCoroutine(TypeSentence(Dialogue2));

                }
            }




            if (Aupif == 3)
            {
            
                if (DialogueBox.activeInHierarchy)
                {
                    DialogueBox.SetActive(false);


                }

                else
                {
                    FindObjectOfType<AudioManager>().Play("SFX_UI_Speak");
                    DialogueBox.SetActive(true);
                    DialogueText.text = Dialogue1;
                    StopAllCoroutines();
                    StartCoroutine(TypeSentence(Dialogue3));

                }
            }




            if (Aupif == 4)
            {
                if (DialogueBox.activeInHierarchy)
                {
                    DialogueBox.SetActive(false);


                }

                else
                {
                    FindObjectOfType<AudioManager>().Play("SFX_UI_Speak");
                    DialogueBox.SetActive(true);
                    DialogueText.text = Dialogue1;
                    StopAllCoroutines();
                    StartCoroutine(TypeSentence(Dialogue4));

                }
            }


        }
    }



    IEnumerator TypeSentence(string Dialogue)
    {
        DialogueText.text = "";
        foreach (char letter in Dialogue.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(1f / LettersPerSecond);
            //  yield return null;

        }
    }

}

