using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunRotate : MonoBehaviour {

    Transform gunTransf;

    public float MaxX = 90;
    public float MinX = -90;

    public float sensitivity = 500f;

    private void Awake()
    {
        gunTransf = this.gameObject.transform;
    }

    private void LateUpdate()
    {
        transform.localEulerAngles += new Vector3(-Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime, 0, 0);


        if (transform.localEulerAngles.z > MaxX && transform.localEulerAngles.z < 180)
        {
            // transform.localEulerAngles.x = Mathf.Clamp(transform.localEulerAngles.x, 0, MaxX);
            transform.localEulerAngles = new Vector3(MaxX, 0, 0);
        }
        if (transform.localEulerAngles.z < MinX && transform.localEulerAngles.z > 180)
        {
            transform.localEulerAngles = new Vector3(MinX, 0, 0);
        }

        float direction = gunTransf.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, direction, 0);
    }
}
