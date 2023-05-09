using System.Collections;
using System.Collections.Generic;
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

    void Attack()
    {
        
        Instantiate(projectile, attackSpawner.position, attackSpawner.rotation);
    }
}
