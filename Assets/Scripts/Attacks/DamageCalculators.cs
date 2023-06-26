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
        if (activeSkillSO != null)
        {
            statManager.AddPassiveSkill(activeSkillSO.statList);
        }

        float cur_power = (int)statManager.GetStatValue(StatTypes.power);
        float cur_critChance = statManager.GetStatValue(StatTypes.critChance);
        float cur_critDamage = statManager.GetStatValue(StatTypes.critDamage);

        float curPoisonChance = statManager.GetStatValue(StatTypes.poisonChance);
        float poisonDamage = statManager.GetStatValue(StatTypes.poisonDamage);

        if (activeSkillSO != null)
        {
            statManager.RemovePassiveSkill(activeSkillSO.statList);
        }


        float power_range = Random.Range(cur_power * (float).70, cur_power * (float)1.30 + 1);
        float final_damage;

        bool is_crit = false;
        float check_if_crit = Random.Range((float)0.0, (float)100.0);


        if (cur_critChance >= check_if_crit)
        {
            is_crit = true;
        }

        if (is_crit)
        {
            final_damage = power_range * (1 + cur_critDamage / 100);
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


        return new DamageInfo((int)final_damage, is_crit, isPoison, (int)poisonDamage);
    }
}
