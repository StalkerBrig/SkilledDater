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


[Serializable]
public class StatCalcInfo
{
    public float value;
    public StatCalculationType calcInfo;

    public StatCalcInfo(float value, StatCalculationType calcInfo)
    {
        this.value = value;
        this.calcInfo = calcInfo;
    }

    public StatCalcInfo(float value) : this(value, StatCalculationType.flat) { }


}

public enum StatCalculationType
{
    flat,
    percentage,
    debuffFlat,
    debuffPercentage,
    info
}
public enum StatTypeTypes
{
    flatBasedStats = 0,


    primaryStats=1,
    primaryStatsEnd=9999,

    secondaryStats=10000,
    secondaryStatsEnd=19999,

    debuffBasedStats=20000,
    debuffBasedStatsEnd=29998,
    

    flatBasedStatsEnd=29999,



    percentageBasedStats=30000,


    secondaryPercentageBasedStats=30001,
    secondaryPercentageBasedStatsEnd=39999,

    debuffPercentageBasedStats=40000,
    debuffPercentageBasedStatsEnd=49999,


    percentageBasedStatsEnd=99998,



    nonInfoStatsEnd = 99999,

    infoStats=100000,
    infoStatsEnd=199999,
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
    attackSpeed,

    poisonDamage=StatTypeTypes.debuffBasedStats,

    critDamage=StatTypeTypes.secondaryPercentageBasedStats,
    critChance,

    poisonChance=StatTypeTypes.debuffPercentageBasedStats,


    className = StatTypeTypes.infoStats
}

public enum StatModTypes
{
    initStats,
    baseStats,
    flat,

    percentBase,
    percentAdd,
    percentMult,

    skillFlat,
    skillPercentAdd,
    skillPercentMult,

    info = StatTypeTypes.infoStats
}

public enum ClassTypes
{
    none=-1,
    warrior,
    thief
}


public enum SkillStatTypes
{
    numberOfAttacks,
    cooldown,
    buffDurationNumAttacks,
    buffDurationSeconds,
    castingTime
}

public enum ActiveSkillType
{
    projectile,
    buff,
    debuff
}


