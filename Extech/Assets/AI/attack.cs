using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : Node
{

    
    public override void Execute()
    {
      


        if (Vector3.Distance(BT.transform.position, BT.player.transform.position) < 3 && BT.ect.characterOnFire==false)
        {
            BT.enemy.SetInteger("enemyanm", 3);
            BT.idle = false;
            BT.playerspotted = false;
            BT.attack = true;
            Debug.Log("attack"+state);
            state = Node_State.success;
            BT.pct.takeDamage(BT.attackDamage);
            BT.speed = 0;
            BT.chase = 0;
        }
        else
        {
           // BT.playerspotted = true;
            BT.attack = false;
            state = Node_State.faliure;
            BT.speed = BT.movespeed;
            BT.chase = BT.movespeed;
        }
        
        

    }
}
