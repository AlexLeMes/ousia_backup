using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour {

    public GameObject objectToLookAt;
    Vector3 target;

    private void Start()
    {
        target = objectToLookAt.transform.position;
    }


    void Update ()
    {
        target = objectToLookAt.transform.position;

        transform.LookAt(target);
    }
}
