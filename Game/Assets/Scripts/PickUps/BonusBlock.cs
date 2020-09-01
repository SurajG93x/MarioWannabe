using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour
{
    [SerializeField] private AudioSource bonusHit;

    public Transform bottomHit;
    public Transform spawnPoint;
    public GameObject[] spawnCollectibles;
    public GameObject spawnObject;
    public LayerMask playerLayer;

    private Animator anim;
    private Vector3 moveDir = Vector3.up;
    private Vector3 originPos;
    private Vector3 animPos;
    private bool startAnim;
    private bool active = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        animPos = transform.position;
        animPos.y += 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();
        ImpactAnimate();
    }

    void CheckCollision()
    {
        if (active)
        {
            RaycastHit2D hit = Physics2D.Raycast(bottomHit.position, Vector2.down, 0.1f, playerLayer);

            if (hit)
            {
                if (hit.collider.gameObject.tag == MyTags.PLAYER_TAG)
                {
                    spawnObject = spawnCollectibles[Random.Range(0, spawnCollectibles.Length)];
                    Instantiate(spawnObject, spawnPoint.position, Quaternion.identity);
                    bonusHit.Play();
                    Score.scoreCount += 20;
                    anim.Play("BlockOld");
                    startAnim = true;
                    active = false;
                }
            }
        }
    }

    void ImpactAnimate()
    {
        if (startAnim)
        {
            transform.Translate(moveDir * Time.deltaTime);
            if (transform.position.y >= animPos.y)
            {
                moveDir = Vector3.down;
            }
            else if(transform.position.y <= originPos.y)
            {
                startAnim = false;
            }
        }
    }
}
