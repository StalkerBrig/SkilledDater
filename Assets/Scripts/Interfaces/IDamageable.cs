using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public int amount { get; private set; }
    public bool isCrit { get; private set; }

    public DamageInfo(int amount, bool isCrit)
    {
        this.amount = amount;
        this.isCrit = isCrit;
    }


    public DamageInfo(int amount) : this(amount, false) { }
}
public interface IDamageable
{
    void Damage(DamageInfo dmgInfo);
}