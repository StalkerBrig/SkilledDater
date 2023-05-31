using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcSkillAttack : MonoBehaviour
{
    private PlayerAttack playerAttack;
    [SerializeField] GameObject projectile;
    private int num_attacks = 2;

    private void Awake()
    {
        playerAttack = FindAnyObjectByType<PlayerAttack>();
    }

    void Start()
    {
        PlayerAttack.OnAttack += ProcAttack;
    }

    public void ProcAttack()
    {
        for (int i = 0; i < num_attacks; i++)
        {
            playerAttack.Attack(projectile);
        }
    }
}
