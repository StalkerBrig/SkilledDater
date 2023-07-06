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
    //NOTE: Bool based stats will use 0 for false 1 for true
    boolBased,
    info
}

//CHANGE THE ORDERS ON THESE AND WE ALL DIEEEEEEE AHHHHHHHHHHHHHHHHHH
public enum StatTypeTypes
{
    flatBasedStats = 0,


    primaryStats=1,
    primaryStatsEnd=999,

    secondaryStats=1000,
    secondaryStatsEnd=1999,

    debuffBasedStats=2000,
    debuffBasedStatsEnd=2999,

    

    flatBasedStatsEnd=29999,

    percentageBasedStats=30000,

    secondaryPercentageBasedStats=30001,
    secondaryPercentageBasedStatsEnd=30999,

    debuffPercentageBasedStats=31000,
    debuffPercentageBasedStatsEnd=31999,

    percentageBasedStatsEnd=69999,



    nonInfoStatsEnd = 99999,

    //NOTE: Bool based stats will use 0 for false 1 for true
    boolStats = 100000,
    boolStatsEnd = 102999,
    
    infoStats=103000,
    infoStatsEnd=103999
}

//CHANGE THE ORDERS ON THESE AND WE ALL DIEEEEEEE AHHHHHHHHHHHHHHHHHH
public enum StatTypes
{
    strength=StatTypeTypes.primaryStats,
    agility,
    intelligence,
    wisdom,
    clout,
    vitality,
    
    maxHealth = StatTypeTypes.secondaryStats,
    attackSpeed,
    castSpeed,
    cooldownSpeed,
    defense,
    dodge,
    fortify,

    physicalDamage,
    meleeDamage,
    rangedDamage,
    thornsDamage,

    arcaneDamage,
    fireDamage,
    iceDamage,
    lightningDamage,

    divineDamage,
    natureDamage,
    holyDamage,
    darkDamage,



    ailmentDamage=StatTypeTypes.debuffBasedStats,
    bleedDamage,
    burnDamage,
    sparkDamage,
    poisonDamage,

    debuffPotency,
    chilledPotency,
    guidancePotency,
    voidPotency,

    critDamage=StatTypeTypes.secondaryPercentageBasedStats,
    critChance,
    dodgeChance,
    fortifyChance,

    ailmentChance = StatTypeTypes.debuffPercentageBasedStats,
    bleedChance,
    burnChance,
    sparkChance,
    poisonChance,

    debuffChance,
    chilledChance,
    guidanceChance,
    voidChance,

    //NOTE: Bool based stats will use 0 for false 1 for true
    canPhysicalDamage = StatTypeTypes.boolStats,
    canMeleeDamage,
    canRangedDamage,
    canThornsDamage,

    canArcaneDamage,
    canFireDamage,
    canIceDamage,
    canLightningDamage,

    canDivineDamage,
    canNatureDamage,
    canHolyDamage,
    canVoidDamage,

    canAilment,
    canBleed,
    canBurn,
    canSpark,
    canPoison,

    canDebuff,
    canChilled,
    canGuidance,
    canVoid,

    canCrit,
    canDodge,
    canFortify,

    className = StatTypeTypes.infoStats,

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

    //NOTE: Bool based stats will use 0 for false 1 for true
    boolBased = StatTypeTypes.boolStats,
    boolCannotBe,
    boolHasToBe,
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


