using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour {

    //JUST TESTING

    public float health = 10f;

    public GameObject brokenVersion;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "bullet")
        {
            health -= 1;

            if(health <= 0)
            {
                breakObject();
            }
        }
    }

    public void breakObject()
    {
        Instantiate(brokenVersion, transform.position, transform.rotation);

        Destroy(this.gameObject);
    }
}
