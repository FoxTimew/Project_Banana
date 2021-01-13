using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeakBox : MonoBehaviour
{
    public GameObject DialogueBox;
    public Text DialogueText;
    public string Dialogue;
    public bool DialogueActive;
    public int LettersPerSecond;
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
        }
    }

    public IEnumerator TypeDialogue(string line)
    {
        DialogueText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(1f / LettersPerSecond);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (DialogueActive = true))
        {
            if (DialogueBox.activeInHierarchy)
            {
                DialogueBox.SetActive(false);
               
            }
            else
            {
               
                DialogueBox.SetActive(true);
                DialogueText.text = Dialogue;
                StopAllCoroutines();
                StartCoroutine(TypeSentence(Dialogue));

            }
        }
    }
    IEnumerator TypeSentence (string Dialogue)
    {
        DialogueText.text = "";
        foreach (char letter in  Dialogue.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;
        }
    }
}

