using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] private AudioSource jumpAudio;

    public bool canMove;

    private Rigidbody2D playerBody;
    private Animator anim;
    private bool onGround;
    private bool jump;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            GroundCheck();
            PlayerJump();
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            PlayerWalk();
        }
    }

    void PlayerWalk()
    {
        float key = Input.GetAxisRaw("Horizontal");

        if (key > 0)
        {
            playerBody.velocity = new Vector2(moveSpeed, playerBody.velocity.y);
            changeDirection(1);
        }
        else if (key < 0)
        {
            playerBody.velocity = new Vector2(-moveSpeed, playerBody.velocity.y);
            changeDirection(-1);
        }
        else
        {
            playerBody.velocity = new Vector2(0f, playerBody.velocity.y);           //Get rid of sliding
        }

        anim.SetInteger("speed", Mathf.Abs((int)playerBody.velocity.x));            //Set transition check value (speed) as the player movement for animation
    }

    void changeDirection(int direction)
    {
        Vector3 dirScale = transform.localScale;
        dirScale.x = direction;
        transform.localScale = dirScale;
    }

    void GroundCheck()
    {
        /*Physics2D.Raycast(origin, direction, distance); returns results of the raycast
         Can also add a LayerMask parameter to check detect collisions with objects only of the specified layer type (found beside the tag)*/
        onGround = Physics2D.Raycast(groundCheck.position, Vector3.down, 0.1f, groundLayer);
        if (onGround)
        {
            if (jump)
            {
                jump = false;                                                       //Resetting jump back to false if previously jumped
                anim.SetBool("jump", false);
            }
        }
    }
    void PlayerJump()
    {
        if (onGround)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumpAudio.Play();
                jump = true;
                playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);    //player x-axis velocity (moving or idle), jump force y-axis
                anim.SetBool("jump", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == MyTags.GROUND_TAG)
            this.transform.parent = collision.transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == MyTags.GROUND_TAG)
            this.transform.parent = null;
    }
}
