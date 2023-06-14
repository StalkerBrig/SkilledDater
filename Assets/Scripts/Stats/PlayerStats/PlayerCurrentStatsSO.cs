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

    public bool isInitalized = false;

    public void OnEnable()
    {
        isInitalized = false;
    }

    public void InitalizeStats()
    {
        foreach (StatTypes key in Enum.GetValues(typeof(StatTypes)))
        {
            StatCalcInfo tmp = new StatCalcInfo(0);

            if ((StatTypeTypes)key >= StatTypeTypes.percentageBasedStats && (StatTypeTypes)key < StatTypeTypes.percentageBasedStatsEnd)
            {
                if ((StatTypeTypes) key >= StatTypeTypes.debuffPercentageBasedStats && (StatTypeTypes)key < StatTypeTypes.debuffPercentageBasedStatsEnd)
                {
                    tmp.calcInfo = StatCalculationType.debuffPercentage;
                }
                else
                {
                    tmp.calcInfo = StatCalculationType.percentage;
                }
            }
            else
            {
                if ((StatTypeTypes)key >= StatTypeTypes.debuffBasedStats && (StatTypeTypes)key < StatTypeTypes.debuffBasedStatsEnd)
                {
                    tmp.calcInfo = StatCalculationType.debuffFlat;
                }
                else
                {
                    tmp.calcInfo = StatCalculationType.flat;
                }
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

        isInitalized = true;
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


    //TODO: Need to come back and set up debuff damage/etc
    public void CalculateStats()
    {
        foreach (StatTypes statType in Enum.GetValues(typeof(StatTypes)))
            if ((int)statType == (int)StatTypeTypes.infoStats) { continue; }
            else
            {
                float statTotal = 0;
                float percentageAddTotal = 0;
                float percentageMultTotal = 0;
                float skillPercentageAddTotal = 0;
                float skillPercentageMultTotal = 0;

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

                    else if (statMod == StatModTypes.skillPercentAdd)
                    {
                        skillPercentageAddTotal += calcInstanceStats[statType][statMod];
                    }
                    else if (statMod == StatModTypes.skillPercentMult)
                    {
                        skillPercentageMultTotal = calcInstanceStats[statType][statMod];

                    }

                    else
                    {
                        statTotal += calcInstanceStats[statType][statMod];
                    }

                }

                CalculateStatValues(statType, statTotal, percentageAddTotal, percentageMultTotal, skillPercentageAddTotal, skillPercentageMultTotal);

            }
    }

    private void CalculateStatValues(StatTypes statType, float statTotal, float percentageAddTotal, float percentageMultTotal, float skillPercentageAddTotal, float skillPercentageMultTotal)
    {
        if (instanceStats[statType].calcInfo == StatCalculationType.flat || instanceStats[statType].calcInfo == StatCalculationType.debuffFlat)
        {

            instanceStats[statType].value = statTotal;
            if (instanceStats[statType].calcInfo == StatCalculationType.debuffFlat)
            {
                instanceStats[statType].value += CalculateDebuffFlatDamage(statType);
            }
            instanceStats[statType].value *= (((100 + percentageAddTotal) / 100) * ((100 + percentageMultTotal) / 100));

            instanceStats[statType].value *= (((100 + skillPercentageAddTotal) / 100) * ((100 + skillPercentageMultTotal) / 100));
        }
        else if (instanceStats[statType].calcInfo == StatCalculationType.percentage || instanceStats[statType].calcInfo == StatCalculationType.debuffPercentage)
        {
            instanceStats[statType].value = statTotal + percentageAddTotal;
            instanceStats[statType].value *= ((100 + percentageMultTotal) / 100);

            instanceStats[statType].value += skillPercentageAddTotal;
            instanceStats[statType].value *= ((100 + skillPercentageMultTotal) / 100);
        }
    }

    private float CalculateSecondaryStats(StatTypes statType)
    {
        if ((int)statType == (int)StatTypes.critDamage)
        {
            return (float)(instanceStats[StatTypes.strength].value * .3);
        }
        else if ((int)statType == (int)StatTypes.critChance)
        {
            return (float)(instanceStats[StatTypes.agility].value * .3);
        }

        return 0;
    }

    private float CalculateDebuffFlatDamage(StatTypes statType)
    {
        if (statType == StatTypes.poisonDamage)
        {
            return (float)Math.Ceiling(GetCurrentStatValue(StatTypes.power) * (float).05 + GetCurrentStatValue(StatTypes.wisdom)*(float).05);
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