using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : Node
{
    public float raycastlenght = 0.1f;
    public Vector3 height = new Vector3(0, 2, 0);



    public override void Execute()
    {

        #region Attributes

        if (BT.waypoint && BT.waypoints.Length > 0)
        {
            BT.enemy.SetInteger("enemyanm", 1);
            //Debug.Log("patrol" + state);

            BT.idle = false;



            //  BT.transform.LookAt(new Vector3(BT.waypoints[BT.target].transform.position.x, BT.player.transform.forward.y, BT.waypoints[BT.target].transform.position.z));




            Vector3 directin = (BT.waypoints[BT.target].transform.position - BT.transform.position).normalized;
            Vector3 dir = (BT.waypoints[BT.target].transform.position - BT.transform.position).normalized;

            float distance = (BT.waypoints[BT.target].transform.position - BT.transform.position).magnitude;//Vector3.Distance(BT.transform.position, BT.waypoints[BT.target].transform.position);


            //BT.transform.position += BT.steeringforce;






            //Vector3.MoveTowards(BT.transform.position, BT.player.transform.position, BT.speed * BT.deceleration);










            #endregion





            #region Collision Avoidance


            if (Physics.Raycast(BT.transform.position+height, BT.transform.forward+height, out BT.hitInfo, raycastlenght))
            {
                Debug.DrawLine(BT.transform.position, BT.hitInfo.point, Color.blue);
                if (BT.hitInfo.transform != BT.player.transform)
                {
                    dir += BT.hitInfo.normal * BT.avoidanceforce;
                }
            }

            Vector3 left = BT.transform.position;
            Vector3 right = BT.transform.position;
            left.x -= 7;
            right.x += 7;

            if (Physics.Raycast(left+height, BT.transform.forward+height, out BT.hit, raycastlenght))
            {
                Debug.DrawLine(BT.transform.position, BT.hitInfo.point, Color.red);

                if (BT.hitInfo.transform != BT.player.transform)
                {
                    dir += BT.hitInfo.normal * BT.avoidanceforce;
                }
            }
            if (Physics.Raycast(right+height, BT.transform.forward+height, out BT.hitInfo, raycastlenght))
            {
                Debug.DrawLine(BT.transform.position, BT.hitInfo.point, Color.green);
                if (BT.hitInfo.transform != BT.player.transform)
                {
                    dir += BT.hitInfo.normal * BT.avoidanceforce;
                }

            }

            Quaternion rot = Quaternion.LookRotation(dir);
            BT.transform.rotation = Quaternion.Slerp(BT.transform.rotation, rot, Time.deltaTime);

            BT.transform.position += BT.transform.forward * BT.speed * Time.deltaTime * 0.5f;



            #endregion





            #region Arrival Steering Behaviour and Waypoint system
            // Debug.Log(Vector3.Distance(BT.transform.position, BT.waypoints[BT.target].transform.position));
            //  Debug.Log(distance);


            state = Node_State.running;


            //Vector3 directin = (BT.waypoints[BT.target].transform.position - BT.transform.position).normalized;
            //if we are inside the slowradius
            if (distance < BT.slowradius)
            {
                //using newtons laws of motion, v2= u2+ 2as we make 'a' the subject of the formula which will be a=v2-u2/2s
                BT.deceleration = (0 - (BT.speed * BT.speed)) / (2 * BT.slowradius);
                //the speed is clamped between a min and a max
                BT.speed = Mathf.Clamp(BT.speed, 3f, BT.movespeed);
                //we add our deceleration+speed 
                BT.speed += (BT.deceleration / distance);


                // BT.steeringforce = directin.normalized * Time.deltaTime * BT.speed;


                //we then move from our posiition to the target *speed*our deceleration to slow down and 'arrive'

                Vector3.MoveTowards(BT.transform.position, BT.waypoints[BT.target].transform.position, BT.speed * BT.deceleration);

            }


            else
            {

                // if we move onto the next waypoint we need to reset the speed value after changing it and move not using our decelartion
                BT.speed = BT.movespeed;

                // BT.steeringforce = directin.normalized * Time.deltaTime * BT.speed;
                Vector3.MoveTowards(BT.transform.position, BT.waypoints[BT.target].transform.position, BT.speed);

            }


            // BT.transform.position += BT.steeringforce;
            //Vector3.MoveTowards(BT.transform.position, BT.waypoints[BT.target].transform.position, BT.speed);







            // to move to the next waypoint we add to our target which is our index in the array
            if (Vector3.Distance(BT.transform.position, BT.waypoints[BT.target].transform.position) < BT.DISTANCETOWAYPOINT && !BT.isreverse)
            {

                BT.target++;
            }
            if (BT.target == BT.waypoints.Length && !BT.isreverse)
            {
                BT.isreverse = true;
                BT.target -= 1;

            }

            if (Vector3.Distance(BT.transform.position, BT.waypoints[BT.target].transform.position) < BT.DISTANCETOWAYPOINT && BT.isreverse)
            {

                BT.target--;

            }
            if (Vector3.Distance(BT.transform.position, BT.waypoints[0].transform.position) < BT.DISTANCETOWAYPOINT && BT.isreverse)
            {
                BT.target = 0;
                BT.isreverse = false;

            }







            else
            {
                state = Node_State.faliure;
            }









        }



        else
        {
            BT.waypoint = false;
            state = Node_State.faliure;
        }
    }
}

#endregion