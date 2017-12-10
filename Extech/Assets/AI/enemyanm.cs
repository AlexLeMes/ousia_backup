using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyanm : MonoBehaviour {
    public Animator enemy;
    public Behaviour_tree bt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if  (bt.ect.characterOnFire == true || bt.playerspotted==true)
        {
            enemy.SetInteger("enemyanm", 2);
        }
         else if (bt.waypoints.Length>0)
        {
            enemy.SetInteger("enemyanm", 1);
        }
     
         else if (bt.attack == true)
        {
            enemy.SetInteger("enemyanm", 3);
            
        }
        else  if (bt.ect.health <= 0)
        {
            enemy.SetInteger("enemyanm", 4);
        }
       else  if (bt.idle == true)
        {
            enemy.SetInteger("enemyanm", 0);
        }
    }
}
