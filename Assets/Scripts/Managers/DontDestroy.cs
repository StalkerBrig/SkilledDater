using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static GameObject playerEquipmentManager;
    private static DontDestroy instance;

    private void Awake()
    {
        playerEquipmentManager = GameObject.Find("PlayerEquipmentManager");

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(playerEquipmentManager);
            Destroy(this);

        }
    }
}
