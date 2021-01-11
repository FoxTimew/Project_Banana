using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Amulette", menuName = "Amulette")]
public class AmuletteDescriptions : ScriptableObject
{
	public Sprite sprite;
	public Description[] description;
	[System.Serializable]
	public class Description
	{
		public int niveau = 1;
		public BonusType bonusType;
		public BonusList whichBonus;
		public ValueType typeOfValue;
		public float value;

		public Description(int _niveau, BonusType _type, BonusList _list, ValueType _vtype, float _value)
		{
			niveau = _niveau;
			bonusType = _type;
			whichBonus = _list;
			typeOfValue = _vtype;
			value = _value;
		}

		public string GetTypeName()
		{
			return bonusType.ToString();
		}

		public string GetBonusName()
		{
			return whichBonus.ToString();
		}

		public string GetValueTypeName()
		{
			return typeOfValue.ToString();
		}
	}
}