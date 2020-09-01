using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.GROUND_TAG)
        {
            Destroy(gameObject);
        }
        else if (collision.tag == MyTags.PLAYER_TAG)
        {
            collision.GetComponent<PlayerDamage>().DealDamage();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
