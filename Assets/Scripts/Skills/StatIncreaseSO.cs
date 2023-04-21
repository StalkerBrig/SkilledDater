using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatIncreaseSO", menuName = "SkilledDater/Skills/Player/StatIncrease", order = 0)]
public class StatIncreaseSO : ScriptableObject
{
    public StatTypes statName;
    public float increaseValue;
}
