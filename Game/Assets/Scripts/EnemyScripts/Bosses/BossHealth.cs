using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float damageInterval = 2f;

    private Animator anim;
    private bool canDamage;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(damageInterval);
        canDamage = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canDamage)
        {
            if (collision.tag == MyTags.BULLET_TAG)
            {
                health--;
                if (health <= 0)
                {
                    anim.Play("Dead");
                    GetComponent<Boss4Script>().StopBossAttack();
                    StartCoroutine(DestroyBoss());
                }
                canDamage = false;
                StartCoroutine(WaitForDamage());
            }
        }
    }

    IEnumerator DestroyBoss()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
