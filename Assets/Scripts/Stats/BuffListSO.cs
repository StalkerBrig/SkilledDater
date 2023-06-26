using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffListSO", menuName = "SkilledDater/Buffs/Player Buff List", order = 0)]

public class BuffListSO : ScriptableObject
{
    public Dictionary<ActiveSkillsSO, Dictionary<SkillStatTypes, float>> buffList = new Dictionary<ActiveSkillsSO, Dictionary<SkillStatTypes, float>>();
}
