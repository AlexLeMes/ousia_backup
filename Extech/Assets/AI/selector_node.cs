using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class selector_node : Node
{


    public override void Execute()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].Execute();
            if (children[i].state == Node_State.success)
            {
                state = Node_State.success;
                return;
            }
            if (children[i].state == Node_State.running)
            {
                state = Node_State.running;
                return;

            }


        }






        state = Node_State.faliure;

    }








}





