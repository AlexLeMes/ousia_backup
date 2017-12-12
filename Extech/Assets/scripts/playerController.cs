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
    Rigidbody rb;
    public weapon _weapon;
    public Animator anim;
    float inputH = 0f;
    float inputV = 0f;

    //PLAYER MOVE SPEED
    bool isMoving = false;
    public float moveSpeed = 5f;
    //public float rotateSpeed = 90f;
    public float boostSpeed = 3f;
    float maxSpeed = 10f;
    float currentSpeed = 0f;
    Vector3 direction;

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
        rb = GetComponent<Rigidbody>();
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

        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);

        /*
        if (Input.GetKey(KeyCode.W))
        {
            canBoost = true;
            rb.velocity = (transform.rotation * Vector3.forward * currentSpeed * Time.deltaTime);
        }
        else
        {
            canBoost = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = (transform.rotation * Vector3.back * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = (transform.rotation * Vector3.right * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = (transform.rotation * Vector3.left * currentSpeed * Time.deltaTime);
        }
        */
        //PLAYER KEY INPUT MOVEMENT//
        
        if (Input.GetKey(KeyCode.W))
        {
            isMoving = true;
            canBoost = true;
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        }
        else
        {
            canBoost = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            isMoving = true;
            currentSpeed = moveSpeed;
            transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            currentSpeed = moveSpeed;
            transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            currentSpeed = moveSpeed;
            transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.LeftShift) && canBoost && isMoving)
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
