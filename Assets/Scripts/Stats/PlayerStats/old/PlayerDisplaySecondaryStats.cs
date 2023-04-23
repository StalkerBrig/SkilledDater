using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerDisplaySecondaryStats : MonoBehaviour
{
    [SerializeField] private PlayerCurrentStatsSO statSO;

    public TMP_Text critDamage;

    private void Start()
    {
        if (statSO != null) { UpdateStats(); }
        
    }

    public void Update()
    {
        UpdateStats();
    }

    //TODO: Get rid of this after making display stats more generic
    public void UpdateStats()
    {
        critDamage.text = ((int)statSO.GetCurrentStat(StatTypes.critDamage)).ToString()+"%";
    }

}
