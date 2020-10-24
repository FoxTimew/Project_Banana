using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public CameraFollow cameraFollow;
    public Transform playerTransform;
  
    private  void Start()
    {
        cameraFollow.setup(() => playerTransform.position);
      
    }

 
}
