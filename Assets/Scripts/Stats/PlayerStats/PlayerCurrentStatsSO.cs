using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

[Serializable]
public class BaseClassStatsDict
{
    public string className;
    public BaseStatsSO classSO;
}


[CreateAssetMenu(fileName = "PlayerCurrentStats", menuName = "SkilledDater/Stats/Player/PlayerCurrentStats", order = 0)]
public class PlayerCurrentStatsSO : ScriptableObject
{
    [SerializeField] BaseClassStatsDict[] baseClassStatsDict;

    public Dictionary<StatTypes, Dictionary<StatModTypes, float>> calcInstanceStats = new Dictionary<StatTypes, Dictionary<StatModTypes, float>>();
    public Dictionary<StatTypes, float> instanceStats = new Dictionary<StatTypes, float>();

    public void InitalizeStats()
    {
        foreach (StatTypes key in Enum.GetValues(typeof(StatTypes)))
        {
            //StatModifier tmp = new StatModifier(0, StatModTypes.initStats);
            instanceStats[key] = 0;
            calcInstanceStats[key] = new Dictionary<StatModTypes, float>();

            foreach (StatModTypes modType in Enum.GetValues(typeof(StatModTypes)))
            {
                calcInstanceStats[key][modType] = (int)0;
            }
            
        }

        //StatModifier className = new StatModifier((float)ClassTypes.none, StatModTypes.initStats);
        instanceStats[StatTypes.className] = (float)ClassTypes.none;

        calcInstanceStats[StatTypes.className][StatModTypes.initStats] = (float)ClassTypes.none;

        CalculateStats();
    }

    private void PrintCalcDict()
    {
        foreach (KeyValuePair<StatTypes, Dictionary<StatModTypes, float>> statTypes in calcInstanceStats)
        {
            Debug.Log(statTypes.Key);
            Dictionary<StatModTypes, float> tmpDict = statTypes.Value;

            foreach (KeyValuePair<StatModTypes, float> stats in tmpDict)
            {
                Debug.Log($"Key: {stats.Key}  |  Value: {stats.Value}");
            }

        }
            
    }

    public void SetBaseStats(string className)
    {
        /*
        foreach (var charClass in baseClassStatsDict)
        {
            if (charClass.className == className)
            {
                charClass.classSO.GetBaseStats(instanceStats);
                break;
            }
        }
        */


        foreach (var charClass in baseClassStatsDict)
        {
            if (charClass.className == className)
            {
                charClass.classSO.InputBaseStats(calcInstanceStats);
                break;
            }

        }

        CalculateStats();

    }

    //TODO: Delete Eventually
    public void UpdateSecondaryStats()
    {
        instanceStats[StatTypes.critDamage] = 5 + instanceStats[StatTypes.strength] * (float).3;
    }

    //TODO: Delete Eventually
    public void UpdateBaseStats(StatTypes stat, float value)
    {
        instanceStats[stat] += value;
        UpdateSecondaryStats();
    }

    public void ModifyStats(StatTypes statName, StatModifier statModifier)
    {
        calcInstanceStats[statName][statModifier.modType] += statModifier.value;
        CalculateStats();
    }

    public void CalculateStats()
    {
        //TODO: Need to add a way to identify percentage based stats (crit damage) vs non percentage based stats (strength)
        foreach(StatTypes statType in Enum.GetValues(typeof(StatTypes)))
            if ((int)statType != (int)StatTypeTypes.infoStats)
            {
                float statTotal = 0;
                float percentageTotal = 0;
                float percentageMultTotal = 100;

                percentageTotal += CalculateSecondaryStats(statType);

                foreach (StatModTypes statMod in Enum.GetValues(typeof(StatModTypes)))
                {
                    Debug.Log(statMod);

                    if ((int)statMod != (int)StatTypeTypes.infoStats)
                    {
                        if (statMod == StatModTypes.percentAdd)
                        {
                            percentageTotal += calcInstanceStats[statType][statMod];
                        }
                        else if (statMod == StatModTypes.percentMult)
                        {
                            percentageMultTotal = 1+(calcInstanceStats[statType][statMod]/100);

                        }
                        else
                        {
                            statTotal += calcInstanceStats[statType][statMod];


                        }
                    }
                }

                instanceStats[statType] = statTotal * (percentageTotal * percentageMultTotal);

            }
    }

    public float CalculateSecondaryStats(StatTypes statType)
    {
        if ((int)statType == (int)StatTypes.critDamage)
        {
            return (float)(instanceStats[StatTypes.strength] * .3);
        }

        return 0;
    }


    public float GetCurrentStatValue(StatTypes stat)
    {
        return instanceStats[stat];
    }


    public string GetClassName()
    {
        int classNamePre = ((int)GetCurrentStatValue(StatTypes.className));
        string classNamePost = Enum.GetName(typeof(ClassTypes), classNamePre);

        return classNamePost;
    }


    
}