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

    public void IncreaseStat(StatIncreaseSO statInc)
    {
        StatTypes statName = statInc.statName;
        float value = statInc.increaseValue;
        curStatSO.UpdateBaseStats(statName, value);
    }


}
