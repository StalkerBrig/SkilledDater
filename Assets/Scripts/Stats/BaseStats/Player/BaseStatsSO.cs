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

    public void GetBaseStats(Dictionary<StatTypes, float> stats) 
    {
        stats[StatTypes.health] = health;
        stats[StatTypes.strength] = strength;
        stats[StatTypes.agility] = agility;
        stats[StatTypes.intelligence] = intelligence;
        stats[StatTypes.wisdom] = wisdom;
        stats[StatTypes.clout] = clout;
        stats[StatTypes.vitality] = vitality;


        try
        {
            ClassTypes classEnum = (ClassTypes)System.Enum.Parse(typeof(ClassTypes), className);
            stats[StatTypes.className] = (float)classEnum;
        }
        catch 
        {
            stats[StatTypes.className] = (float)-1;
        }

       //if (className == "warrior")
       //   stats[StatTypes.className] = (float)ClassTypes.warrior;
       //else if (className == "thief")
       //   stats[StatTypes.className] = (float)ClassTypes.thief;
       //else
       //   stats[StatTypes.className] = -1;

    }
}

