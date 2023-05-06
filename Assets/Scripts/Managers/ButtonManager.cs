using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private GameObject playerEquipmentManager;
    private PlayerEquipment playerEquip;
    // Start is called before the first frame update
    void Start()
    {
        playerEquipmentManager = GameObject.Find("PlayerEquipmentManager");
        playerEquip = playerEquipmentManager.GetComponent<PlayerEquipment>();
    }

    public void EquipItem(EquipmentSO playerEquipment)
    {
        playerEquip.EquipItem(playerEquipment);
    }

    public void UnequipItem()
    {
        playerEquip.UnequipItem();
    }
}
