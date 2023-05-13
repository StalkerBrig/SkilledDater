using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

using TMPro;

public class EnemyDamaged : MonoBehaviour, IDamageable
{
    [SerializeField] public EnemyStats stats;

    public GameObject damageTextPrefab;

    private float currentHealth;


    void Start()
    {
        currentHealth = stats.maxHealth;
    }


    public void Damage(float amount)
    {
        currentHealth -= amount;
        GameObject DamageTextInstance = Instantiate(damageTextPrefab, transform);
        DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().text = amount.ToString();

    }
}
