using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    float attackSpeedRate;
    float attackTimer;

    float buffCastingTime;
    float buffTimer;

    public static event Action OnAttack;
    public static event Action OnBuff;

    private List<ActiveSkillsSO> emptyActiveSkillSOList = new List<ActiveSkillsSO>();

    //private Dictionary<ActiveSkillsSO, Dictionary<SkillStatTypes, float>> buffList = new Dictionary<ActiveSkillsSO, Dictionary<SkillStatTypes, float>>();
    [SerializeField] private BuffListSO buffListSO;
    Dictionary<ActiveSkillsSO, Dictionary<SkillStatTypes, float>> buffList;


    //TODO: Sorta jankey.. come back..
    //[SerializeField] private PlayerSkillManager playerSkillManager;

    private StatManager statManager;
    private void Awake()
    {
        statManager = FindObjectOfType<StatManager>();
        buffList = buffListSO.buffList;

    }

    void Start()
    {
        PlayerSkillManager.SetCastingTime += SetCastingTime;
        attackSpeedRate = statManager.GetStatValue(StatTypes.attackSpeed);
        attackTimer = 0;
        buffCastingTime = 0;
        buffTimer = 0;
    }

    void Update()
    {
        AttackProc();
        BuffProc();
        CheckBuffTimers();
    }

    private void AttackProc()
    {
        attackSpeedRate = statManager.GetStatValue(StatTypes.attackSpeed);

        if (attackSpeedRate <= .001)
        {
            attackTimer = 0;
            return;
        }

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackSpeedRate)
        {
            OnAttack?.Invoke();
            attackTimer = 0;
        }
    }

    private void SetCastingTime(float castingTime)
    {
        buffCastingTime = castingTime;
    }

    private void BuffProc()
    {

        if (buffCastingTime <= .001)
        {
            buffTimer = 0;
            return;
        }

        buffTimer += Time.deltaTime;

        if (buffTimer >= buffCastingTime)
        {
            OnBuff?.Invoke();
            buffTimer = 0;
        }
    }

    private void CheckBuffTimers()
    {
        List<ActiveSkillsSO> buffsToRemove = emptyActiveSkillSOList;

        foreach (ActiveSkillsSO curBuff in buffList.Keys)
        {
            if (buffList[curBuff].ContainsKey(SkillStatTypes.buffDurationSeconds))
            {
                if (Time.time >= buffList[curBuff][SkillStatTypes.buffDurationSeconds])
                {
                    if (buffsToRemove == emptyActiveSkillSOList)
                    {
                        buffsToRemove = new List<ActiveSkillsSO>();
                    }

                    buffsToRemove.Add(curBuff);

                }
            }
            if (buffList[curBuff].ContainsKey(SkillStatTypes.buffDurationNumAttacks))
            {
                if (Time.time >= buffList[curBuff][SkillStatTypes.buffDurationSeconds])
                {
                    if (buffsToRemove == emptyActiveSkillSOList)
                    {
                        buffsToRemove = new List<ActiveSkillsSO>();
                    }

                    buffsToRemove.Add(curBuff);

                }
            }

        }

        if (buffsToRemove != emptyActiveSkillSOList)
        {
            foreach (ActiveSkillsSO removeBuff in buffsToRemove)
            {
                statManager.RemovePassiveSkill(removeBuff.statList);
                buffList.Remove(removeBuff);
                print("Removed buff!");
            }

        }
    }

    public GameObject Attack(GameObject projectile, Transform attackSpawner)
    {
        return Instantiate(projectile, attackSpawner.position, attackSpawner.rotation);
    }


}
