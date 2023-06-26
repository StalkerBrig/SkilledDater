using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public int amount { get; private set; }
    public bool isCrit { get; private set; }
    public bool isPoison { get; private set; }
    public int poisonDamage { get; private set; }



    public DamageInfo(int amount, bool isCrit, bool isPoison, int poisonDamage)
    {
        this.amount = amount;
        this.isCrit = isCrit;
        this.isPoison = isPoison;
        this.poisonDamage = poisonDamage;
    }


    public DamageInfo(int amount) : this(amount, false, false, 0) { }
}
public interface IDamageable
{
    void Damage(DamageInfo dmgInfo);
}