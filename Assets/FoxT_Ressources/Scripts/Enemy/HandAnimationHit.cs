using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimationHit : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    [SerializeField]
    string hitState;


    public IEnumerator hitAnimation()
    {
        anim.Play(hitState);
        yield return new WaitForSeconds(.4f);
        anim.Play("void");
    }
}
