using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAttacks : MonoBehaviour
{
    public void SetBuffDuration(float buffDuration)
    {
        UnityEngine.Object.Destroy(gameObject, buffDuration);
    }
}
