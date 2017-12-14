using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Node
{
    
    float radius = 5;
    float distance;

    
    float maxavoidforce;
    Vector3 target;
    public float raycastlenght = 0.1f;
   
    public Vector3 height = new Vector3(0, 2, 0);







    public override void Execute()
    {
        BT.playerspotted = false;

        #region Attributes
        //we get the direction to our target.
        Vector3 targetDir = BT.player.transform.position - BT.transform.position;
        Vector3 dir = (BT.player.transform.position - BT.transform.position).normalized;


        Physics.Raycast(BT.transform.position, targetDir.normalized, out BT.hit);

        Debug.DrawRay(BT.transform.position, BT.player.transform.position - BT.transform.position, Color.red);
        //we creat an angle to compare to our 'vision'
        BT.angle = Vector3.Angle(targetDir.normalized, BT.transform.forward);

        #endregion



        #region detecting logic and collision avoidance
        if (Vector3.Distance(BT.transform.position, BT.player.transform.position) < BT.maxdistance || BT.angle <= BT.vision && BT.hitInfo.transform==BT.player.transform || BT.ect.health<BT.ect.MaxHealth && BT.ect.health!=0 )
        {

            BT.enemy.SetInteger("enemyanm", 2);


            BT.idle = false;
            //if the chase is true, then that means our target is the player
            BT.tar = BT.player.transform.position;
            //we shoot a raycast from our forward
            if (Physics.Raycast(BT.transform.position+height, BT.transform.forward+height, out BT.hitInfo, raycastlenght))
            {
                Debug.DrawLine(BT.transform.position, BT.hitInfo.point, Color.blue);
                //to make sure we dont avoid the player 
                if (BT.hitInfo.transform != BT.player.transform)
                {
                    //our new direction becomes the normal of the object hit 
                    dir += BT.hitInfo.normal * BT.avoidanceforce;
                }
            }
            //we creat 2 more vectors to give us more perspective to detect objects on both sides
            Vector3 left = BT.transform.position;
            Vector3 right = BT.transform.position;
            left.x -= 7;
            right.x += 7;

            // we do the same thing as we did before here
            if (Physics.Raycast(left+height, BT.transform.forward+height, out BT.hitInfo, raycastlenght))
            {
                Debug.DrawLine(left, BT.hitInfo.point, Color.red);
                if (BT.hitInfo.transform != BT.player.transform)
                {
                    dir += BT.hitInfo.normal * BT.avoidanceforce;
                }

            }
            if (Physics.Raycast(right+height, BT.transform.forward+height, out BT.hitInfo, raycastlenght))
            {
                Debug.DrawLine(right, BT.hitInfo.point, Color.yellow);
                if (BT.hitInfo.transform != BT.player.transform)
                {
                    dir += BT.hitInfo.normal  * BT.avoidanceforce;

                }


            }

            //we creat a quaternion and its looking at our dir
            if (BT.hitInfo.transform != BT.player.transform)
            {
                Quaternion rot = Quaternion.LookRotation(dir);
                //our rotation will go from our initial rotation to our rot by time.deltatime
                BT.transform.rotation = Quaternion.Slerp(BT.transform.rotation, rot, Time.deltaTime*0.5f);
                //and then our postition will equal our transfrom.forward times our speed times our time.deltatime
                BT.transform.position += BT.transform.forward * BT.chase * Time.deltaTime;
            }
            
           /* if (BT.playerspotted == true)
            {
               BT.transform.position= Vector3.MoveTowards(BT.transform.position, BT.player.transform.position, BT.speed*Time.deltaTime);
            }
            */
            
            #endregion



#region results

            //  Vector3 lookatpos = new Vector3(BT.player.transform.position.x, BT.transform.position.y, BT.player.transform.position.z);

            BT.playerspotted = true;
            //BT.transform.LookAt(lookatpos);

            BT.waypoint = false;
            BT.canmove = true;

           // BT.transform.position = Vector3.MoveTowards(BT.transform.position, BT.player.transform.position, BT.speed);
            state = Node_State.success;
            //Debug.Log("chase" + state);








        }



        else
        {




            BT.playerspotted = false;
            BT.waypoint = true;
            BT.canmove = false;
            state = Node_State.faliure;

        }
#endregion

























    }

    
}

