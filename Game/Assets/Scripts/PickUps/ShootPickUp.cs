using System.Collections;
using UnityEngine;

public class ShootPickUp : MonoBehaviour
{
    [SerializeField] private AudioSource pickedUp;
    private Rigidbody2D objectBody;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == MyTags.PLAYER_TAG)
        {
            collision.gameObject.GetComponent<PlayerShoot>().canShoot = true;
            pickedUp.Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
