using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoinAttaque : MonoBehaviour
{
    [SerializeField]
    Transform repere, ombre;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    Collider2D col;

    [SerializeField]
    bool left;

    [SerializeField]
    Animator anim;

    [SerializeField]
    HandAnimationHit hitAnim;

    public float vitesse;
    public float vitesseTranslate;

    [SerializeField]
    string animationName, mortanimation;
    string currentState;

    public Vector2 direction;

    [SerializeField]
    float dureeAnimation, dureeMiseEnPoing, dureeStopMouving, dureeANimationMort;


    bool canMove, animattionStart, stop, dying;

    public bool isStun;

    [SerializeField]
    public int health;

    [SerializeField]
    int number;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Repere") canMove = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Repere") canMove = true;
    }

    void Start()
    {
        canMove = true;
    }


    void Update()
    {
        if (!dying) GoToRepereStart();
    }

    void GoToRepereStart()
    {
        if (animattionStart)
        {
            if (!stop) AnimationTranslate();
            return;
        }
        GoToRepere();
    }


    void GoToRepere()
    {
        float step = vitesse * Time.deltaTime;
        ombre.position = Vector3.MoveTowards(ombre.position, repere.position, step);
        /*if (canMove) rb.velocity = direction.normalized * vitesse * Time.fixedDeltaTime;
        else rb.velocity = Vector2.zero;*/
    }

    void AnimationTranslate()
    {
        float step = vitesseTranslate * Time.deltaTime;
        if (left) ombre.position = Vector3.MoveTowards(ombre.position, new Vector3(ombre.position.x + 10, ombre.position.y, 0f), step);
        else ombre.position = Vector3.MoveTowards(ombre.position, new Vector3(ombre.position.x - 10, ombre.position.y, 0f), step);
        //this.transform.Translate(new Vector3(vitesseTranslate, 0f, 0f));
    }

    public IEnumerator AttackStart()
    {
        if (!dying)
        {
            ChangeAnimationState(animationName);
            stop = false;
            yield return new WaitForSeconds(dureeMiseEnPoing);
            animattionStart = true;
            StartCoroutine(EndAttack());
            StartCoroutine(StopMouve());
        }
        else yield return null;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        anim.Play(newState);
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(dureeAnimation);
        animattionStart = false;
        ChangeAnimationState("void");
    }

    IEnumerator StopMouve()
    {
        yield return new WaitForSeconds(dureeStopMouving);
        stop = true;
    }

    public void TakeDammage(int dammage)
    {
        if (!isStun) return;
        health -= dammage;

        if (health <= 0)
        {
            health = 0;
            StopAllCoroutines();
            Die();
        }
        else 
        {
            StartCoroutine(hitAnim.hitAnimation());
        }
    }

    void Die()
    {
        GameObject.Find("BossManager").GetComponent<HandManager>().mainKilled++;
        GameObject.Find("BossManager").GetComponent<HandManager>().mainActive[number] = false;
        GameObject.Find("BossManager").GetComponent<HandManager>().hands[number] = null;
        ChangeAnimationState(mortanimation);
        StartCoroutine(DieAnimationDelay());
    }

    IEnumerator DieAnimationDelay()
    {
        yield return new WaitForSeconds(dureeANimationMort);
        Destroy(this.gameObject);
    }
}
