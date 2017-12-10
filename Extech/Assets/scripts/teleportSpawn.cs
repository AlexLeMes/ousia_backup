using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportSpawn : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameController.instance.respawn();
        }
    }

}
