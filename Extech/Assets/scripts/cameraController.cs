using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    GameObject player;
    Transform pTrans;

    public Transform lookat;

    Vector3 playerPos;
    Vector3 gotoPos;
    public float MaxX = -90;
    public float MinX = 90;

    float sensitivity = 100f;

    public Vector3 offset;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        pTrans = player.GetComponent<Transform>();
        transform.position = pTrans.transform.position + offset;
    }


    private void LateUpdate()
    {

        //The rotation as Euler angles in degrees relative to the parent transform's rotation.
        //we get the mouse axis we r trying to rotate to move around.


        //transform.localEulerAngles += new Vector3(-Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime, 0, 0);
        //to make sure the player doesnt exceed the limit of looking up or down, we creat a MaxX and MinX to restrict the player from looking over the limit.


        if (transform.localEulerAngles.x > MaxX && transform.localEulerAngles.x < 180)
        {

            transform.localEulerAngles = new Vector3(MaxX, 0, 0);
        }
        if (transform.localEulerAngles.x < MinX && transform.localEulerAngles.x > 180)
        {
            transform.localEulerAngles = new Vector3(MinX, 0, 0);
        }


        float direction = pTrans.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, direction, 0);


        transform.position = pTrans.transform.position + rotation * offset;
        transform.LookAt(lookat);

    }

}
