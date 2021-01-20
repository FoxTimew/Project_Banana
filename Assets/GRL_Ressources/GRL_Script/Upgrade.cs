using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    GameObject [] barre, barrePleine, prix;
    [SerializeField]
    int[] price, value;
    public int moula = 0;
    private int index = 0;
    public GameObject boutton;
    public LevelSave save;
    public int upgrade;
    public PlayableCharacterInfos infos;
    public InventoryManager ui;

    bool onClick = false;

	private void Start()
	{
        if (upgrade == 0) index = save.niveauVie;
        else if (upgrade == 1) index = save.damage;
        else if (upgrade == 2) index = save.resistance;
        else if (upgrade == 3) index = save.prix;

        moula = GameObject.Find("Core").GetComponent<Core>().data.cristal;
        for (int i = 0; i < index; i++)
        {
            barrePleine[i].SetActive(true);
            prix[i].SetActive(false);
            prix[i + 1].SetActive(true);
        }
	}

	public void OnClick()
    {
        IsPoorCheck();
        Debug.Log("je click");
    }

    public void IsPoorCheck()
    {
        moula = GameObject.Find("Core").GetComponent<Core>().data.cristal;
        if( moula >= price[index])
        {
            moula -= price[index];
            GameObject.Find("Core").GetComponent<Core>().data.cristal = moula;
            ui.MoneyUpdateAnimation();
            barrePleine[index].SetActive(true);
            prix[index].SetActive(false);
            prix[index +1].SetActive(true);
            if (upgrade == 0)
            {
                save.niveauVie++;
                infos.HP += value[index];
            }
            else if (upgrade == 1)
            {
                save.damage++;
                infos.force += value[index];
            }
            else if (upgrade == 2)
            {
                save.resistance++;
                infos.resitance += value[index];
            }
            else if (upgrade == 3)
            {
                save.prix++;
                infos.reductionPrix = +value[index];
            }
            index++;
        }
        else
        {
            //sinon annim refus
        }

        /*if (index == price.Length)
        {
            boutton.SetActive(false);
        }*/
    }

    

    
}
