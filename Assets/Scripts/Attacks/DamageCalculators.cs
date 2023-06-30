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


        if (tmpActiveSkillSO != null)
        {
            statManager.AddPassiveSkill(activeSkillSO.statList);
        }


        /* ~~~ Physical Damage ~~~ */
        float curPhysicalDamage = (int)statManager.GetStatValue(StatTypes.physicalDamage);
        float curMeleeDamage = (int)statManager.GetStatValue(StatTypes.meleeDamage);
        float curRangedDamage = (int)statManager.GetStatValue(StatTypes.rangedDamage);
        float curThornsDamage = (int)statManager.GetStatValue(StatTypes.thornsDamage);


        /* ~~~ Arcane Damage ~~~ */
        float curArcaneDamage = (int)statManager.GetStatValue(StatTypes.arcaneDamage);
        float curFireDamage = (int)statManager.GetStatValue(StatTypes.fireDamage);
        float curIceDamage = (int)statManager.GetStatValue(StatTypes.iceDamage);
        float curLightningDamage = (int)statManager.GetStatValue(StatTypes.lightningDamage);


        /* ~~~ Divine Damage ~~~ */
        float curDivineDamage = (int)statManager.GetStatValue(StatTypes.divineDamage);
        float curNatureDamage = (int)statManager.GetStatValue(StatTypes.natureDamage);
        float curHolyDamage = (int)statManager.GetStatValue(StatTypes.holyDamage);
        float curVoidDamage = (int)statManager.GetStatValue(StatTypes.voidDamage);


        /* ~~~ Crit ~~~ */
        float curCritChance = statManager.GetStatValue(StatTypes.critChance);
        float curCritDamage = statManager.GetStatValue(StatTypes.critDamage);

        /* ~~~ Ailments ~~~ */
        float curAilmentChance = statManager.GetStatValue(StatTypes.ailmentChance);
        float curAilmentDamage = statManager.GetStatValue(StatTypes.ailmentDamage);

        float curBleedChance = statManager.GetStatValue(StatTypes.bleedChance);
        float curBleedDamage = statManager.GetStatValue(StatTypes.bleedDamage);

        float curBurnChance = statManager.GetStatValue(StatTypes.burnChance);
        float curBurnDamage = statManager.GetStatValue(StatTypes.burnDamage);

        float curSparkChance = statManager.GetStatValue(StatTypes.sparkChance);
        float curSparkDamage = statManager.GetStatValue(StatTypes.sparkDamage);

        float curPoisonChance = statManager.GetStatValue(StatTypes.poisonChance);
        float curPoisonDamage = statManager.GetStatValue(StatTypes.poisonDamage);

        /* ~~~ Potency ~~~ */
        float curDebuffChance = statManager.GetStatValue(StatTypes.debuffChance);
        float curDebuffPotency = statManager.GetStatValue(StatTypes.debuffPotency);

        float curChilledPChance = statManager.GetStatValue(StatTypes.chilledChance);
        float curChilledPotency = statManager.GetStatValue(StatTypes.chilledPotency);

        float curGuidanceChance = statManager.GetStatValue(StatTypes.guidanceChance);
        float curGancePotency = statManager.GetStatValue(StatTypes.guidancePotency);

        float curVoidChance = statManager.GetStatValue(StatTypes.sparkChance);
        float curVoidPotency = statManager.GetStatValue(StatTypes.voidPotency);


        if (tmpActiveSkillSO != null)
        {
            statManager.RemovePassiveSkill(activeSkillSO.statList);
        }


        float physicalDamageRange = Random.Range(curPhysicalDamage * (float).70, curPhysicalDamage * (float)1.30 + 1);
        float finalDamage;

        bool isCrit = false;
        float checkIfCrit = Random.Range((float)0.001, (float)100.0);


        if (curCritChance >= checkIfCrit)
        {
            isCrit = true;
        }

        if (isCrit)
        {
            finalDamage = physicalDamageRange * (1 + curCritDamage / 100);
        }
        else
        {
            finalDamage = physicalDamageRange;
        }

        bool isPoison = false;
        float checkIfPoison = Random.Range((float)0.0, (float)100.0);

        if (curPoisonChance >= checkIfPoison)
        {
            isPoison = true;
        }


        return new DamageInfo((int)finalDamage, isCrit, isPoison, (int)curPoisonDamage);
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
