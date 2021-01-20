using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    GameObject [] barre, barrePleine, prix;
    [SerializeField]
    int[] price;
    public int moula = 0;
    private int index = 0;
    public GameObject boutton;


    bool onClick = false;

    public void OnClick()
    {
        IsPoorCheck();
        Debug.Log("je click");
    }

    public void IsPoorCheck()
    {
        if( moula >= price[index])
        {
            moula -= price[index];
            barrePleine[index].SetActive(true);
            prix[index].SetActive(false);
            prix[index +1].SetActive(true);
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
