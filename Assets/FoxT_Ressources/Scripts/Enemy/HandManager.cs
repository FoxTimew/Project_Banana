using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HandManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] hands, repere;

    [SerializeField]
    DisableCanvas canvas;

    [SerializeField]
    bool[] left;

    [SerializeField]
    Unit[] unit;

    [SerializeField]
    float dureeSlap, dureePoing, creditDelay;

    [SerializeField]
    SellerDetector detector;

    public bool isSlaping, isKicking, waitTime;
    public bool[] mainActive;

    public bool playerIsHere {get { return detector.playerIsHere; } }

    public int mainKilled;

    string currentState;

    [SerializeField]
    string dieAnimation;

    [SerializeField]
    Animator anim;

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Player")
        {
            repere[0].GetComponent<RepereScript>().enabled = true;
            repere[1].GetComponent<RepereScript>().enabled = true;
            waitTime = true;
        }

    }

	void Start()
    {
        canvas = GameObject.Find("SceneTransition").GetComponent<DisableCanvas>();
        detector = this.GetComponentInParent<SellerDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mainKilled == 4) EndGame();
        int choose1;
        int choose2;
        if (playerIsHere)
        {
            if (!waitTime) return;
            if (mainActive[0] && mainActive[1])
            {
                choose1 = Random.Range(0, 2);
            }
            else if (mainActive[0]) choose1 = 0;
            else if (mainActive[1]) choose1 = 1;
            else choose1 = 0;

            if (mainActive[2] && mainActive[3]) choose2 = Random.Range(2, 4);
            else if (mainActive[2]) choose2 = 2;
            else if (mainActive[3]) choose2 = 3;
            else choose2 = 0;

            if (!isSlaping && (mainActive[2] || mainActive[3])) StartCoroutine(SlapStart(choose2));
            if (!isKicking && (mainActive[0] || mainActive[1])) StartCoroutine(PoingStart(choose1));
        }
    }

    IEnumerator SlapStart(int number)
    {
        isSlaping = true;
        StartCoroutine(hands[number].GetComponent<SlapAttaque>().StartAnimation());
        yield return new WaitForSeconds(dureeSlap);
        isSlaping = false;
    }

    IEnumerator PoingStart(int number)
    {
        isKicking = true;
        StartCoroutine(hands[number].GetComponent<PoinAttaque>().AttackStart());
        yield return new WaitForSeconds(dureePoing);
        isKicking = false;
    }


    void EndGame()
    {
        //animation de mort
        //animation de fondu
        ChangeAnimationState(dieAnimation);

        canvas.EndTransition();
        StopAllCoroutines();
        StartCoroutine(CreditDelay());
        Debug.Log("MORT");
    }

    IEnumerator CreditDelay()
    {
        //transition.transitionOn = true;
        yield return new WaitForSeconds(creditDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        anim.Play(newState);
    }
}
