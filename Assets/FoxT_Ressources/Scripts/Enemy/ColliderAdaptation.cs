using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAdaptation : MonoBehaviour
{
    EnemySys sys;
    string currentState { get { return sys.currentState; } }
    // Start is called before the first frame update
    void Awake()
    {
        sys.GetComponentInParent<EnemySys>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == sys.runLeft || currentState == sys.idleLeft || currentState == sys.attackLeft)
        {
            this.transform.localPosition = new Vector2();
        }
    }
}
