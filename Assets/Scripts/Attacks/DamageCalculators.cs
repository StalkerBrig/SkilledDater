using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculators : MonoBehaviour
{
    private StatManager statManager;

    [SerializeField] private BuffListSO buffListSO;
    Dictionary<ActiveSkillsSO, Dictionary<SkillStatTypes, float>> buffList;

    private void Awake()
    {
        statManager = FindObjectOfType<StatManager>();
        buffList = buffListSO.buffList;
    }
    public void BuffDamage(ActiveSkillsSO activeSkillSO = null)
    {
        if (activeSkillSO != null)
        {
            if (buffList.ContainsKey(activeSkillSO) == false)
            {
                statManager.AddPassiveSkill(activeSkillSO.statList);
            }

            foreach (ActiveSkillInput activeSkillData in activeSkillSO.activeStatList)
            {
                if (!buffList.ContainsKey(activeSkillSO))
                {
                    buffList[activeSkillSO] = new Dictionary<SkillStatTypes, float>();
                }

                if (activeSkillData.statName == SkillStatTypes.buffDurationSeconds)
                {
                    buffList[activeSkillSO][SkillStatTypes.buffDurationSeconds] = Time.time + activeSkillData.value;
                }
                if (activeSkillData.statName == SkillStatTypes.buffDurationNumAttacks)
                {
                    buffList[activeSkillSO][SkillStatTypes.buffDurationNumAttacks] = activeSkillData.value;
                }
            }

        }
    }


    public DamageInfo CalculateDamage(ActiveSkillsSO activeSkillSO = null)
    {
        ActiveSkillsSO tmpActiveSkillSO = null;


        if (activeSkillSO != null)
        {
            tmpActiveSkillSO = Instantiate(activeSkillSO);
        }

        Dictionary<StatTypes, float> calcDamageDict = new Dictionary<StatTypes, float>();

        if (tmpActiveSkillSO != null)
        {
            statManager.AddPassiveSkill(tmpActiveSkillSO.statList);
        }

        CalcDamageDictSetup(ref calcDamageDict);

        if (tmpActiveSkillSO != null)
        {
            statManager.RemovePassiveSkill(tmpActiveSkillSO.statList);
            Destroy(tmpActiveSkillSO);
        }


        float physicalDamageRange = Random.Range(calcDamageDict[StatTypes.physicalDamage] * (float).70, calcDamageDict[StatTypes.physicalDamage] * (float)1.30 + 1);
        float finalDamage;

        bool isCrit = false;
        float checkIfCrit = Random.Range((float)0.001, (float)100.0);


        if (calcDamageDict[StatTypes.critChance] >= checkIfCrit)
        {
            isCrit = true;
        }

        if (isCrit)
        {
            finalDamage = physicalDamageRange * (1 + calcDamageDict[StatTypes.critDamage] / 100);
        }
        else
        {
            finalDamage = physicalDamageRange;
        }

        bool isPoison = false;
        float checkIfPoison = Random.Range((float)0.0, (float)100.0);

        if (calcDamageDict[StatTypes.poisonChance] >= checkIfPoison)
        {
            isPoison = true;
        }


        return new DamageInfo((int)finalDamage, isCrit, isPoison, (int)calcDamageDict[StatTypes.natureDamage]);
    }


    private void CalcDamageDictSetup(ref Dictionary<StatTypes, float> calcDamageDict)
    {

        /* ~~~ Physical Damage ~~~ */
        float curPhysicalDamage = (int)statManager.GetStatValue(StatTypes.physicalDamage);
        calcDamageDict[StatTypes.physicalDamage] = curPhysicalDamage;
        float curMeleeDamage = (int)statManager.GetStatValue(StatTypes.meleeDamage);
        calcDamageDict[StatTypes.meleeDamage] = curMeleeDamage;
        float curRangedDamage = (int)statManager.GetStatValue(StatTypes.rangedDamage);
        calcDamageDict[StatTypes.rangedDamage] = curRangedDamage;
        float curThornsDamage = (int)statManager.GetStatValue(StatTypes.thornsDamage);
        calcDamageDict[StatTypes.thornsDamage] = curThornsDamage;


        /* ~~~ Arcane Damage ~~~ */
        float curArcaneDamage = (int)statManager.GetStatValue(StatTypes.arcaneDamage);
        calcDamageDict[StatTypes.arcaneDamage] = curArcaneDamage;
        float curFireDamage = (int)statManager.GetStatValue(StatTypes.fireDamage);
        calcDamageDict[StatTypes.fireDamage] = curFireDamage;
        float curIceDamage = (int)statManager.GetStatValue(StatTypes.iceDamage);
        calcDamageDict[StatTypes.iceDamage] = curIceDamage;
        float curLightningDamage = (int)statManager.GetStatValue(StatTypes.lightningDamage);
        calcDamageDict[StatTypes.lightningDamage] = curLightningDamage;



        /* ~~~ Divine Damage ~~~ */
        float curDivineDamage = (int)statManager.GetStatValue(StatTypes.divineDamage);
        calcDamageDict[StatTypes.divineDamage] = curDivineDamage;
        float curNatureDamage = (int)statManager.GetStatValue(StatTypes.natureDamage);
        calcDamageDict[StatTypes.natureDamage] = curNatureDamage;
        float curHolyDamage = (int)statManager.GetStatValue(StatTypes.holyDamage);
        calcDamageDict[StatTypes.holyDamage] = curHolyDamage;
        float curVoidDamage = (int)statManager.GetStatValue(StatTypes.darkDamage);
        calcDamageDict[StatTypes.darkDamage] = curVoidDamage;


        /* ~~~ Crit ~~~ */
        float curCritChance = statManager.GetStatValue(StatTypes.critChance);
        calcDamageDict[StatTypes.critChance] = curCritChance;
        float curCritDamage = statManager.GetStatValue(StatTypes.critDamage);
        calcDamageDict[StatTypes.critDamage] = curCritDamage;

        /* ~~~ Ailments ~~~ */
        float curAilmentChance = statManager.GetStatValue(StatTypes.ailmentChance);
        calcDamageDict[StatTypes.ailmentChance] = curAilmentChance;
        float curAilmentDamage = statManager.GetStatValue(StatTypes.ailmentDamage);
        calcDamageDict[StatTypes.ailmentDamage] = curAilmentDamage;


        float curBleedChance = statManager.GetStatValue(StatTypes.bleedChance);
        calcDamageDict[StatTypes.bleedChance] = curBleedChance;

        float curBleedDamage = statManager.GetStatValue(StatTypes.bleedDamage);
        calcDamageDict[StatTypes.bleedDamage] = curBleedDamage;

        float curBurnChance = statManager.GetStatValue(StatTypes.burnChance);
        calcDamageDict[StatTypes.burnChance] = curBurnChance;
        float curBurnDamage = statManager.GetStatValue(StatTypes.burnDamage);
        calcDamageDict[StatTypes.burnDamage] = curBurnDamage;

        float curSparkChance = statManager.GetStatValue(StatTypes.sparkChance);
        calcDamageDict[StatTypes.sparkChance] = curSparkChance;
        float curSparkDamage = statManager.GetStatValue(StatTypes.sparkDamage);
        calcDamageDict[StatTypes.sparkDamage] = curSparkDamage;

        float curPoisonChance = statManager.GetStatValue(StatTypes.poisonChance);
        calcDamageDict[StatTypes.poisonChance] = curPoisonChance;
        float curPoisonDamage = statManager.GetStatValue(StatTypes.poisonDamage);
        calcDamageDict[StatTypes.poisonDamage] = curPoisonDamage;

        /* ~~~ Potency ~~~ */
        float curDebuffChance = statManager.GetStatValue(StatTypes.debuffChance);
        calcDamageDict[StatTypes.debuffChance] = curDebuffChance;
        float curDebuffPotency = statManager.GetStatValue(StatTypes.debuffPotency);
        calcDamageDict[StatTypes.debuffPotency] = curDebuffPotency;

        float curChilledPChance = statManager.GetStatValue(StatTypes.chilledChance);
        calcDamageDict[StatTypes.chilledChance] = curChilledPChance;
        float curChilledPotency = statManager.GetStatValue(StatTypes.chilledPotency);
        calcDamageDict[StatTypes.chilledPotency] = curChilledPotency;

        float curGuidanceChance = statManager.GetStatValue(StatTypes.guidanceChance);
        calcDamageDict[StatTypes.guidanceChance] = curGuidanceChance;
        float curGancePotency = statManager.GetStatValue(StatTypes.guidancePotency);
        calcDamageDict[StatTypes.guidancePotency] = curGancePotency;

        float curVoidChance = statManager.GetStatValue(StatTypes.sparkChance);
        calcDamageDict[StatTypes.sparkChance] = curVoidChance;
        float curVoidPotency = statManager.GetStatValue(StatTypes.voidPotency);
        calcDamageDict[StatTypes.voidPotency] = curVoidPotency;

        /* ~~~ Bools ~~~ */
        bool canPhysicalDamage = (1 == statManager.GetStatValue(StatTypes.canPhysicalDamage));
        bool canMeleeDamage = (1 == statManager.GetStatValue(StatTypes.canMeleeDamage));
        bool canRangedDamage = (1 == statManager.GetStatValue(StatTypes.canRangedDamage));
        bool canThornsDamage = (1 == statManager.GetStatValue(StatTypes.canThornsDamage));

        bool canArcaneDamage = (1 == statManager.GetStatValue(StatTypes.canArcaneDamage));
        bool canFireDamage = (1 == statManager.GetStatValue(StatTypes.canFireDamage));
        bool canIceDamage = (1 == statManager.GetStatValue(StatTypes.canIceDamage));
        bool canLightningDamage = (1 == statManager.GetStatValue(StatTypes.canLightningDamage));

        bool canDivineDamage = (1 == statManager.GetStatValue(StatTypes.canDivineDamage));
        bool canNatureDamage = (1 == statManager.GetStatValue(StatTypes.canNatureDamage));
        bool canHolyDamage = (1 == statManager.GetStatValue(StatTypes.canHolyDamage));
        bool canVoidDamage = (1 == statManager.GetStatValue(StatTypes.canVoidDamage));

        bool canAilment = (1 == statManager.GetStatValue(StatTypes.canAilment));
        bool canBleed = (1 == statManager.GetStatValue(StatTypes.canBleed));
        bool canBurn = (1 == statManager.GetStatValue(StatTypes.canBurn));
        bool canSpark = (1 == statManager.GetStatValue(StatTypes.canSpark));
        bool canPoison = (1 == statManager.GetStatValue(StatTypes.canPoison));

        bool canDebuff = (1 == statManager.GetStatValue(StatTypes.canDebuff));
        bool canChilled = (1 == statManager.GetStatValue(StatTypes.canChilled));
        bool canGuidance = (1 == statManager.GetStatValue(StatTypes.canGuidance));
        bool canVoid = (1 == statManager.GetStatValue(StatTypes.canVoid));

        bool canCrit = (1 == statManager.GetStatValue(StatTypes.canCrit));
        bool canDodge = (1 == statManager.GetStatValue(StatTypes.canDodge));
        bool canFortify = (1 == statManager.GetStatValue(StatTypes.canFortify));
    }


    /*
    public DamageInfo OLD_CalculateDamage(ActiveSkillsSO activeSkillSO = null)
    {
        if (activeSkillSO != null)
        {
            statManager.AddPassiveSkill(activeSkillSO.statList);
        }

        float curPower = (int)statManager.GetStatValue(StatTypes.power);
        float curCritChance = statManager.GetStatValue(StatTypes.critChance);
        float curCritDamage = statManager.GetStatValue(StatTypes.critDamage);

        float curPoisonChance = statManager.GetStatValue(StatTypes.poisonChance);
        float poisonDamage = statManager.GetStatValue(StatTypes.poisonDamage);

        if (activeSkillSO != null)
        {
            statManager.RemovePassiveSkill(activeSkillSO.statList);
        }


        float power_range = Random.Range(curPower * (float).70, curPower * (float)1.30 + 1);
        float final_damage;

        bool isCrit = false;
        float check_if_crit = Random.Range((float)0.0, (float)100.0);


        if (curCritChance >= check_if_crit)
        {
            isCrit = true;
        }

        if (isCrit)
        {
            final_damage = power_range * (1 + curCritDamage / 100);
        }
        else
        {
            final_damage = power_range;
        }

        bool isPoison = false;
        float checkIfPoison = Random.Range((float)0.0, (float)100.0);

        if (curPoisonChance >= checkIfPoison)
        {
            isPoison = true;
        }


        return new DamageInfo((int)final_damage, isCrit, isPoison, (int)poisonDamage);
    }
    */
}
