using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text coinScore;
    public AudioSource audioManager;
    public static int scoreCount;

    // Start is called before the first frame update
    void Start()
    {
        coinScore = GameObject.Find("CoinText").GetComponent<Text>();
        coinScore.text = scoreCount.ToString();
    }

    private void FixedUpdate()
    {
        coinScore.text = scoreCount.ToString();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.COIN_TAG)
        {
            collision.gameObject.SetActive(false);
            scoreCount += 5;
            audioManager.Play();
        }
    }
}
