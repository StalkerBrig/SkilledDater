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
        //playerSkillManager = FindObjectOfType<PlayerSkillManager>();

        attackSpawner = transform.Find("AttackPosition").transform;


    }

    void Start()
    {
        PlayerAttack.OnAttack += ProcAttack;
    }

    private void Update()
    {
        //TODO: Set this up as a event probably?
        print(playerSkillManager.activeSkill);
        if (playerSkillManager.activeSkill != null)
        {
            projectile = playerSkillManager.activeSkill.skillGFX.GameObject();
            foreach (ActiveSkillInput activeSkillData in playerSkillManager.activeSkill.activeStatList)
            {
                if (activeSkillData.statName == SkillStatTypes.numberOfAttacks)
                {
                    numAttacks = (int)activeSkillData.value;
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
        if (projectile != null)
        {
            StartCoroutine(MultiAttackHelper());
        }
    }

    IEnumerator MultiAttackHelper() 
    {
        for (int i = 0; i < numAttacks; i++)
        {
            if (attackSpawner != null)
            {
                playerAttack.Attack(projectile, attackSpawner);

                yield return new WaitForSeconds(.03f);

            }
        }
    }
}
