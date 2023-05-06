using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] private PlayerCurrentStatsSO curStatSO;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (!curStatSO.isInitalized)
        {
            curStatSO.InitalizeStats();
        }
    }
    public void SetBaseStats(string className)
    {
        curStatSO.SetBaseStats(className);
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


    //TODO: Make this better/more generic
    public void AddWeapon(EquipmentSO weaponStats)
    {
        foreach (SkillStatInput stats in weaponStats.statList)
        {
            StatTypes statName = stats.statName;
            float value = stats.value;
            StatModTypes modType = stats.modType;
            object source = stats;

            StatModifier addStats = new StatModifier(value: value, modType: modType, source: source);

            curStatSO.ModifyStats(statName, addStats);
        }
    }

    //TODO: Make this better/more generic
    public void RemoveWeapon(EquipmentSO weaponStats)
    {
        foreach (SkillStatInput stats in weaponStats.statList)
        {
            StatTypes statName = stats.statName;
            float value = -stats.value;
            StatModTypes modType = stats.modType;
            object source = stats;

            StatModifier addStats = new StatModifier(value: value, modType: modType, source: source);

            curStatSO.ModifyStats(statName, addStats);
        }

    }

}