using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    /*
    TODO:
        - set the player to only be able to move when
        the mouse position is a certain distance away from the player
*/
    mouseLookat _mouseLook;
    public weapon _weapon;
    public Animator anim;
    float inputH = 0f;
    float inputV = 0f;

    //PLAYER MOVE SPEED
    public float moveSpeed = 5f;
    //public float rotateSpeed = 90f;
    public float boostSpeed = 3f;
    float maxSpeed = 10f;
    float currentSpeed = 0f;

    bool canShoot = true;

    //public Slider healthBar;
    public Slider staminaBar;

    cameraController _camera;
    public GameObject camera;

    float stamnia = 1f;
    public float maxStam = 100f;

    public float stamRegenAmount = 5f;
    public float stamUsageAamount = 35f;

    public float stamStopAmount = 5f;
    public float stamCanBeUsedAmount = 1f;

    bool canMove = true;
    bool moving = false;
    bool canBoost = true;
    bool boosting = false;
    //private pickups pickup;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        _mouseLook = GetComponent<mouseLookat>();
        _camera = camera.GetComponent<cameraController>();

        GetComponent<playerController>().enabled = true;
        _mouseLook.enabled = true;


        _weapon.enabled = true;
    }

    private new void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        anim.SetBool("dead", false);

        stamnia = maxStam;
        staminaBar.maxValue = maxStam;
        staminaBar.value = stamnia;

        currentSpeed = moveSpeed;

        stamCanBeUsedAmount = maxStam / 2;
        //player can re-sprint when stamina is half their max stamina
    }

    void Update()
    {

        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);

        /*
        Debug.DrawRay(transform.position, camera.transform.position, Color.green);

        if ((Vector3.Distance(transform.position, camera.transform.position) > 11) || (!Physics.Raycast(transform.position, camera.transform.position)))
        {
            _camera.moveTowardsPlayer = true;
        }
        else
        {
            _camera.moveTowardsPlayer = false;
        }
        */
        /*
        if (!Physics.Raycast(transform.position, camera.transform.position))
        {
            _camera.moveTowardsPlayer = true;
        }
        else
        {
            _camera.moveTowardsPlayer = false;
        }
        */
        //Debug.Log(!Physics.Raycast(transform.position, camera.transform.position));


        //PLAYER KEY INPUT MOVEMENT//
        if (Input.GetKey(KeyCode.W))
        {
            //anim.Play("Walk_Forward");
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //anim.Play("Walk_Backward");
            transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //anim.Play("Walk_Right");
            transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //anim.Play("Walk_Left");
            transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift) && canBoost)
        {
            boosting = true;
            _weapon.canShoot = false;

            //anim.Play("Sprint");
            currentSpeed += boostSpeed;

            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }

            stamnia -= stamUsageAamount * Time.deltaTime;

            staminaBar.value = stamnia;
        }
        else
        {
            boosting = false;
            _weapon.canShoot = true;
        }

        anim.SetBool("sprinting", boosting);

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = moveSpeed;
        }

        if (stamnia < maxStam)
        {
            boosting = false;
            canMove = false;
            stamnia += stamRegenAmount * Time.deltaTime;
        }

        if (stamnia <= stamStopAmount)
        {
            boosting = false;
            canBoost = false;
            currentSpeed = moveSpeed;
        }
        else if (stamnia >= stamCanBeUsedAmount)
        {
            canBoost = true;
        }

        
        staminaBar.value = stamnia;
    }

    public void playerDeath()
    {
        _weapon.enabled = false;
        anim.SetBool("dead", true);
        _mouseLook.enabled = false;
        GetComponent<playerController>().enabled = false;
    }

}
