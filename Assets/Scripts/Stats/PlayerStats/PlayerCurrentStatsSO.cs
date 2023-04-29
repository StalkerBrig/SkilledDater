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
    public Dictionary<StatTypes, StatCalcInfo> instanceStats = new Dictionary<StatTypes, StatCalcInfo>();

    public void InitalizeStats()
    {
        foreach (StatTypes key in Enum.GetValues(typeof(StatTypes)))
        {
            StatCalcInfo tmp = new StatCalcInfo(0);

            if ((StatTypeTypes)key >= StatTypeTypes.percentageBasedStats && (StatTypeTypes)key < StatTypeTypes.infoStats)
            {
                tmp.calcInfo = StatCalculationType.percentage;
            }
            else
            {
                tmp.calcInfo = StatCalculationType.flat;
            }

            instanceStats[key] = tmp;
            calcInstanceStats[key] = new Dictionary<StatModTypes, float>();

            foreach (StatModTypes modType in Enum.GetValues(typeof(StatModTypes)))
            {
                calcInstanceStats[key][modType] = (int)0;
            }
            
        }

        StatCalcInfo className = new StatCalcInfo(0, StatCalculationType.info);
        instanceStats[StatTypes.className] = className;

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

    public void ModifyStats(StatTypes statName, StatModifier statModifier)
    {
        calcInstanceStats[statName][statModifier.modType] += statModifier.value;
        CalculateStats();
    }

    public void CalculateStats()
    {
        //TODO: Need to add a way to identify percentage based stats (crit damage) vs non percentage based stats (strength)
        foreach (StatTypes statType in Enum.GetValues(typeof(StatTypes)))
            if ((int)statType == (int)StatTypeTypes.infoStats) { continue; }
            else
            { 
                float statTotal = 0;
                float percentageAddTotal = 0;
                float percentageMultTotal = 0;

                percentageAddTotal += CalculateSecondaryStats(statType);

                foreach (StatModTypes statMod in Enum.GetValues(typeof(StatModTypes)))
                {

                    if ((int)statMod == (int)StatTypeTypes.infoStats) { continue; }
                    else if (statMod == StatModTypes.percentAdd || statMod == StatModTypes.percentBase)
                    {
                        percentageAddTotal += calcInstanceStats[statType][statMod];
                    }
                    else if (statMod == StatModTypes.percentMult)
                    {
                        percentageMultTotal = calcInstanceStats[statType][statMod];

                    }
                    else
                    {
                        statTotal += calcInstanceStats[statType][statMod];
                    }
                    
                }

                if (instanceStats[statType].calcInfo == StatCalculationType.flat)
                {
                    instanceStats[statType].value = statTotal;
                    instanceStats[statType].value *= (((100 + percentageAddTotal)/100) * ((100 + percentageMultTotal) / 100));
                }
                else if (instanceStats[statType].calcInfo == StatCalculationType.percentage)
                {
                    instanceStats[statType].value = statTotal + percentageAddTotal;
                    instanceStats[statType].value *= ((100 + percentageMultTotal) / 100);
                }

            }
    }

    public float CalculateSecondaryStats(StatTypes statType)
    {
        if ((int)statType == (int)StatTypes.critDamage)
        {
            return (float)(instanceStats[StatTypes.strength].value * .3);
        }

        return 0;
    }


    public float GetCurrentStatValue(StatTypes stat)
    {
        return instanceStats[stat].value;
    }


    public string GetClassName()
    {
        int classNamePre = ((int)GetCurrentStatValue(StatTypes.className));
        string classNamePost = Enum.GetName(typeof(ClassTypes), classNamePre);

        return classNamePost;
    }
    
}