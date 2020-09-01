using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera vcam;
    [SerializeField] private AudioSource gameOverSound;

    private Text lifeText;
    private int lives;
    private bool canDamage;
    private BoxCollider2D playerCollider;

    // Start is called before the first frame update

    private void Awake()
    {
        playerCollider = GetComponent<BoxCollider2D>();

        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lives = 3;
        lifeText.text = "x" + lives;

        canDamage = true;
    }

    public void DealDamage()
    {
       if (canDamage)
        {
            lives--;

            if (lives >= 0)
            {
                lifeText.text = "x" + lives;
            }

            if (lives < 0)
            {
                PlayerDie();
            }

            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == MyTags.HAZARD_TAG)
        {
            PlayerDie();
        }
    }

    void PlayerDie()
    {
        playerCollider.isTrigger = true;
        vcam.gameObject.SetActive(false);
        gameObject.GetComponent<PlayerMove>().canMove = false;
        gameOverSound.Play();
        StartCoroutine(killPlayer());
    }

    public void PlayerHurt()
    {

    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    IEnumerator killPlayer()
    {
        yield return new WaitForSeconds(3f);
        FindObjectOfType<InGameUI>().GameOver();
        gameObject.SetActive(false);
        Time.timeScale = 0f;
    }
}
