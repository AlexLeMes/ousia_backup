using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunLookat : MonoBehaviour {
    /*
  TODO:

*/

    public GameObject player;
    mouseLookat _mouseLookat;
    public Vector3 target;
    

    public bool canShoot = false;
    bool lookat = true;
    float shootDistance = 5f;
	
	void Update ()
    {
        
        Vector3 mousePos = Input.mousePosition;
         target = Camera.main.ScreenToWorldPoint((new Vector3(mousePos.x, mousePos.y, 100.0f)));
        
        //transform.LookAt(target);

        if (lookat == true)
       {
           transform.LookAt(target);
       }

        shootDistance = Vector3.Distance(target, transform.position);

        if (shootDistance >= 5f)
        {
            canShoot = true;
            lookat = true;
        }
        else if (shootDistance < 5f)
        {
            canShoot = false;
            lookat = false;
        }
    }
}
