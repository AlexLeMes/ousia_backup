using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idle : Node {

    public override void Execute()
    {

        //play enemy idle animation here


        if (BT.idle == true)
        {
            state = Node_State.success;
            BT.enemy.SetInteger("enemyanm", 0);
        }
        else
        {
            BT.idle = false;
        }
       
       
        }


    }


    
	


