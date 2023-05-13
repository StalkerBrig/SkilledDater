using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacks : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 30;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        rb.velocity = new Vector2(projectileSpeed, 0);
        rb.gravityScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Enemy")
        {
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(10);
            }
        }
    }
}
