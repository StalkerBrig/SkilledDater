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

    public float CalculateDamage()
    {
        float power = Random.Range((int)statManager.GetStatValue(StatTypes.power) * (float).9, (int)statManager.GetStatValue(StatTypes.power) * (float)1.1 + 1);
        return (int)power;
    }
}
