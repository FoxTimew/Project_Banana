using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public bool isFollowing;
    // Start is called before the first frame update
    void Start()
    {
        isFollowing = true;
        
    }
    private  Func <Vector3> GetcameraFollowPositionFunc;
    public void setup (Func<Vector3> GetcameraFollowPositionFunc)
    {
        this.GetcameraFollowPositionFunc = GetcameraFollowPositionFunc;
    }
    // Update is called once per frame

    public void SetGetCameraFollowPositionFunc(Func<Vector3> GetcameraFollowPositionFunc)
    {
        this.GetcameraFollowPositionFunc = GetcameraFollowPositionFunc;
    }
    void Update()
    {
       
        Vector3 cameraFollowPosition = GetcameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition -transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition , transform.position);
        float cameraMoveSpeed = 2f;

        if (distance > 0 && isFollowing)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;
            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            {
                newCameraPosition = cameraFollowPosition;
            }
            transform.position = newCameraPosition;
           

        }
       

       
    }
}
