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
    public GameObject poisonDamageTextPrefab;

    private bool isPoisoned = false;
    private float poisonDuration = 5;
    private float poisonProcInterval = 1f;
    private float curPoisonTimer;
    private int poisonDamage = 0;
    private bool poisonProcCoroutineReady = false;


    [SerializeField] private float currentHealth;


    void Start()
    {
        currentHealth = stats.maxHealth;
    }

    void Update()
    {
        if (isPoisoned)
        {
            PoisonedTarget();
        }
    }


    public void Damage(DamageInfo dmgInfo)
    {
        currentHealth -= dmgInfo.amount;

        GameObject DamageTextInstance;

        if (dmgInfo.isPoison)
        {
            curPoisonTimer = Time.deltaTime;
            poisonDamage = dmgInfo.poisonDamage;

            if (isPoisoned ==  false)
            {
                isPoisoned = true;
                poisonProcCoroutineReady = true;
            }

        }

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

    private void PoisonedTarget()
    {
        if ( curPoisonTimer > poisonDuration+.01)
        {
            isPoisoned = false;
            return;
        }
        else
        {
            curPoisonTimer += Time.deltaTime;

            if (poisonProcCoroutineReady == true)
            {
                StartCoroutine(ProcHelper(poisonProcInterval));
            }
            
        }

    }

    private IEnumerator ProcHelper(float delayTime)
    {
        poisonProcCoroutineReady = false;

        GameObject DamageTextInstance = Instantiate(poisonDamageTextPrefab, transform);
        DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().text = poisonDamage.ToString();
        currentHealth -= poisonDamage;
        yield return new WaitForSeconds(delayTime);
        poisonProcCoroutineReady = true;
    }
}
