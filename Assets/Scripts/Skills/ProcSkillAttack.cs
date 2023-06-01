using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcSkillAttack : MonoBehaviour
{
    private PlayerAttack playerAttack;
    [SerializeField] GameObject projectile;
    private int num_attacks = 4;
    private Transform attackSpawner;

    private void Awake()
    {
        playerAttack = FindAnyObjectByType<PlayerAttack>();
        
        attackSpawner = transform.Find("AttackPosition").transform;

    }

    void Start()
    {
        PlayerAttack.OnAttack += ProcAttack;
    }

    public void ProcAttack()
    {
        StartCoroutine(MultiAttackHelper());
    }

    IEnumerator MultiAttackHelper() 
    {
        for (int i = 0; i < num_attacks; i++)
        {
            if (attackSpawner != null)
            {
                playerAttack.Attack(projectile, attackSpawner);

                yield return new WaitForSeconds(.03f);

            }
        }
    }
}
