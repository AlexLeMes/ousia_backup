using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionsController : MonoBehaviour {

    public Transform[] explosionSpawns;
    public GameObject explosionEffect;

    public float minTime = 0f;
    public float maxTime = 15f;
    public float intervalTime = 2f;
    public float tickRate = 1f;

    public bool trigger = false;

    float spawnTime = 0f;
    float destroyTime = 1f;
    
    GameObject explosion;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            trigger = true;
            triggerExplosion();
        }
    }


    public void triggerExplosion()
    {
        spawnTime = Random.Range(minTime, maxTime);

        if(trigger)
        {
            for (int i = 0; i < explosionSpawns.Length; i++)
            {
                Invoke("spawnExplosion", spawnTime);
            }
        }

    }

    public void spawnExplosion()
    {
        for (int x = 0; x < explosionSpawns.Length; x++)
        {
            explosion = Instantiate(explosionEffect, explosionSpawns[x].transform.position, explosionSpawns[x].transform.rotation);
        }

        Destroy(this.gameObject, destroyTime);
    }

}
