using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Animator anim;
    private bool animStart, animFinish;
    private bool jumpLeft = true;

    private int jumpNum;
    private string coroutineName = "FrogJump";
    // Start is called before the first frame update

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(FrogJump());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (animFinish && animStart)
        {
            animStart = false;
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }

    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));
        
        animStart = true;
        animFinish = false;
        jumpNum++;

        if (jumpLeft)
        {
            anim.Play("FrogJumpLeft");
        }
        else
        {
            anim.Play("FrogJumpRight");
        }

        StartCoroutine(coroutineName);
    }

    void AnimationFinish()
    {
        animFinish = true;
        if (jumpLeft)
        {
            anim.Play("FrogIdleLeft");
        }
        else
        {
            anim.Play("FrogIdleRight");
        }

        if (jumpNum == 3)
        {
            jumpNum = 0;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;

            jumpLeft = !jumpLeft;
        }
    }
}
