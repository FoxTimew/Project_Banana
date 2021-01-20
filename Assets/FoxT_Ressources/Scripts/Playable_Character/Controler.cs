using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Controler : MonoBehaviour
{
    public float vitesse = 1f, dureeAnimationPousseeSpe, repostAnimationDelay;

    float dashingTimeElapsed;

    public bool isDashing, isAttacking, isEjected, isTouched, isDie, refusDeLaMort, refusDeLaMortCheck, waitForDying, isInteracting, isSpecialing, SpecialOn, isParrying, ejectionCancel, isReposting, isDebuging, isInAnimation, isTeleporting;

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

    public int combos { get { return attack.combos; } }
    public bool attackBlocked { get { return attack.attackBlocked; } }

    [SerializeField]
    GameObject DebugCanvas;

    void Start()
    {
        pcRB = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isEjected && !isInAnimation || isTeleporting) InputHandler();
    }

    private void FixedUpdate()
    {
        if (isInAnimation || isTeleporting)
        {
            pcRB.velocity = Vector2.zero;
            return;
        }
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
            ParryUpdate();
            Move();
        }
        else Ejecting();
    }

	private void LateUpdate()
	{
        ejectionCancel = false;
	}

	void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isDebuging)
        {
            DebugCanvas.SetActive(true);
            Time.timeScale = 0f;
            isDebuging = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isDebuging)
        {
            Time.timeScale = 1f;
            isDebuging = false;
            DebugCanvas.SetActive(false);
        }

        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        movement = Vector3.Normalize(moveInput) * vitesse;

        if (Input.GetButtonDown("Dash") && !isAttacking && !SpecialOn) DashTest();

        if (Input.GetButtonDown("Attack") && !SpecialOn) AttackTest();

        if (Input.GetButtonDown("Interaction") && !isAttacking && !SpecialOn && !isDashing) isInteracting = true;
        else isInteracting = false;

        if (Input.GetButtonDown("Special") && !isAttacking && !SpecialOn) isSpecialing = true;
        else if (Input.GetButtonUp("Special") && !SpecialOn)
        {
            isSpecialing = false;
            StartCoroutine(pousseeAnimationDelay());
        }

        if (Input.GetButtonDown("Parade") && !isAttacking && !SpecialOn && !isDashing)
        {
            Debug.Log("lancerTest");
            ParryTest();
        }
        else if (Input.GetButtonUp("Parade") && !isReposting)
        {
            isParrying = false;
        }

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
        if (!isTouched && !isEjected && !isParrying) pcRB.velocity = movement * Time.fixedDeltaTime;

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
        else if ((playerDirection.x > 0 && playerDirection.y <= 0.71f && playerDirection.y >= -0.71f) && !attackBlocked && !SpecialOn &&!isEjected && !isParrying)
        {
            if (isDashing) ChangeAnimationState(animName[15]);
            else ChangeAnimationState(animName[1]);
        }
        else if ((playerDirection.x < 0 && playerDirection.y <= 0.71f && playerDirection.y >= -0.71f) && !attackBlocked == !SpecialOn && !isEjected && !isParrying)
        {
            if (isDashing) ChangeAnimationState(animName[17]);
            else ChangeAnimationState(animName[3]);
        }
        else if ((playerDirection.y > 0 && playerDirection.x <= 0.71f && playerDirection.x >= -0.7f) && !attackBlocked && !SpecialOn && !isEjected && !isParrying)
        {
            if (isDashing) ChangeAnimationState(animName[14]);
            else ChangeAnimationState(animName[0]);
        }
        else if ((playerDirection.y < 0 && playerDirection.x <= 0.71f && playerDirection.x >= -0.71f) && !attackBlocked && !SpecialOn && !isEjected && !isParrying)
        {
            if (isDashing) ChangeAnimationState(animName[16]);
            else ChangeAnimationState(animName[2]);
        }
        else if (playerDirection == Vector2.zero && !attackBlocked && !SpecialOn && !isEjected && !isParrying)
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
        else if (attackBlocked && !SpecialOn && !isEjected && !isParrying)
        {

            pcRB.velocity = Vector2.zero;
            if (lastDirectionState == animName[0])
            {
                // animationUP
                if (combos == 0) ChangeAnimationState(animName[29]);
                else if (combos == 1) ChangeAnimationState(animName[27]);
                else if (combos == 2) ChangeAnimationState(animName[28]);
            }
            else if (lastDirectionState == animName[1])
            {
                // animationRight
                if (combos == 0) ChangeAnimationState(animName[24]);
                else if (combos == 1) ChangeAnimationState(animName[22]);
                else if (combos == 2) ChangeAnimationState(animName[23]);
            }
            else if (lastDirectionState == animName[2])
            {
                // animationDown
                if (combos == 0) ChangeAnimationState(animName[32]);
                else if (combos == 1) ChangeAnimationState(animName[30]);
                else if (combos == 2) ChangeAnimationState(animName[31]);
            }
            else if (lastDirectionState == animName[3])
            {
                // animationLeft
                if (combos == 0) ChangeAnimationState(animName[21]);
                else if (combos == 1) ChangeAnimationState(animName[19]);
                else if (combos == 2) ChangeAnimationState(animName[20]);
            }
        }
        else if (SpecialOn && !isEjected && !isParrying)
        {
            pcRB.velocity = Vector2.zero;
            if (lastDirectionState == animName[0])
            {
                // animationUP
                ChangeAnimationState(animName[33]);
            }
            else if (lastDirectionState == animName[1])
            {
                // animationRight
                ChangeAnimationState(animName[26]);
            }
            else if (lastDirectionState == animName[2])
            {
                // animationDown
                ChangeAnimationState(animName[34]);
            }
            else if (lastDirectionState == animName[3])
            {
                // animationLeft
                ChangeAnimationState(animName[25]);
            }

        }
    }

    public void Ejecting()
    {
        if (isDie || ejectionCancel) return;
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

        //Animation
        if (pousseeDirection == Vector2.up || pousseeDirection == new Vector2(1, 1).normalized)
        {
            // animationUP
            ChangeAnimationState(animName[35]);
        }
        else if (pousseeDirection == Vector2.left || pousseeDirection == new Vector2(1, -1).normalized)
        {
            // animationRight
            ChangeAnimationState(animName[36]);
        }
        else if (pousseeDirection == Vector2.down || pousseeDirection == new Vector2(-1, -1).normalized)
        {
            // animationDown
            ChangeAnimationState(animName[37]);
        }
        else if (pousseeDirection == Vector2.right || pousseeDirection == new Vector2(-1, 1).normalized)
        {
            // animationLeft
            ChangeAnimationState(animName[38]);
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

    IEnumerator pousseeAnimationDelay()
    {
        SpecialOn = true;
        yield return new WaitForSeconds(dureeAnimationPousseeSpe);
        SpecialOn = false;
    }

    void ParryTest()
    {
        if (isParrying) return;
        ParryStart();
    }

    void ParryStart()
    {
        isParrying = true;
    }

    void ParryUpdate()
    {
        if (!isReposting && isParrying)
        {
        Debug.Log("parade");
            pcRB.velocity = Vector2.zero;
            if (lastDirectionState == animName[0])
            {
                // animationUP
                ChangeAnimationState(animName[39]);
            }
            else if (lastDirectionState == animName[1])
            {
                // animationRight
                ChangeAnimationState(animName[40]);
            }
            else if (lastDirectionState == animName[2])
            {
                // animationDown
                ChangeAnimationState(animName[41]);
            }
            else if (lastDirectionState == animName[3])
            {
                // animationLeft
                ChangeAnimationState(animName[42]);
            }
        }
    }

    public IEnumerator RepostAnimationDelay()
    {
        if (lastDirectionState == animName[0])
        {
            // animationUP
            ChangeAnimationState(animName[43]);
        }
        else if (lastDirectionState == animName[1])
        {
            // animationRight
            ChangeAnimationState(animName[44]);
        }
        else if (lastDirectionState == animName[2])
        {
            // animationDown
            ChangeAnimationState(animName[45]);
        }
        else if (lastDirectionState == animName[3])
        {
            // animationLeft
            ChangeAnimationState(animName[46]);
        }
        isReposting = true;
        yield return new WaitForSeconds(repostAnimationDelay);
        isReposting = false;
        isParrying = Input.GetButton("Parade");
    }
}
