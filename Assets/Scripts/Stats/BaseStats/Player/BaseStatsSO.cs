using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BaseStatsSO", menuName = "SkilledDater/Stats/Player/BaseStatsSO", order = 0)]

public class BaseStatsSO : ScriptableObject
{    
    public string className;
    public int health;
    public int strength;
    public int agility;
    public int intelligence;
    public int wisdom;
    public int clout;
    public int vitality;

    public void GetBaseStats(Dictionary<StatTypes, StatModifier> stats) 
    {
        stats[StatTypes.health].value = health;
        stats[StatTypes.health].modType = StatModTypes.initStats;

        stats[StatTypes.strength].value = strength;
        stats[StatTypes.strength].modType = StatModTypes.initStats;

        stats[StatTypes.agility].value = agility;
        stats[StatTypes.agility].modType = StatModTypes.initStats;

        stats[StatTypes.intelligence].value = intelligence;
        stats[StatTypes.intelligence].modType = StatModTypes.initStats;

        stats[StatTypes.wisdom].value = wisdom;
        stats[StatTypes.wisdom].modType = StatModTypes.initStats;

        stats[StatTypes.clout].value = clout;
        stats[StatTypes.clout].modType = StatModTypes.initStats;

        stats[StatTypes.vitality].value = vitality;
        stats[StatTypes.vitality].modType = StatModTypes.initStats;



        try
        {
            ClassTypes classEnum = (ClassTypes)System.Enum.Parse(typeof(ClassTypes), className);
            stats[StatTypes.className].value = (float)classEnum;
        }
        catch 
        {
            stats[StatTypes.className].value = (float)-1;
        }
    }
}

