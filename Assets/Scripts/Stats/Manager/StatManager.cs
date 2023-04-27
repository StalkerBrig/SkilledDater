using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] private PlayerCurrentStatsSO curStatSO;

    private void Awake()
    {
        curStatSO.InitalizeStats();
    }
    public void SetBaseStats(string className)
    {
        curStatSO.SetBaseStats(className);
    }

    //TODO: Delete eventually
    public void IncreaseStat(StatIncreaseSO statInc)
    {
        StatTypes statName = statInc.statName;
        float value = statInc.increaseValue;
        curStatSO.UpdateBaseStats(statName, value);
    }

    public void AddPassiveSkill(SkillStatsSO skillStatSO)
    {
        foreach (SkillStatInput stats in skillStatSO.statList)
        {
            StatTypes statName = stats.statName;
            float value = stats.value;
            StatModTypes modType = stats.modType;
            object source = stats;

            StatModifier addStats = new StatModifier(value: value, modType: modType, source: source);

            curStatSO.ModifyStats(statName, addStats);
        }
    }

}