using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    mouseLookat _mouseLook;
    Rigidbody rb;
    float inputH = 0f;
    float inputV = 0f;
    bool isMoving = false;

    bool canMove = true;
    bool moving = false;
    bool canBoost = true;
    bool boosting = false;

    float maxSpeed = 10f;
    float currentSpeed = 0f;
    Vector3 direction;

    bool canShoot = true;
    cameraController _camera;

    [Header("SPEED VARIABLES")]
    public float moveSpeed = 5f;
    public float boostSpeed = 3f;
    [Space(10)]

    [Header("STAMINA")]
    public float maxStam = 100f;
    public float stamRegenAmount = 5f;
    public float stamUsageAamount = 35f;
    public float stamStopAmount = 5f;
    public float stamCanBeUsedAmount = 1f;
    public Slider staminaBar;
    float stamnia = 1f;
    [Space(10)]

    [Header("MISC")]
    public weapon _weapon;
    public Animator anim;
    //public GameObject camera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        _mouseLook = GetComponent<mouseLookat>();
        //_camera = camera.GetComponent<cameraController>();

        GetComponent<playerController>().enabled = true;
        _mouseLook.enabled = true;


        _weapon.enabled = true;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        canBoost = false;

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
        if(rb.velocity.magnitude > 0f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        //animation controller
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);


        //PLAYER KEY INPUT MOVEMENT//
        if (Input.GetKey(KeyCode.W))
        {
            isMoving = true;
            canBoost = true;
            transform.position += transform.forward * Time.deltaTime * currentSpeed;
        }
        else
        {
            canBoost = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            isMoving = true;
            currentSpeed = moveSpeed;
            transform.position += -transform.forward * Time.deltaTime * currentSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            currentSpeed = moveSpeed;
            transform.position += transform.right * Time.deltaTime * currentSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            currentSpeed = moveSpeed;
            transform.position += -transform.right * Time.deltaTime * currentSpeed;
        }
        
        if (Input.GetKey(KeyCode.LeftShift) && canBoost && isMoving)
        {
            if(stamnia > 0)
            {
                boosting = true;
            }
            
            _weapon.canShoot = false;
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

        if(stamnia < 0)
        {
            stamnia = 0;
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
