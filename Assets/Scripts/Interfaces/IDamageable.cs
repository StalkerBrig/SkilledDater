using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public int amount { get; private set; }
    public bool isCrit { get; private set; }
    public bool isPoison { get; private set; }


    public DamageInfo(int amount, bool isCrit, bool isPoison)
    {
        this.amount = amount;
        this.isCrit = isCrit;
        this.isPoison = isPoison;
    }


    public DamageInfo(int amount) : this(amount, false, false) { }
}
public interface IDamageable
{
    void Damage(DamageInfo dmgInfo);
}