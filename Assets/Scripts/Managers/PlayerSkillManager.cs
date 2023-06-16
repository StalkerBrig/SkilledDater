using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class PlayerSkillManager : MonoBehaviour
{
    public ActiveSkillsSO activeSkill;

    [SerializeField] public bool isActive = false;

    public static event Action<float> SetCastingTime;

    private static GameObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }

        else if (instance != this)
        {
            Destroy(this);
        }
    }



    public void AddActiveSkill(ActiveSkillsSO newActiveSkill)
    {
        isActive = !isActive;

        if (isActive == false)
        {
            activeSkill = null;
        }
        else
        {
            activeSkill = newActiveSkill;

            foreach (ActiveSkillInput activeSkillData in activeSkill.activeStatList)
            {
                if (activeSkillData.statName == SkillStatTypes.castingTime)
                {
                    print(activeSkillData.value);
                    SetCastingTime?.Invoke(activeSkillData.value);
                }
            }
        }
    }
}
