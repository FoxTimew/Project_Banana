﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : MonoBehaviour
{
    public float vitesse = 1f;

    float dashingTimeElapsed;

    public bool isDashing, isAttacking;

    public AnimationCurve dashCurve = AnimationCurve.Constant(0f, 0.25f, 1f);

    Vector2 moveInput;

    Vector3 movement, currentDirection, dashCharmObject;

    Rigidbody2D pcRB;

    public Transform attackPoint;

    public Attack attack;

    public Animator anim;

    [SerializeField]
    GameObject dashObject;

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
        AttackPointPosition();
        DashUpdate();
        AttackUpdate();
        Move();
    }

    void InputHandler()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        movement = Vector3.Normalize(moveInput) * vitesse;

        if (Input.GetButtonDown("Dash")) DashTest();

        if (Input.GetButtonDown("Attack")) AttackTest();

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
        dashCharmObject = this.transform.position;
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
            Object.Instantiate(dashObject, dashCharmObject + (transform.position - dashCharmObject), transform.rotation);
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
        pcRB.velocity = movement * Time.fixedDeltaTime;
        AnimationControler();
    }

    void AnimationControler()
    {
        if (movement.normalized.x > 0f && movement.normalized.y > -0.5 && movement.normalized.y < 0.5f)
        {
            anim.SetBool("RUN_Right", true);
            anim.SetBool("RUN_Left", false);
            anim.SetBool("RUN_Backward", false);
            anim.SetBool("RUN_Up", false);
        }
        
        if (movement.normalized.x < 0f && movement.normalized.y >= -0.5f && movement.normalized.y <= 0.5f)
        {
            anim.SetBool("RUN_Left", true);
            anim.SetBool("RUN_Backward", false);
            anim.SetBool("RUN_Right", false);
            anim.SetBool("RUN_Up", false);
        }

        if (movement.normalized.x > -0.5f && movement.normalized.x < 0.5f && movement.normalized.y < 0f)
        {
            anim.SetBool("RUN_Backward", true);
            anim.SetBool("RUN_Left", false);
            anim.SetBool("RUN_Right", false);
            anim.SetBool("RUN_Up", false);
        }

        if (movement.normalized.x > -0.5f && movement.normalized.x < 0.5f && movement.normalized.y > 0f)
        {
            anim.SetBool("RUN_Up", true);
            anim.SetBool("RUN_Backward", false);
            anim.SetBool("RUN_Left", false);
            anim.SetBool("RUN_Right", false);
        }

        /*if (movement.normalized.x > 0f && movement.normalized.y > 0f)
        { 
        
        }

        if (movement.normalized.x > 0f && movement.normalized.y < 0f)
        { 
        
        }

        if (movement.normalized.x < 0f && movement.normalized.y > 0f)
        {

        }

        if (movement.normalized.x < 0f && movement.normalized.y < 0f)
        {

        }*/
        if (movement.x == 0f && movement.y == 0f) ResetAnimation();
    }

    void ResetAnimation()
    {
        anim.SetBool("RUN_Right", false);
        anim.SetBool("RUN_Left", false);
        anim.SetBool("RUN_Up", false);
        anim.SetBool("RUN_Backward", false);
    }
}
