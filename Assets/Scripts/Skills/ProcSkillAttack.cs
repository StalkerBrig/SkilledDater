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

    private void Awake()
    {
        playerAttack = FindObjectOfType<PlayerAttack>();

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
            if (playerSkillManager.activeSkill.skillType == ActiveSkillType.buff)
            {
                BuffAttacks buffAttack = playerAttack.Attack(projectile, attackSpawner).GetComponent<BuffAttacks>();
                foreach (ActiveSkillInput activeSkillData in playerSkillManager.activeSkill.activeStatList)
                {
                    if (activeSkillData.statName == SkillStatTypes.buffDurationNumAttacks)
                    {
                        //buffAttack.SetBuffDuration(activeSkillData.value);
                    }
                }

            }
            else
            {
                StartCoroutine(MultiAttackHelper());
            }
        }

    }

    public void ProcBuff()
    {
        Debug.Log("YEAAAAAA");
    }

    IEnumerator MultiAttackHelper() 
    {
        for (int i = 0; i < numAttacks; i++)
        {
            if (attackSpawner != null)
            {
                
                MeleeAttacks meleeAttack = playerAttack.Attack(projectile, attackSpawner).GetComponent<MeleeAttacks>();
                meleeAttack.SetDamage(playerAttack.CalculateDamage(playerSkillManager.activeSkill));

                yield return new WaitForSeconds(.03f);

            }
        }
    }
}
