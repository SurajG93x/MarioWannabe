using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D myBody;
    private Vector3 moveDir = Vector3.left;
    public float moveTime = 3f;

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
        MoveGhost();
    }

    void MoveGhost()
    {
        transform.Translate(moveDir * Time.smoothDeltaTime);
    }

    IEnumerator ChangeDir()
    {
        yield return new WaitForSeconds(moveTime);

        if (moveDir == Vector3.left)
        {
            moveDir = Vector3.right;
            ChangeDir(-1f);
        }
        else
        {
            moveDir = Vector3.left;
            ChangeDir(1f);
        }

        StartCoroutine(coroutineName);
    }

    void ChangeDir(float dir)
    {
        Vector3 tempscale = transform.localScale;
        tempscale.x = dir;
        transform.localScale = tempscale;
    }

    IEnumerator Dead()
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
            GetComponent<BoxCollider2D>().enabled = false;
            Score.scoreCount += 15;

            StartCoroutine(Dead());
            StopCoroutine(ChangeDir());
        }

        if (collision.tag == MyTags.PLAYER_TAG)
        {
            collision.GetComponent<PlayerDamage>().DealDamage();
        }
    }
}
