using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class character : MonoBehaviour {

    cameraController _cameraControls;

    Behaviour_tree BT;
    Rigidbody enemyRB;

    GameObject enemyModel;

    bool isPlayer = false;

    GameObject fireDamageEffect;

    [Header("STATS")]
    public float MaxHealth = 100f;
    public float health = 1f;
    public float stamina = 1f;
    public bool alive = true;
    public Slider characterHealthBar;
    [Space(10)]

    [Header("PLAYER")]
    public weapon characterWeapon;
    public GameObject characterDeathMenu;

    [Space(10)]

    [Header("ENEMY")]
    public GameObject enemyDeathModel;

    [Space(10)]

    [Header("WEAPON")]
    public float weaponOneAmmo = 0;
    public bool characterOnFire = false;
    public float fireDamageOverTime = 1f;
    public float burnTime = 5f;
    public float burnTickRate = 1f;


    public void Start()
    {
        health = MaxHealth;

        if(this.gameObject.tag == "enemy")
        {
            enemyModel = this.gameObject;

            BT = GetComponent<Behaviour_tree>();

            if(BT != null)
            {
                BT.enabled = true;
            }

            enemyRB = GetComponent<Rigidbody>();
        }

        

        if (characterHealthBar != null)
        {
            characterHealthBar.maxValue = MaxHealth;
            characterHealthBar.value = health;
        }

        if(this.gameObject.tag == "Player")
        {
            isPlayer = true;
            GameObject playerCam;
            playerCam = GameObject.FindGameObjectWithTag("MainCamera");
            _cameraControls = playerCam.GetComponent<cameraController>();
            _cameraControls.enabled = true;
        }

        if(characterDeathMenu != null)
        {
            characterDeathMenu.SetActive(false);
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;

        if(characterHealthBar != null)
        {
            characterHealthBar.value = health;
        }

        if(health <= 0)
        {
            die();
        }
    }

    public void takeFireDamage()
    {
        if (characterOnFire)
        {
            health -= fireDamageOverTime;
        }

        if (characterHealthBar != null)
        {
            characterHealthBar.value = health;
        }


        burnTime -= burnTickRate;

        if (burnTime <= 0)
        {
            characterOnFire = false;
            Destroy(fireDamageEffect);
            CancelInvoke();
        }

        if (health <= 0)
        {
            die();
        }
    }


    public void heal(float healAmount)
    {
        health += healAmount;

        characterHealthBar.value = health;
    }

    public void giveAmmo(int ammoAmmount, int ammoType)
    {
        if(ammoType == 0)
        {
            characterWeapon.gas += ammoAmmount;
        }
    }

    public void setOnFire(GameObject fire, float fireBurnTime, float fireDamage, int tickRate)
    {
        fireDamageOverTime = fireDamage;
        burnTime = fireBurnTime;
        burnTickRate = tickRate;

        if (!characterOnFire)
        {
            characterOnFire = true;

            fireDamageEffect = Instantiate(fire, transform.position, transform.rotation);
            fireDamageEffect.transform.parent = this.gameObject.transform;

            if (characterOnFire)
            {
                InvokeRepeating("takeFireDamage", 0f, tickRate);
            }
        }

        
    }

    public void die()
    {
        if(isPlayer)
        {
            killPlayer();
        }
        else if(!isPlayer)
        {
            killEnemy();
        }
    }

    public void killEnemy()
    {
        if(enemyDeathModel != null)
        {
            Instantiate(enemyDeathModel, transform.position, transform.rotation);
            enemyModel.SetActive(false);
        }
        
        //enemyRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;

        if (BT != null)
        {
            BT.isDead = true;
            Invoke("disableBT", 1f);
        }
    }

    public void disableBT()
    {
        BT.enabled = false;
    }

    public void killPlayer()
    {
        Debug.Log("PLAYER DIED");
        GameController.instance.playerDied();
    }
}
