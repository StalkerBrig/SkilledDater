using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

[Serializable]
public class BaseStatsDict
{
    public string className;
    public BaseStatsSO classSO;
}

[CreateAssetMenu(fileName = "PlayerCurrentStats", menuName = "SkilledDater/Stats/Player/PlayerCurrentStats", order = 0)]
public class PlayerCurrentStatsSO : ScriptableObject
{
    [SerializeField] BaseStatsDict[] baseSODict;

    public Dictionary<StatTypes, float> instanceStats = new Dictionary<StatTypes, float>();

    public void InitalizeStats()
    {
        foreach (StatTypes key in Enum.GetValues(typeof(StatTypes)))
        {
            instanceStats[key] = 0;
        }

        instanceStats[StatTypes.className] = (float)ClassTypes.none;

        UpdateSecondaryStats();

    }

    public void SetBaseStats(string className)
    {
        foreach (var info in baseSODict)
        {
            if (info.className == className)
            {
                info.classSO.GetBaseStats(instanceStats);
            }
        }

        UpdateSecondaryStats();

    }

    public void UpdateSecondaryStats()
    {
        instanceStats[StatTypes.critDamage] = 5 + (float)instanceStats[StatTypes.strength] * (float).3;
    }

    public void UpdateBaseStats(StatTypes stat, float value)
    {
        instanceStats[stat] += value;
        UpdateSecondaryStats();
    }
    
    public float GetCurrentStat(StatTypes stat)
    {
        return instanceStats[stat];
    }

    public string GetClassName()
    {
        int classNamePre = ((int)GetCurrentStat(StatTypes.className));
        string classNamePost = Enum.GetName(typeof(ClassTypes), classNamePre);

        return classNamePost;
    }
}
