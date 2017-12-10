using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolTest : MonoBehaviour {


    public Transform[] Nodes;
    public float speed = 0f;

    int currentNode = 0;


	
	void Update () 
    {
        patrol();
	}

    public void patrol()
    {
        if(currentNode >= Nodes.Length)
        {
            currentNode = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, Nodes[currentNode].transform.position, speed * Time.deltaTime);
        transform.LookAt(Nodes[currentNode].transform.position);

        if(transform.position == Nodes[currentNode].transform.position)
        {
            currentNode++;
        }
    }

}
