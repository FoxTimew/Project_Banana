using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : MonoBehaviour
{
    public float vitesse = 1f;

    float dashingTimeElapsed;

    KeyCode dash = KeyCode.E;

    bool isDashing;

    public AnimationCurve dashCurve = AnimationCurve.Constant(0f, 0.25f, 1f);

    Vector2 moveInput;

    Vector3 movement, currentDirection;

    Rigidbody2D pcRB;

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
        DashUpdate();
        Move();
	}

	void InputHandler()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        movement = Vector3.Normalize(moveInput) * vitesse * Time.deltaTime;

        if (Input.GetKeyDown(dash)) DashTest();

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
        Debug.Log("Sa fonctionne");
        if (movement == new Vector3(0f, 0f, 0f)) movement = currentDirection;

        movement = Vector3.Normalize(movement * 10) * dashCurve.Evaluate(dashingTimeElapsed) * Time.deltaTime;
        dashingTimeElapsed += Time.deltaTime;

        if (dashingTimeElapsed > dashCurve.keys[dashCurve.keys.Length - 1].time)
        {
            isDashing = false;
        }
    }

    void Move()
    {
        if (movement != new Vector3(0f, 0f, 0f)) currentDirection = Vector3.Normalize(movement * 10);
        pcRB.velocity = movement;
    }

    void AnimationControler()
    { 
        //Play animation;
    }
}
