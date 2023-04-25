using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillStatsSO", menuName = "SkilledDater/Skills/Player/SkillStats", order = 0)]
public class SkillStatsSO : ScriptableObject
{
    public List<SkillStatInput> statList;
}


[Serializable]
public class SkillStatInput
{

    public StatTypes statName;
    public float value;
    public StatModTypes modType;
}