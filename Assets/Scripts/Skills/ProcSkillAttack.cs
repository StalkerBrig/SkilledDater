using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProcSkillAttack : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private GameObject projectile;
    //TODO: This might be sorta jankey? Come back to this probably...
    [SerializeField] private PlayerSkillManager playerSkillManager;
    private int numAttacks = 1;
    private Transform attackSpawner;
    private DamageCalculators damageCalculator;

    private void Awake()
    {
        playerAttack = FindObjectOfType<PlayerAttack>();
        damageCalculator = FindObjectOfType<DamageCalculators>();


        attackSpawner = transform.Find("AttackPosition").transform;
    }

    void Start()
    {
        PlayerAttack.OnAttack += ProcAttack;
        PlayerAttack.OnBuff += ProcBuff;

    }

    private void Update()
    {
        //TODO: Set this up as a event probably?
        if (playerSkillManager.activeSkill != null)
        {
            projectile = playerSkillManager.activeSkill.skillGFX.GameObject();

            if (playerSkillManager.activeSkill.skillType == ActiveSkillType.projectile)
            {
                foreach (ActiveSkillInput activeSkillData in playerSkillManager.activeSkill.activeStatList)
                {
                    if (activeSkillData.statName == SkillStatTypes.numberOfAttacks)
                    {
                        numAttacks = (int)activeSkillData.value;
                    }
                }
            }
        }
        else
        {
            projectile = null;
        }
    }

    public void ProcAttack()
    {
        if (projectile != null && this != null)
        {
            if (playerSkillManager.activeSkill.skillType == ActiveSkillType.projectile)
            {
                StartCoroutine(MultiAttackHelper());
            }
        }

    }

    public void ProcBuff()
    {
        //TODO: Probably will want to create a new function for the playerAttack.Attack for buffs.. sometime..
        if (projectile != null && this != null)
        {
            BuffAttacks buffAttack = playerAttack.Attack(projectile, attackSpawner).GetComponent<BuffAttacks>();
            damageCalculator.BuffDamage(playerSkillManager.activeSkill);

            foreach (ActiveSkillInput activeSkillData in playerSkillManager.activeSkill.activeStatList)
            {
                if (activeSkillData.statName == SkillStatTypes.buffDurationSeconds)
                {
                    buffAttack.SetBuffDuration(activeSkillData.value);
                }
            }
        }

    }

    IEnumerator MultiAttackHelper() 
    {
        var tmpProjectile = Instantiate(projectile);

        for (int i = 0; i < numAttacks; i++)
        {
            if (attackSpawner != null)
            {
                MeleeAttacks meleeAttack = playerAttack.Attack(tmpProjectile, attackSpawner).GetComponent<MeleeAttacks>();
                meleeAttack.SetDamage(damageCalculator.CalculateDamage(playerSkillManager.activeSkill));

                //TODO: Don't have the time hard coded probably?
                yield return new WaitForSeconds(.03f);

            }
        }

        Destroy(tmpProjectile);
    }
}
