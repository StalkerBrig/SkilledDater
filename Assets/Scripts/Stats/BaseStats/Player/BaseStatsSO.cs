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
    public int critDamage;



    //TODO: Delete this later
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

    public void InputBaseStats(Dictionary<StatTypes, Dictionary<StatModTypes, float>> calcInstanceStats)
    {
        calcInstanceStats[StatTypes.health][StatModTypes.baseStats] = health;
        calcInstanceStats[StatTypes.strength][StatModTypes.baseStats] = strength;
        calcInstanceStats[StatTypes.agility][StatModTypes.baseStats] = agility;
        calcInstanceStats[StatTypes.intelligence][StatModTypes.baseStats] = intelligence;
        calcInstanceStats[StatTypes.wisdom][StatModTypes.baseStats] = wisdom;
        calcInstanceStats[StatTypes.clout][StatModTypes.baseStats] = clout;
        calcInstanceStats[StatTypes.vitality][StatModTypes.baseStats] = vitality;

        calcInstanceStats[StatTypes.critDamage][StatModTypes.percentAdd] = critDamage;


        try
        {
            ClassTypes classEnum = GetClassEnum(className);
            calcInstanceStats[StatTypes.className][StatModTypes.info] = (float)classEnum;
        }
        catch
        {
            calcInstanceStats[StatTypes.className][StatModTypes.info] = (float)-1.0;
        }
    }

    public ClassTypes GetClassEnum(string className)
    {
        ClassTypes classEnum = (ClassTypes)System.Enum.Parse(typeof(ClassTypes), className);
        return classEnum;
    }
}

