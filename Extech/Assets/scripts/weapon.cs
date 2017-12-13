using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon : MonoBehaviour {

    public Animator weaponAnim;
    bool animShoot = false;
    GameObject _player;


    AudioSource weaponAudio;
    public AudioSource flamethrowerAudioSource;
    //AudioClip shootingSound;
    public AudioClip plasmaSFX;
    public AudioClip flameSFX;

    Bullet _bullet;

    public bool flamethrower;

    public float powerattacktimer = 0;

    public GameObject[] plasmaInUse;
    public GameObject[] fireInUse;

    public bool ischarging = false;
    public bool flamethrowerpicked = false;
    public bool canShoot = true;

    public int maxGas;
    public int gas;
    public Slider gasBar;
    public GameObject gasBarObj;

    public GameObject plasma;
    public GameObject plasmaSpecial;

    public float powerattack;
    public float maxPowerattack;
    public Slider powerAttackBar;
    public GameObject powerAttackBarObj;
    public float powerAttackCanUseValue;
    public float powerAttackChargeRate;

    public GameObject flameBullet;
    public GameObject flamethrowerLight;

    GameObject plasmashot;
    GameObject flameShot;

    public Vector3 weaponpos;
    public ParticleSystem flame;

    Rigidbody plasmarb;
    Rigidbody flameBulletRB;

    public float force;
    public float flameForce;
    
    public bool plasmaWeaponActive;

    public ParticleSystem chargingEffect;
    public ParticleSystem shootingPlasmaEffect;

    bool canUseFlamethrower;

    bool showAmmo = false;


    // Use this for initialization
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        weaponAnim = _player.GetComponent<Animator>();

        weaponAudio = GetComponent<AudioSource>();

        flamethrowerLight.SetActive(false);

        chargingEffect.Stop();
        ischarging = false;
        plasmaWeaponActive = true;
        flamethrower = false;

        showAmmo = false;

        for(int i = 0; i < plasmaInUse.Length; i++)
        {
            plasmaInUse[i].SetActive(true);
            fireInUse[i].SetActive(false);
        }

        powerAttackBarObj.SetActive(true);
        gasBarObj.SetActive(false);

        //currentWeapon.text = "Press '2' for Flamethrower";

        gasBar.maxValue = maxGas;
        gasBar.value = gas;

        powerAttackBar.value = 0;

        powerAttackCanUseValue = powerAttackBar.maxValue;

    }


    void Update()
    {
        if (gas <= 0)
        {
            canUseFlamethrower = false;
        }
        else if (gas > 0)
        {
            canUseFlamethrower = true;
        }


        if (Input.GetMouseButton(1) && plasmaWeaponActive && canShoot) //starts the timer for charging the plasma weapon
        {
            chargingEffect.Emit(5);
            powerattacktimer += Time.deltaTime * powerAttackChargeRate;
            ischarging = true;
        }


        if (Input.GetMouseButtonUp(1) && powerattacktimer >= powerAttackCanUseValue && ischarging && plasmaWeaponActive)
        {
            shootPowerAttack();
            powerattacktimer = 0;
        }
        if (Input.GetMouseButtonUp(1) && powerattacktimer < powerAttackCanUseValue && plasmaWeaponActive)
        {
            powerattacktimer = 0;
            ischarging = false;
        }

        if(ischarging)
        {
            chargingEffect.Play();
        }
        else if(!ischarging)
        {
            chargingEffect.Stop();
        }

        if (Input.GetMouseButtonDown(0) && plasmaWeaponActive)
        {
            shootPlasmaGun();
        }

        powerAttackBar.value = powerattacktimer;

        //choose weapon

        //PLASMA GUN ACTIVE
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            flamethrower = false;
            plasmaWeaponActive = true;
            showAmmo = false;

            for (int x = 0; x < plasmaInUse.Length; x++)
            {
                plasmaInUse[x].SetActive(true);
                fireInUse[x].SetActive(false);
            }

            gasBarObj.SetActive(false);
            powerAttackBarObj.SetActive(true);
        }

        //FLAME THROWER ACTIVE
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            plasmaWeaponActive = false;
            flamethrower = true;
            showAmmo = true;

            for (int x = 0; x < plasmaInUse.Length; x++)
            {
                plasmaInUse[x].SetActive(false);
                fireInUse[x].SetActive(true);
            }
            powerAttackBarObj.SetActive(false);
            gasBarObj.SetActive(true);
        }

        if (Input.GetMouseButton(0) && flamethrower && canUseFlamethrower)
        {
            if (canShoot)
            {
                flame.Play();
                flamethrowerLight.SetActive(true);
                shootFlameThrower();
                gas--;
            }
            else
            {
                flame.Stop();
                flamethrowerLight.SetActive(false);
            }

            if(gas <= 0)
            {
                weaponAudio.Stop();
            }

            if (!weaponAudio.isPlaying)
            {
                weaponAudio.PlayOneShot(flameSFX);
            }

        }
        else
        {
            flame.Stop();
            flamethrowerLight.SetActive(false);
        }

        if(Input.GetMouseButtonUp(0) && flamethrower)
        {
            weaponAudio.Stop();
        }

        gasBar.value = gas;
        
    }
    public void shootPlasmaGun()
    {
        if(canShoot)
        {
            weaponAudio.PlayOneShot(plasmaSFX);
            shootingPlasmaEffect.Emit(15);

            plasmashot = Instantiate(plasma, transform.position, Quaternion.identity);
            plasmarb = plasmashot.GetComponent<Rigidbody>();
            plasmarb.AddForce(transform.forward * force);

            animShoot = true;
        }
        

    }
    public void shootPowerAttack()
    {
            weaponAudio.PlayOneShot(plasmaSFX);

            plasmashot = Instantiate(plasmaSpecial, transform.position, Quaternion.identity);
            plasmarb = plasmashot.GetComponent<Rigidbody>();
            plasmarb.AddForce(transform.forward * force);

            animShoot = true;

    }


    public void shootFlameThrower()
    {

        flameShot = Instantiate(flameBullet, transform.position, Quaternion.identity);
        flameBulletRB = flameShot.GetComponent<Rigidbody>();
        flameBulletRB.AddForce(transform.forward * flameForce);

    }
}

