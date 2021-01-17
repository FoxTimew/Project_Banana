using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camera : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera shopVirtualCam;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("cam prete");
        shopVirtualCam.Priority= -25;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shopVirtualCam.Priority= 11;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
