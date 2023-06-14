using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacks : MonoBehaviour
{

    [SerializeField] private float projectileSpeed = 30;
    private Rigidbody2D rb;
    private DamageInfo damageInfo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(projectileSpeed, 0);
        rb.gravityScale = 0;
    }

    public void SetDamage(DamageInfo newDamageInfo)
    {
        damageInfo = newDamageInfo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Enemy")
        {
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                if (damageInfo != null)
                {
                    damageable.Damage(damageInfo);
                }
                else
                {
                    Debug.LogError("damageInfo must be set before firing off MeleeAttacks");
                }
            }
            Destroy(gameObject);
        }
    }

    /*
    public static explicit operator MeleeAttacks(GameObject v)
    {
        throw new NotImplementedException();
    }
    */
}
