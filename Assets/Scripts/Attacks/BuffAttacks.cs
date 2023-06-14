using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAttacks : MonoBehaviour
{
    public void SetBuffDuration(float buffDuration)
    {
        print("EH?");
        UnityEngine.Object.Destroy(gameObject, buffDuration);
    }
}
