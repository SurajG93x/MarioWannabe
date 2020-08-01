using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D myBody;
    private Vector3 moveDir = Vector3.down;

    private string coroutineName = "ChangeDir";

    private void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coroutineName);
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpider();
    }

    void MoveSpider()
    {
        transform.Translate(moveDir * Time.smoothDeltaTime);
    }

    IEnumerator ChangeDir()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        if (moveDir == Vector3.down)
        {
            moveDir = Vector3.up;
            ChangeDir(-0.5f);
        }
        else
        {
            moveDir = Vector3.down;
            ChangeDir(0.5f);
        }

        StartCoroutine(coroutineName);
    }

    void ChangeDir(float dir) {
        Vector3 tempscale = transform.localScale;
        tempscale.y = dir;
        transform.localScale = tempscale;
    }

    IEnumerator SpiderDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.BULLET_TAG)
        {
            anim.Play("Dead");
            myBody.bodyType = RigidbodyType2D.Dynamic;

            StartCoroutine(SpiderDead());
            StopCoroutine(ChangeDir());
        }
    }
}
