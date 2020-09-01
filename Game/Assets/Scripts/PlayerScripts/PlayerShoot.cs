using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    public bool canShoot;

    private void Awake()
    {
        canShoot = false;
    }

    void Update()
    {
        if (canShoot)
        {
            Shoot();
            StartCoroutine(ShootTimer());
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject fireBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            fireBullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
        }
    }

    IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(5f);
        canShoot = false;
    }
}
