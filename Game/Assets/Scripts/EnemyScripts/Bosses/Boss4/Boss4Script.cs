using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Script : MonoBehaviour
{
    public GameObject rock;
    public Transform instantiateRock;

    private Animator anim;
    private string coroutineName = "StartAttack";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(coroutineName);
    }

    void Attack()
    {
        GameObject obj = Instantiate(rock, instantiateRock.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700f), 0f));
    }

    void Idle()
    {
        anim.Play("Idle");
    }

    public void StopBossAttack()
    {
        StopCoroutine(coroutineName);
        enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == MyTags.PLAYER_TAG)
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        anim.Play("Attack");
        StartCoroutine(coroutineName);
    }
}
