using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireDamage : MonoBehaviour {

    public GameObject fireBurningEffect;

    public float burnTime = 0f;
    public float burnDamage = 0f;
    public int burnTickRate = 0;

    character _char;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<character>() != null)
        {
            _char = other.gameObject.GetComponent<character>();
            _char.setOnFire(fireBurningEffect, burnTime, burnDamage, burnTickRate);
        }
    }
}
