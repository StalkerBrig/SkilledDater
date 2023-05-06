using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentSO", menuName = "SkilledDater/Equipment/Weapons", order = 0)]

public class EquipmentSO : ScriptableObject
{
    public string equipmentName;
    public List<SkillStatInput> statList;

    public void EnhanceWeapon(EquipmentStatInput enhancementStats)
    {

    }

}


[Serializable]
public class EquipmentStatInput
{
    public string name;
    public StatTypes statName;
    public float value;
    public StatModTypes modType;
}