using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public bool isPlasma = false;
    public bool isFire = false;

    public float plasmaDamage;
    public float fireDamage;

    public GameObject plasmaImpactEffect;
    public GameObject alienBloodHitEffect;
    public GameObject fireImpactEffect;

    float damageToDeal = 0f;

    public float burnDamage = 5f;
    public float burnTime = 5f;
    public int burnTickRate = 1;

    character _charater;

    private void Start()
    {
        if(isPlasma)
        {
            damageToDeal = plasmaDamage;
        }
        else if(isFire)
        {
            damageToDeal = fireDamage;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _charater = other.gameObject.GetComponent<character>();

        if (other.gameObject.tag == "enemy")
        {
            /*
            _charater = other.gameObject.GetComponent<character>();

            if (_charater != null)
            {
                hitEnemy();
            }
            */

            //_charater = other.gameObject.GetComponent<character>();
            hitEnemy();
        }
        else if(other.gameObject.tag == "targetDummy" && isFire)
        {
            //_charater = other.gameObject.GetComponent<character>();
            _charater.setOnFire(fireImpactEffect, burnTime, burnDamage, burnTickRate);
        }

        if(!isFire && other.gameObject.tag != "enemy")
        {
            Instantiate(plasmaImpactEffect, transform.position, transform.rotation);
        }

        if(!isFire)
        {
            Destroy(this.gameObject);
        }

        /*
        else
        {
            if(other.gameObject.GetComponent<MeshRenderer>() != null && other.gameObject.GetComponent<MeshRenderer>().enabled && !isFire)
            {
                Instantiate(plasmaImpactEffect, transform.position, transform.rotation);
            }
        }
        */
        
    }

    public void hitEnemy()
    {
        if (isPlasma)
        {
            Instantiate(alienBloodHitEffect, transform.position, transform.rotation);
            _charater.takeDamage(damageToDeal);
        }
        else if (isFire)
        {
            _charater.setOnFire(fireImpactEffect, burnTime, burnDamage, burnTickRate);
        }

        //Destroy(this.gameObject);
    }

   
}
