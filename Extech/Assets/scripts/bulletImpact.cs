using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletImpact : MonoBehaviour {

    public GameObject imapctEffect;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemy")
        {
            //LOGIC HERE
        }
        else
        {
            Instantiate(imapctEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
