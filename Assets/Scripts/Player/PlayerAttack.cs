using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Transform attackSpawner;
    [SerializeField] private GameObject projectile;
    float attackSpeedRate;
    float attackTimer;

    private StatManager statManager;
    private void Awake()
    {
        statManager = FindObjectOfType<StatManager>();
        attackSpawner = transform.Find("AttackPosition");
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
            Attack();
            attackTimer = 0;
        }
    }

    private void Attack()
    {
        Instantiate(projectile, attackSpawner.position, attackSpawner.rotation);
    }

    public DamageInfo CalculateDamage(ActiveSkillsSO activeSkillSO = null)
    {
        if (activeSkillSO != null)
        {
            foreach (StatTypes stat in activeSkillSO.statList)
            {
                //TODO: Need to allow AddPassiveSkill to take in a statlist and not just a SO
                //statManager.AddPassiveSkill(stat);
            }
        }
        

        float cur_power = (int)statManager.GetStatValue(StatTypes.power);
        float power_range = Random.Range(cur_power * (float).70, cur_power * (float)1.30 + 1);
        float final_damage;

        bool is_crit = false;
        float cur_critChance = statManager.GetStatValue(StatTypes.critChance);
        float check_if_crit = Random.Range((float)0.0, (float)100.0);

        if (cur_critChance >= check_if_crit)
        {
            is_crit = true;
        }

        if (is_crit)
        {
            float cur_critDamage = statManager.GetStatValue(StatTypes.critDamage);
            final_damage = power_range * (1+cur_critDamage/100);
        }
        else
        {
            final_damage = power_range;
        }

        return new DamageInfo((int)final_damage, is_crit);
    }
}
