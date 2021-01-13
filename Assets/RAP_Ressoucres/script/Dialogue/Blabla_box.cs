using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blabla_box : MonoBehaviour
{
    public GameObject DialogueBox;
    public TextEditor DialogueText;
    public int LettersPerSecond;
 
    public static Blabla_box Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }
    public void ShownDialogue(Dialogue dialogue)
    {
        DialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[0]));
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
        
    }
}
