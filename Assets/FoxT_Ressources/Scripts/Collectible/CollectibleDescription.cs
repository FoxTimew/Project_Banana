using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectible", menuName = "Collectible")]
public class CollectibleDescription : ScriptableObject
{
    public collectible collectible;

    public DropBourse[] value;

    [System.Serializable]
    public class DropBourse
    {
        public int bourse;
        public int probability;

        public DropBourse(int _bourse, int _probability)
        {
            bourse = _bourse;
            probability = _probability;
        }
    }
}
