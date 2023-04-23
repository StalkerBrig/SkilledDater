public class StatModifier
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
    }

    public StatModifier(float value, StatModTypes modType) : this (value, modType, (int)modType, null) { }
    public StatModifier(float value, StatModTypes modType, int order) : this(value, modType, order, null) { }
    public StatModifier(float value, StatModTypes modType, object source) : this(value, modType, (int)modType, source) { }

}

public enum StatTypes
{
    strength,
    agility,
    intelligence,
    wisdom,
    clout,
    vitality,
    health,
    className,
    power=100,
    critDamage,
    attackSpeed
}

public enum StatModTypes
{
    initStats,
    baseStats,
    flat,
    percentAdd,
    percentMult,

    info=99999
}

public enum ClassTypes
{
    none=-1,
    warrior,
    thief
}


