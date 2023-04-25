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

    public Dictionary<StatTypes, StatModifier> instanceStats = new Dictionary<StatTypes, StatModifier>();

    public void InitalizeStats()
    {
        foreach (StatTypes key in Enum.GetValues(typeof(StatTypes)))
        {
            StatModifier tmp = new StatModifier(0, StatModTypes.initStats);
            instanceStats[key] = tmp;
        }

        StatModifier className = new StatModifier((float)ClassTypes.none, StatModTypes.info);
        instanceStats[StatTypes.className] = className;

        UpdateSecondaryStats();

    }

    public void SetBaseStats(string className)
    {
        foreach (var charClass in baseClassStatsDict)
        {
            if (charClass.className == className)
            {
                charClass.classSO.GetBaseStats(instanceStats);
            }
        }

        UpdateSecondaryStats();

    }

    public void UpdateSecondaryStats()
    {
        instanceStats[StatTypes.critDamage].value = 5 + instanceStats[StatTypes.strength].value * (float).3;
    }

    public void UpdateBaseStats(StatTypes stat, float value)
    {
        instanceStats[stat].value += value;
        UpdateSecondaryStats();
    }

    public void CalculateStats(StatTypes statName, StatModifier statModifier)
    {
        //TODO: Need to make this loop through each stat...
        // This probably means I need to make a list of stats and not just directly update the value
        instanceStats[statName].value += statModifier.value;

        UpdateSecondaryStats();

    }

    public float GetCurrentStatValue(StatTypes stat)
    {
        return instanceStats[stat].value;
    }

    public StatModifier GetCurrentStat(StatTypes stat)
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