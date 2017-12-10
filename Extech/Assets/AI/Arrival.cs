using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrival : MonoBehaviour {
    public float deceleration;
    public Vector3 direction;
    public float speed;
    public Vector3 target;
   public float distance;
  public  float slowradius;
    public Vector3 steeringforce;
	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
        direction = (target - transform.position);
        distance = direction.magnitude;
        if (distance < slowradius)
        {

            deceleration = (0 - (speed * speed)) / (2 * slowradius);
           speed = Mathf.Clamp(speed, 2f, 5f);
            speed += deceleration;
            steeringforce = direction.normalized * Time.deltaTime * speed;
           
            


        }
        else
        {
            steeringforce = direction.normalized * Time.deltaTime * speed;
        }
        
        transform.position += steeringforce;

        




        Vector3.MoveTowards(transform.position, target,speed*deceleration);
        

		
	}
}
