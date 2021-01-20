using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tuto : MonoBehaviour
{
    public GameObject deplacement, attaque, special, parade;
    public Animator animDeplacement, animAttaque, animSpecial, animParade;
    bool zoneDeplacement, zoneAttaque, zoneSpecial, zoneParade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && zoneAttaque == true)
        {
            animAttaque.SetBool("fin", true);
            zoneSpecial = true;
            zoneAttaque = false;
            //zoneDeplacement = true;
            special.SetActive(true);
        }

        else if(Input.GetKeyDown(KeyCode.F) && zoneSpecial == true && zoneAttaque == false)
        {
            animSpecial.SetBool("end", true);
            zoneSpecial = false;
            zoneParade = true;
            parade.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.F) && zoneSpecial == false && zoneParade == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("depalcement");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("zone attaque");
        //deplacement.SetActive(false);
        animDeplacement.SetBool("exit", true);
        attaque.SetActive(true);
        zoneAttaque = true;
    }
}
