using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    // Start is called before the first frame update
    
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject fireBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            fireBullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
        }
    }
}
