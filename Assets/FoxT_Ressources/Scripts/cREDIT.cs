using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cREDIT : MonoBehaviour
{
    [SerializeField]
    GameObject[] active;

    [SerializeField] 
    float[] dureeEntreCredit;

    [SerializeField] AudioSource boom;
    void Start()
    {
        StartCoroutine(Credit());
        boom.Play(0);
    }

    IEnumerator Credit()
    {
        yield return new WaitForSeconds(dureeEntreCredit[0]);
        active[0].SetActive(true);
        boom.Play(0);
        //Son
        yield return new WaitForSeconds(dureeEntreCredit[1]);
        active[1].SetActive(true);
        boom.Play(0);
        //Son
        active[0].SetActive(false);
        yield return new WaitForSeconds(dureeEntreCredit[2]);
        active[1].SetActive(false);
        boom.Play(0);
        //Son
        yield return new WaitForSeconds(dureeEntreCredit[3]);
        SceneManager.LoadScene(0);
    }
}
