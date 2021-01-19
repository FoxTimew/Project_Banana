using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    private Transform shake;
    private float ShakeDuration = 0f;
    public float ShakeForce = 0.5f;
    public float dampingSpeed = 1.0f;
    Vector3 initialPosition;

    void Awake()
    {
        if (shake == null)
        {
            shake = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        initialPosition = shake.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ShakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * ShakeForce;

            ShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            ShakeDuration = 0f;
            transform.localPosition = initialPosition;
        }

        TestScreenshake();
    }

    public void TriggerShake()
    {
        ShakeDuration = 0.2f;
    }

    private void TestScreenshake()
    {
       if (Input.GetKeyDown("y"))
        {
            ShakeDuration = 0.3f;
            Debug.Log("yesayz");
        }

    }

}
