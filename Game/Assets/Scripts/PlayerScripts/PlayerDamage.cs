using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera vcam;
    [SerializeField] AudioSource gameOverSound;

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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                playerCollider.isTrigger = true;
                vcam.gameObject.SetActive(false);
                gameOverSound.Play();
                StartCoroutine(killPlayer());
            }

            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
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
