using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectible", menuName = "Collectible")]
public class CollectibleDescription : ScriptableObject
{
    public collectible collectible;

    public int chance;
}
