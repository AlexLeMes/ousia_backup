using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTakeDamageTest : MonoBehaviour {

    public GameObject bullet;
    GameObject spawnedBullet;

    Rigidbody bulletRB;

    public float force = 1500f;

    private void Start()
    {
        InvokeRepeating("fireBullet", 1, 2);
    }

    void fireBullet()
    {
        spawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
        bulletRB = spawnedBullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(transform.forward * force);
    }

}
