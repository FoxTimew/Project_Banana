using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    GameObject [] barre, barrePleine;
    [SerializeField]
    int[] price;
    public int moula = 0;
    private int index = 0;
    public GameObject boutton;


    bool onClick = false;

    public void OnClick()
    {
        IsPoorCheck();
    }

    public void IsPoorCheck()
    {
        if( moula >= price[index])
        {
            moula -= price[index];
            barrePleine[index].SetActive(true);
            index++;
        }
        else
        {
            //sinon annim refus
        }

        if (index == price.Length)
        {
            boutton.SetActive(false);
        }
    }

    

    
}
