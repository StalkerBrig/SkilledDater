using System;

[Serializable] public class StatModifier
{
    public float value;
    public StatModTypes modType;
    public int order;
    public object source;

    public StatModifier(float value, StatModTypes modType, int order, object source)
    {
        this.value = value;
        this.modType = modType;
        this.order = order;
        this.source = source;
        //Add an unlock variable to indicate if unlocked?...
    }

    public StatModifier(float value, StatModTypes modType) : this (value, modType, (int)modType, null) { }
    public StatModifier(float value, StatModTypes modType, int order) : this(value, modType, order, null) { }
    public StatModifier(float value, StatModTypes modType, object source) : this(value, modType, (int)modType, source) { }

}

public enum StatTypeTypes
{
    primaryStats=0,
    secondaryStats=1000,
    infoStats=99999
}
public enum StatTypes
{
    strength=StatTypeTypes.primaryStats,
    agility,
    intelligence,
    wisdom,
    clout,
    vitality,
    

    power=StatTypeTypes.secondaryStats,
    health,
    critDamage,
    attackSpeed,

    className=StatTypeTypes.infoStats
}

public enum StatModTypes
{
    initStats,
    baseStats,
    flat,
    percentBase,
    percentAdd,
    percentMult,

    info = StatTypeTypes.infoStats
}

public enum ClassTypes
{
    none=-1,
    warrior,
    thief
}


