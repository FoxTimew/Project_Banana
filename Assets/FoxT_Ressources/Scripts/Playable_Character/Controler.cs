using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : MonoBehaviour
{
    public float vitesse = 1f;

    float dashingTimeElapsed;

    public bool isDashing, isAttacking, isEjected, isTouched, isDie, refusDeLaMort, refusDeLaMortCheck, waitForDying, isInteracting, isSpecialing;

    public AnimationCurve dashCurve = AnimationCurve.Constant(0f, 0.25f, 1f);

    Vector2 moveInput;

    Vector3 movement, dashCharmObject;

    public Vector3 currentDirection;

    Rigidbody2D pcRB;

    public Transform attackPoint;

    public Attack attack;

    public Animator anim;

    public Vector2 pousseeDirection;

    [SerializeField]
    GameObject dashObject;

    [SerializeField]
    string[] animName;

    string currentState, lastDirectionState;

    float knockBackTimeElapsed;

    public AnimationCurve knockBackForce;

    public Vector2 direction;

    void Start()
    {
        pcRB = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        InputHandler();
    }

    private void FixedUpdate()
    {
        if (isDie)
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            ChangeAnimationState(animName[18]);
            isDie = false;
            waitForDying = true;
            return;
        }
        else if (waitForDying)
        {
            if (refusDeLaMortCheck)
            {
                RefusDeLaMort();
                isTouched = false;
                return;
            }
            else return; //EndGame
        }
        if (!isEjected)
        {
            AttackPointPosition();
            DashUpdate();
            AttackUpdate();
            Move();
        }
        else Ejecting();
    }

    void InputHandler()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        movement = Vector3.Normalize(moveInput) * vitesse;

        if (Input.GetButtonDown("Dash")) DashTest();

        if (Input.GetButtonDown("Attack")) AttackTest();

        if (Input.GetButtonDown("Interaction")) isInteracting = true;
        else isInteracting = false;

        if (Input.GetButtonDown("Special")) isSpecialing = true;
        else if(Input.GetButtonUp("Special")) isSpecialing = false;

    }

    void AttackPointPosition()
    {
        Vector3 displacement;
        displacement = currentDirection;
        attackPoint.localPosition = Vector3.Normalize(displacement * 10);
    }

    void DashTest()
    {
        if (isDashing) return;
        DashStart();
    }

    void DashStart()
    {
        dashingTimeElapsed = 0f;
        isDashing = true;
    }

    void DashUpdate()
    {
        if (!isDashing) return;
        if (movement == new Vector3(0f, 0f, 0f)) movement = currentDirection;

        movement = Vector3.Normalize(movement * 10) * dashCurve.Evaluate(dashingTimeElapsed);
        dashingTimeElapsed += Time.deltaTime;

        if (dashingTimeElapsed > dashCurve.keys[dashCurve.keys.Length - 1].time)
        {
            isDashing = false;
        }

    }

    void AttackTest()
    {
        if (isAttacking) return;
        AttackStart();
    }

    void AttackStart()
    {
        isAttacking = true;
    }

    void AttackUpdate()
    {
        if (!isAttacking) return;
        attack.AttackSysteme();
    }
    void Move()
    {
        if (movement != new Vector3(0f, 0f, 0f)) currentDirection = Vector3.Normalize(movement * 10);
        if (!isTouched && !isEjected) pcRB.velocity = movement * Time.fixedDeltaTime;

        //---------------------------------------
        //Modifier Animation
        //---------------------------------------
        Vector2 playerDirection = (movement * 10).normalized;
        if (isTouched)
        {
            pcRB.velocity = Vector2.zero;
            if (lastDirectionState == animName[0])
            {
                ChangeAnimationState(animName[10]);
            }
            else if (lastDirectionState == animName[1])
            {
                ChangeAnimationState(animName[11]);
            }
            else if (lastDirectionState == animName[2])
            {
                ChangeAnimationState(animName[12]);
            }
            else if (lastDirectionState == animName[3])
            {
                ChangeAnimationState(animName[13]);
            }
        }
        else if ((playerDirection.x > 0 && playerDirection.y <= 0.71f && playerDirection.y >= -0.71f))
        {
            if (isDashing) ChangeAnimationState(animName[15]);
            else ChangeAnimationState(animName[1]);
        }
        else if ((playerDirection.x < 0 && playerDirection.y <= 0.71f && playerDirection.y >= -0.71f))
        {
            if (isDashing) ChangeAnimationState(animName[17]);
            else ChangeAnimationState(animName[3]);
        }
        else if ((playerDirection.y > 0 && playerDirection.x <= 0.71f && playerDirection.x >= -0.7f))
        {
            if (isDashing) ChangeAnimationState(animName[14]);
            else ChangeAnimationState(animName[0]);
        }
        else if ((playerDirection.y < 0 && playerDirection.x <= 0.71f && playerDirection.x >= -0.71f))
        {
            if (isDashing) ChangeAnimationState(animName[16]);
            else ChangeAnimationState(animName[2]);
        }
        else if (playerDirection == Vector2.zero)
        {
            if (lastDirectionState == animName[0])
            {
                ChangeAnimationState(animName[4]);
            }
            else if (lastDirectionState == animName[1])
            {
                ChangeAnimationState(animName[5]);
            }
            else if (lastDirectionState == animName[2])
            {
                ChangeAnimationState(animName[6]);
            }
            else if (lastDirectionState == animName[3])
            {
                ChangeAnimationState(animName[7]);
            }
        }
    }

    public void Ejecting()
    {
        if (isDie) return;
        if (knockBackForce.keys.Length == 0) return;
        isEjected = true;
        PousseeEnnemi();
        pcRB.velocity = Vector2.zero;
        pcRB.AddForce(knockBackForce.Evaluate(knockBackTimeElapsed) * pousseeDirection * Time.fixedDeltaTime);
        knockBackTimeElapsed += Time.fixedDeltaTime;

        if (knockBackTimeElapsed > knockBackForce.keys[knockBackForce.keys.Length - 1].time)
        {
            isEjected = false;
            knockBackTimeElapsed = 0f;
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (newState == currentState) return;
        currentState = newState;

        if (currentState == animName[0] || currentState == animName[1] || currentState == animName[2] || currentState == animName[3]) lastDirectionState = currentState;
        if (currentState == animName[10] || currentState == animName[11] || currentState == animName[12] || currentState == animName[13]) StartCoroutine(HitAnimation());

        anim.Play(newState);
    }

    IEnumerator HitAnimation()
    {
        yield return new WaitForSeconds(0.25f);
        isTouched = false;
    }

    void RefusDeLaMort()
    {
        if (!refusDeLaMort) return;
        refusDeLaMortCheck = false;
        ChangeAnimationState(animName[9]);
        StartCoroutine(EndRespawn());
    }
    IEnumerator EndRespawn()
    {
        yield return new WaitForSeconds(1.55f);
        waitForDying = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
    void PousseeEnnemi()
    {
        float angle = CalculArcTangante(pousseeDirection);

        if ((angle > -22.5f && angle < 0.0f) || (angle >= 0.0f && angle < 22.5f))
        {
            pousseeDirection = Vector2.up;
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            pousseeDirection = new Vector2(1f, 1f).normalized;
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            pousseeDirection = Vector2.right;
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            pousseeDirection = new Vector2(1f, -1f).normalized;
        }
        else if ((angle >= 157.5 && angle <= 180) || (angle > -180 && angle < -157.5))
        {
            pousseeDirection = Vector2.down;
        }
        else if (angle >= -157.5f && angle < -112.5f)
        {
            pousseeDirection = new Vector2(-1f, -1f).normalized;
        }
        else if (angle >= -112.5f && angle < -67.5f)
        {
            pousseeDirection = Vector2.left;
        }
        else
        {
            pousseeDirection = new Vector2(-1f, 1f).normalized;
        }
    }
    float CalculArcTangante(Vector2 position)
    {
        return Mathf.Atan2(position.x, position.y) * Mathf.Rad2Deg;
    }
}
