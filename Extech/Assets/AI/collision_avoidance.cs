using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_avoidance : MonoBehaviour {
   public  Vector3 target;
    private RaycastHit hitInfo;
    public float speed;
    public float raycastdist;
    public float avoidanceforce;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        
        Vector3 dir = ( target -transform.position).normalized;


        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 5f))
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.blue);

            dir += hitInfo.normal * 50;
            
        }

        Vector3 left = transform.position;
        Vector3 right = transform.position;
        left.x-= 7;
        right.x += 7;

        if (Physics.Raycast(left, transform.forward, out hitInfo, 5f))
        {
            Debug.DrawLine(left, hitInfo.point, Color.red);
                dir += hitInfo.normal * 50;
            
        }
        if (Physics.Raycast(right, transform.forward, out hitInfo, 5f))
        {
            Debug.DrawLine(right, hitInfo.point, Color.yellow);
            dir += hitInfo.normal * 50;
            

        }

        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;



        Debug.Log("yes" + hitInfo);

    }

}

