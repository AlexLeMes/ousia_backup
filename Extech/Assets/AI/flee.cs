using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flee : Node {

    public float raycastlenght = 10;
    
    public override void Execute()
    {


        if (BT.ect.characterOnFire == true)
        {
            BT.enemy.SetInteger("enemyanm", 2);
            BT.idle = false;
            state = Node_State.success;
            Debug.Log("fleeeeeee");
            // BT.transform.position = Vector3.MoveTowards(BT.transform.position, -BT.player.transform.position, BT.speed);
            BT.waypoint = false;
            BT.tar = -BT.player.transform.position;
            if (Physics.Raycast(BT.transform.position, BT.transform.forward, out BT.hitInfo, raycastlenght))
            {
                Debug.DrawLine(BT.transform.position, BT.hitInfo.point, Color.blue);

                BT.dir += BT.hitInfo.normal * BT.avoidanceforce;

            }

            Vector3 left = BT.transform.position;
            Vector3 right = BT.transform.position;
            left.x -= 7;
            right.x += 7;

            if (Physics.Raycast(left, BT.transform.forward, out BT.hitInfo, raycastlenght))
            {
                Debug.DrawLine(left, BT.hitInfo.point, Color.red);
                BT.dir += BT.hitInfo.normal * BT.avoidanceforce;

            }
            if (Physics.Raycast(right, BT.transform.forward, out BT.hitInfo, raycastlenght))
            {
                Debug.DrawLine(right, BT.hitInfo.point, Color.yellow);
                BT.dir += BT.hitInfo.normal * BT.avoidanceforce;


            }

            Quaternion rot = Quaternion.LookRotation(BT.dir);
            BT.transform.rotation = Quaternion.Slerp(BT.transform.rotation, rot, Time.deltaTime);
            BT.transform.position += BT.transform.forward * BT.movespeed * Time.deltaTime;



           // Debug.Log("yes" + BT.hitInfo);

        }














        else
        {

            BT.ect.characterOnFire = false;
        }
        state = Node_State.faliure;

    }
}





