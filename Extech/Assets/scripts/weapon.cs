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
    float powerAttackCanUseValue;
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

    //gunLookat _gunLookat;
    bool canUseFlamethrower;

    public Text ammoText;
    public Text currentWeapon;
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
        //plasmaInUse.SetActive(true);
        powerAttackBarObj.SetActive(true);
        //fireInUse.SetActive(false);
        gasBarObj.SetActive(false);

        currentWeapon.text = "Press '2' for Flamethrower";

        gasBar.maxValue = maxGas;
        gasBar.value = gas;

        powerAttackBar.value = 0;

        powerAttackCanUseValue = powerAttackBar.maxValue;

        //flamethrowerpicked = false;

        //_gunLookat = this.gameObject.GetComponent<gunLookat>();
        //canUseFlamethrower = _gunLookat.canShoot;
        //  MAKE THIS BOOL WORK ?
    }


    void Update()
    {
        //chargingEffect.Play();
        if (gas <= 0)
        {
            canUseFlamethrower = false;
        }
        else if (gas > 0)
        {
            canUseFlamethrower = true;
        }

        /*
        if (Input.GetMouseButton(1)) //starts the timer for charging the plasma weapon
        {
            ischarging = true;
        }
        */


        if (Input.GetMouseButton(1) && plasmaWeaponActive) //starts the timer for charging the plasma weapon
        {
            chargingEffect.Play();
            powerattacktimer += Time.deltaTime * powerAttackChargeRate;
            ischarging = true;
        }
        else
        {
            chargingEffect.Stop();
        }
        
        if (Input.GetMouseButtonUp(1) && powerattacktimer > powerAttackCanUseValue && ischarging && plasmaWeaponActive)
        {
            shootPowerAttack();
            powerattacktimer = 0;
        }
        if (Input.GetMouseButtonUp(1) && powerattacktimer < powerAttackCanUseValue && plasmaWeaponActive)
        {
            powerattacktimer = 0;
            ischarging = false;
        }

        if (Input.GetMouseButtonDown(0) /*&& powerattacktimer < 2*/ && plasmaWeaponActive)
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

            currentWeapon.text = "Press '2' for Flamethrower";

            for (int x = 0; x < plasmaInUse.Length; x++)
            {
                plasmaInUse[x].SetActive(true);
                fireInUse[x].SetActive(false);
            }

            //fireInUse.SetActive(false);
            gasBarObj.SetActive(false);

            //plasmaInUse.SetActive(true);
            powerAttackBarObj.SetActive(true);
        }

        //FLAME THROWER ACTIVE
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            plasmaWeaponActive = false;
            flamethrower = true;
            showAmmo = true;

            //gasBar.value = gas / 10;

            currentWeapon.text = "Press '1' for Plasmagun";


            for (int x = 0; x < plasmaInUse.Length; x++)
            {
                plasmaInUse[x].SetActive(false);
                fireInUse[x].SetActive(true);
            }
            //plasmaInUse.SetActive(false);
            powerAttackBarObj.SetActive(false);

            //fireInUse.SetActive(true);
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

            plasmashot = Instantiate(plasma, transform.position, Quaternion.identity);
            plasmarb = plasmashot.GetComponent<Rigidbody>();
            plasmarb.AddForce(transform.forward * force);

            animShoot = true;
        }
        
        //weaponAnim.SetBool("shoot", animShoot);
        //weaponAnim.SetTrigger("shootTrigger");

    }
    public void shootPowerAttack()
    {
        if(canShoot)
        {
            weaponAudio.PlayOneShot(plasmaSFX);

            plasmashot = Instantiate(plasmaSpecial, transform.position, Quaternion.identity);
            plasmarb = plasmashot.GetComponent<Rigidbody>();
            plasmarb.AddForce(transform.forward * force);

            animShoot = true;
        }
        //weaponAnim.SetBool("shoot", animShoot);
        //weaponAnim.SetTrigger("shootTrigger");
    }


    public void shootFlameThrower()
    {

        flameShot = Instantiate(flameBullet, transform.position, Quaternion.identity);
        flameBulletRB = flameShot.GetComponent<Rigidbody>();
        flameBulletRB.AddForce(transform.forward * flameForce);
   
    }

    /*
    IEnumerator shootFlameThrowerBullet()
    {
        flameShot = Instantiate(flameBullet, transform.position, Quaternion.identity);
        flameBulletRB = flameShot.GetComponent<Rigidbody>();
        flameBulletRB.AddForce(transform.forward * flameForce);

        yield return new WaitForSeconds(5f);
    }
    */
}

