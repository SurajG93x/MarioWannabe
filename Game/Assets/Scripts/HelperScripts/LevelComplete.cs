using Cinemachine;
using System.Collections;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera vcam;
    [SerializeField] private AudioSource LevelCompleteSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.PLAYER_TAG)
        {
            Debug.Log("GOAL!");
            vcam.gameObject.SetActive(false);
            LevelCompleteSound.Play();
            collision.gameObject.GetComponent<PlayerMove>().canMove = false;
            StartCoroutine(LevelCompleted());
        }
    }

    IEnumerator LevelCompleted()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<InGameUI>().levelComplete();
        Time.timeScale = 0f;
    }
}
