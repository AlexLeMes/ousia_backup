using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestroy : MonoBehaviour {

    public float destoryTime = 5;

    private void Start()
    {
        Destroy(this.gameObject, destoryTime);
    }

}
