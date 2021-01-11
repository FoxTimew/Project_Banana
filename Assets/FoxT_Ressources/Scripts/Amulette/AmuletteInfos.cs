using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletteInfos
{
    public BonusType type;
    public BonusList list;
    public ValueType vtype;
}

public enum BonusType
{
    Bonus,
    Malus
}

public enum BonusList
{ 
    currentHpPC,
    currentHpEnemy,
    hpMaxPC,
    hpMaxEnemy,    
    degatPC,
    degatEnemy,
    degatDash,
    degatPiegePC,
    degatPiegeEnemy,
    puissancePousseePC,
    puissancePousseeEnemy,
    resistancePC,
    resistanceEnemy,
    vittessePC,
    vitesseEnemy,
    tempsParade,
    Loot
}

public enum ValueType
{ 
    pourcent,
    classic
}