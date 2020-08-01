using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Animator anim;
    private Vector3 moveDir = Vector3.left;
    private Vector3 origin;
    private Vector3 movePos;
    [SerializeField] private float speed = 3f;

    public GameObject egg;
    public LayerMask playerLayer;
    private bool attacked;
    private bool canMove;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        origin = transform.position;
        origin.x += 6f;
        movePos = transform.position;
        movePos.x -= 6f;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        BirdFly();
        DropEgg();   
    }

    void BirdFly()
    {
        if (canMove)
        {
            transform.Translate(moveDir * speed * Time.smoothDeltaTime);
            if (transform.position.x >= origin.x)
            {
                moveDir = Vector3.left;
                ChangeDir(0.5f);
            }
            else if (transform.position.x <= movePos.x)
            {
                moveDir = Vector3.right;

                ChangeDir(-0.5f);
            }
        }
    }

    void ChangeDir(float dir)
    {
        Vector3 tempscale = transform.localScale;
        tempscale.x = dir;
        transform.localScale = tempscale;
    }

    void DropEgg()
    {
        if (!attacked)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            {
                Instantiate(egg, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), quaternion.identity);
                attacked = true;
                anim.Play("BirdFly");
            }
        }
    }

    IEnumerator BirdDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.BULLET_TAG)
        {
            anim.Play("BirdDead");
            GetComponent<BoxCollider2D>().isTrigger = true;
            myBody.bodyType = RigidbodyType2D.Dynamic;
            canMove = false;
            StartCoroutine(BirdDead());
        }
    }
}
