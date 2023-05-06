using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEquipment : MonoBehaviour
{

    private EquipmentSO currentEquipment;
    private StatManager statManager;

    [SerializeField] private bool isEquiped = false;

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

        statManager = FindObjectOfType<StatManager>();
    }

    public void EquipItem(EquipmentSO newEquip)
    {
        UnequipItem();

        if (!isEquiped)
        {
            currentEquipment = newEquip;

            statManager.AddWeapon(currentEquipment);
            isEquiped = true;
        }
     
    }

    public void UnequipItem()
    {
        if (currentEquipment != null && isEquiped)
        {
            statManager.RemoveWeapon(currentEquipment);
            isEquiped = false;
        }
    }

}
