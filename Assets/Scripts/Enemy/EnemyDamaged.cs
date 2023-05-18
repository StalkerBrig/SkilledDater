using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

using TMPro;

public class EnemyDamaged : MonoBehaviour, IDamageable
{
    [SerializeField] public EnemyStats stats;

    public GameObject damageTextPrefab;
    public GameObject critDamageTextPrefab;


    private float currentHealth;


    void Start()
    {
        currentHealth = stats.maxHealth;
    }


    public void Damage(DamageInfo dmgInfo)
    {
        currentHealth -= dmgInfo.amount;
        GameObject DamageTextInstance;

        if (dmgInfo.isCrit)
        {
            DamageTextInstance = Instantiate(critDamageTextPrefab, transform);
        }
        else
        {
            DamageTextInstance = Instantiate(damageTextPrefab, transform);
        }

        DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().text = dmgInfo.amount.ToString();


    }
}
