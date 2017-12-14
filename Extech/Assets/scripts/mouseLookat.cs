using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseLookat : MonoBehaviour {


    public float mouseSensitivity = 100f;
    public Slider playerSensBar;

    private void Update()
    {
        if (playerSensBar != null)
        {
            mouseSensitivity = playerSensBar.value;
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * mouseSensitivity * Time.deltaTime, Space.World);

    }

}
