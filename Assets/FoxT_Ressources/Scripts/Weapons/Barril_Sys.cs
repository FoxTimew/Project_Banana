using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barril_Sys : MonoBehaviour
{
    public AnimationCurve pousseeForce;
    public Vector2 direction;
    public float knockBackTimeElapsed, Counter;

    public bool isEjected;

    [SerializeField]
    float dureeAnimation, radius, stunTime;

    [SerializeField]
    int damage;

    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    string explosionState, loadState;

    string currentState;

    Rigidbody2D rb;

    Animator anim;

    private void Awake()
	{
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        rb.velocity = Vector2.zero;
        if (isEjected && (collision.tag == "Enemy" || collision.tag == "Player")) Explosion();
	}

	void FixedUpdate()
    {
        if (isEjected) Ejecting();   
    }

    public void EjectingStart(AnimationCurve force, Vector2 _direction)
    {
        if (isEjected) return;
        knockBackTimeElapsed = 0f;
        pousseeForce = force;
        direction = _direction;
        Ejecting();
    }

    void Ejecting()
    {
        if (!isEjected) StartCoroutine(ExplosionCounter());
        rb.velocity = Vector2.zero;
        isEjected = true;
        PousseeEnnemi();
        rb.AddForce(pousseeForce.Evaluate(knockBackTimeElapsed) * direction * Time.fixedDeltaTime);
        knockBackTimeElapsed += Time.fixedDeltaTime;

        if (knockBackTimeElapsed > pousseeForce.keys[pousseeForce.keys.Length - 1].time)
        {
            isEjected = false;
            knockBackTimeElapsed = 0f;
        }

        ChangeAnimationState(loadState);
    }

    public void Explosion()
    {
        isEjected = false;
        RaycastHit2D[] victime = Physics2D.CircleCastAll(transform.position, radius, Vector2.up, 0f, layerMask);
        foreach (RaycastHit2D col in victime)
        {
            if (col.collider == null) return;
            if (col.collider.tag == "Enemy")
            {
                col.collider.GetComponent<EnemySys>().TakeDamage(damage, stunTime);
            }
            else if (col.collider.tag == "Player")
            {
                col.collider.GetComponent<Health>().TakeDamage(damage, null);
            }
        }
        StartCoroutine(ExplosionDelay());
    }

    IEnumerator ExplosionDelay()
    {
        ChangeAnimationState(explosionState);
        yield return new WaitForSeconds(dureeAnimation);
        Destroy(this.gameObject);
    }

    void PousseeEnnemi()
    {
        float angle = CalculArcTangante(direction);

        if ((angle > -22.5f && angle < 0.0f) || (angle >= 0.0f && angle < 22.5f))
        {
            direction = Vector2.up;
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            direction = new Vector2(1f, 1f).normalized;
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            direction = Vector2.right;
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            direction = new Vector2(1f, -1f).normalized;
        }
        else if ((angle >= 157.5 && angle <= 180) || (angle > -180 && angle < -157.5))
        {
            direction = Vector2.down;
        }
        else if (angle >= -157.5f && angle < -112.5f)
        {
            direction = new Vector2(-1f, -1f).normalized;
        }
        else if (angle >= -112.5f && angle < -67.5f)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = new Vector2(-1f, 1f).normalized;
        }
    }
    float CalculArcTangante(Vector2 position)
    {
        return Mathf.Atan2(position.x, position.y) * Mathf.Rad2Deg;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        anim.Play(newState);
    }

    IEnumerator ExplosionCounter()
    {
        yield return new WaitForSeconds(Counter);
        Explosion();
    }
}
