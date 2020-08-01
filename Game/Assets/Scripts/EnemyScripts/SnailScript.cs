using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Transform navCheck, leftCol, rightCol, topCol;              //Checking for ground or obstacles for navigation
    [SerializeField] private LayerMask playerLayer;

    private Vector3 leftColPosition, rightColPosition;
    private Rigidbody2D myBody;
    private Animator anim;
    private bool leftMove;                                    //To check if the enemy is moving or not (dead)
    private bool canMove;
    private bool stunned;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //To make sure that the positions of these change when the snail changes direction (in change direction)
        leftColPosition = leftCol.position;
        rightColPosition = rightCol.position;
    }
    void Start()
    {
        leftMove = true;                                        //Start moving left when the game starts
        canMove = true;

    }

    void Update()
    {
        if (canMove)
        {
            if (leftMove)
        {
            myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
        }
        else
        {
            myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
        }
        }

        navigationCheck();
    }

    void navigationCheck()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftCol.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightCol.position, Vector2.right, 0.1f, playerLayer);
        Collider2D topHit = Physics2D.OverlapCircle(topCol.position, 0.2f, playerLayer);

        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 5f);
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                    anim.Play("Dead");
                    stunned = true;

                    if(tag == MyTags.BEETLE_TAG)
                    {
                        anim.Play("Dead");
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        if (leftHit)
        {
            if (leftHit.collider.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    //Kill
                }
                else
                {
                    if (tag == MyTags.SNAIL_TAG)
                    {
                        myBody.velocity = new Vector2(15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        if (rightHit)
        {
            if (rightHit.collider.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    //Kill
                }
                else
                if (tag == MyTags.SNAIL_TAG)
                {
                    myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                    StartCoroutine(Dead(3f));
                }
            }
        }

        if (!Physics2D.Raycast(navCheck.position, Vector2.down, 0.1f))
        {
            ChangeDir();
        }
    }

    void ChangeDir()
    {
        leftMove = !leftMove;
        Vector3 tempScale = transform.localScale;

        if (leftMove)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
            leftCol.position = leftColPosition;
            rightCol.position = rightColPosition;
        }
        else
        {
            tempScale.x = -(Mathf.Abs(tempScale.x));
            leftCol.position = rightColPosition;
            rightCol.position = leftColPosition;
        }
        transform.localScale = tempScale;
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTags.BULLET_TAG)
        {
            if(tag == MyTags.BEETLE_TAG)
            {
                anim.Play("Dead");
                canMove = false;
                myBody.velocity = new Vector2(0, 0);
                StartCoroutine(Dead(0.4f));
            }

            if(tag == MyTags.SNAIL_TAG)
            {
                if (!stunned)
                {
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                    anim.Play("Dead");
                    stunned = true;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
