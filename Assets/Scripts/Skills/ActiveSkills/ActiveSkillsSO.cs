using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ActiveSkillInput
{
    public SkillStatTypes statName;
    public float value;
    public StatModTypes modType;
}

[CreateAssetMenu(fileName = "ActiveSkillsSO", menuName = "SkilledDater/Active Skills/Skills", order = 0)]
public class ActiveSkillsSO : ScriptableObject
{
    public string activeSkillName;
    public List<ActiveSkillInput> statList;
}
