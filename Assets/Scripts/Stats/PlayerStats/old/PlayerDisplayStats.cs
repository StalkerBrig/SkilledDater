using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerDisplayStats : MonoBehaviour
{
    [SerializeField] private PlayerCurrentStatsSO statSO;


    public TMP_Text className;
    public TMP_Text health;
    public TMP_Text strength;
    public TMP_Text agility;
    public TMP_Text intelligence; 
    public TMP_Text wisdom;
    public TMP_Text clout;
    public TMP_Text vitality;


    private void Start()
    {
        if (statSO != null) { UpdateStats(); }
        
    }

    public void Update()
    {
        UpdateStats();
    }

    //TODO: Make this more reusable by making the serialized fields a list instead of hard coded 'health', 'strength', etc.
    public void UpdateStats()
    {
        strength.text = ((int)statSO.GetCurrentStatValue(StatTypes.strength)).ToString();
        health.text = ((int)statSO.GetCurrentStatValue(StatTypes.health)).ToString();
        agility.text = ((int)statSO.GetCurrentStatValue(StatTypes.agility)).ToString();
        intelligence.text = ((int)statSO.GetCurrentStatValue(StatTypes.intelligence)).ToString();
        wisdom.text = ((int)statSO.GetCurrentStatValue(StatTypes.wisdom)).ToString();
        clout.text = ((int)statSO.GetCurrentStatValue(StatTypes.clout)).ToString();
        vitality.text = ((int)statSO.GetCurrentStatValue(StatTypes.vitality)).ToString();


        int classNamePre = ((int)statSO.GetCurrentStatValue(StatTypes.className));
        string classNamePost = Enum.GetName(typeof(ClassTypes), classNamePre);
        className.text = classNamePost;
    }

}
