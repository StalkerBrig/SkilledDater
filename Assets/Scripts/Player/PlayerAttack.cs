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

    float buffCastingTime;
    float buffTimer;

    public static event Action OnAttack;
    public static event Action OnBuff;

    private Dictionary<ActiveSkillsSO, float> buffList = new Dictionary<ActiveSkillsSO, float>();

    //TODO: Sorta jankey.. come back..
    //[SerializeField] private PlayerSkillManager playerSkillManager;

    private StatManager statManager;
    private void Awake()
    {
        statManager = FindObjectOfType<StatManager>();
    }

    void Start()
    {
        PlayerSkillManager.SetCastingTime += SetCastingTime;
        attackSpeedRate = statManager.GetStatValue(StatTypes.attackSpeed);
        attackTimer = 0;
        buffCastingTime = 0;
        buffTimer = 0;
    }

    void Update()
    {
        AttackProc();
        BuffProc();
    }

    private void AttackProc()
    {
        attackSpeedRate = statManager.GetStatValue(StatTypes.attackSpeed);

        if (attackSpeedRate <= .001)
        {
            attackTimer = 0;
            return;
        }

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackSpeedRate)
        {
            OnAttack?.Invoke();
            attackTimer = 0;
        }
    }

    private void SetCastingTime(float castingTime)
    {
        buffCastingTime = castingTime;
    }

    private void BuffProc()
    {

        if (buffCastingTime <= .001)
        {
            buffTimer = 0;
            return;
        }

        buffTimer += Time.deltaTime;

        if (buffTimer >= buffCastingTime)
        {
            OnBuff?.Invoke();
            buffTimer = 0;
        }
    }

    public GameObject Attack(GameObject projectile, Transform attackSpawner)
    {
        return Instantiate(projectile, attackSpawner.position, attackSpawner.rotation);
    }


    public void BuffDamage(ActiveSkillsSO activeSkillSO = null)
    {
        if (activeSkillSO != null)
        {
            if (buffList.ContainsKey(activeSkillSO) == false)
            {
                statManager.AddPassiveSkill(activeSkillSO.statList);
                print("ADDED POISON!");

            }
            //TODO: Probably should go back and change this so it isn't... this foreach stuff
            foreach (ActiveSkillInput activeSkillData in activeSkillSO.activeStatList)
            {
                if (activeSkillData.statName == SkillStatTypes.buffDurationSeconds)
                {
                    buffList[activeSkillSO] = activeSkillData.value;
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

        bool isPoison = false;
        float checkIfPoison = Random.Range((float)0.0, (float)100.0);

        if (curPoisonChance >= checkIfPoison)
        {
            isPoison = true;
        }


        return new DamageInfo((int)final_damage, is_crit, isPoison);
    }
}
