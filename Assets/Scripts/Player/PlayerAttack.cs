using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    float attackSpeedRate;
    float attackTimer;

    public static event Action OnAttack;

    private StatManager statManager;
    private void Awake()
    {
        statManager = FindObjectOfType<StatManager>();
    }

    void Start()
    {

        attackSpeedRate = statManager.GetStatValue(StatTypes.attackSpeed);
        attackTimer = 0;
    }

    void Update()
    {
        AttackProc();
    }

    private void AttackProc()
    {
        //TODO: Probably need to make this better.. Something like subscribing
        //       or whatever they call it..
        attackSpeedRate = statManager.GetStatValue(StatTypes.attackSpeed);

        if (attackSpeedRate <= .001)
        {
            return;
        }

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackSpeedRate)
        {
            //Attack();
            OnAttack?.Invoke();
            attackTimer = 0;
        }
    }

    public void Attack(GameObject projectile, Transform attackSpawner)
    {
        Instantiate(projectile, attackSpawner.position, attackSpawner.rotation);
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
            final_damage = power_range * (1+cur_critDamage/100);
        }
        else
        {
            final_damage = power_range;
        }

        return new DamageInfo((int)final_damage, is_crit);
    }
}
