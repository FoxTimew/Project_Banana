using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public List<GameObject> Item = new List<GameObject>();
    public List<InventoryUIManager> ItemManage = new List<InventoryUIManager>();

    PlayableCharacterInfos infos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ItemCreation());
    }

    IEnumerator ItemCreation()
    {
        yield return new WaitForSeconds(1f);
        GameObject[] CheckPoint = GameObject.FindGameObjectsWithTag("SellerPosition");
        foreach (GameObject obj in CheckPoint)
        {
            int numerChoisi = Random.Range(0, Item.Count);
            GameObject.Instantiate<GameObject>(Item[numerChoisi], obj.transform.position, obj.transform.rotation);
            //ItemManage[numerChoisi].GetComponent<Animator>.
            Item.Remove(Item[numerChoisi]);
        }
    }


}
