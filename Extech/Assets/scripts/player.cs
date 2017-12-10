using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : character {

/*
    TODO:
        - set the player to only be able to move when
        the mouse position is a certain distance away from the player
*/
   // character _character;

    public GameObject console;  //CHANGE TO TO TALK TO GAME MANGER - WHEN MADE AN INSTANCE

    public GameObject deathMenu;

    
    //PLAYER MOVE SPEED
    public float movespeed = 5f;
   //public float rotateSpeed = 90f;
    public float boostSpeed = 1f;

    //public Slider healthBar;
    public Slider staminaBar;

    cameraController _camera;
    public GameObject camera;
    
    float stamnia = 1f;
    float maxStam = 100;

    bool canMove = true;
    bool moving = false;
    bool canBoost = true;
    bool boosting = true;
    //private pickups pickup;

    private void Awake()
    {

        // _character = this.gameObject.GetComponent<character>();
        deathMenu.SetActive(false);

        _camera = camera.GetComponent<cameraController>();

        //pickup = gameObject.GetComponent<pickups>();
       // _character = this.gameObject.GetComponent<character>();
    }

    private void Start()
    {
        staminaBar.value = maxStam;
    }

    void Update ()
    {

        Debug.DrawRay(transform.position, camera.transform.position, Color.green);

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
            transform.Translate(Vector3.forward * moveSpeed *Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) )
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)   )
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift) && canBoost == true)
        {
            
            moveSpeed = boostSpeed;
            //Debug.Log(stamina);
            //_playerStats.decreasestamina();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 5f;
        }
        
        if (stamnia < maxStam)
        {
            canMove = false;
            stamnia += 0.05f * Time.deltaTime;
        }

        if (stamnia <= 0.1f)
        {
            canBoost = false;
        }
        else if(stamnia > 0.2f)
        {
            canBoost = true;
        }

        //healthBar.value = health;
        staminaBar.value = stamnia;

        //CONSOLE with `
        if (Input.GetKeyDown(KeyCode.C))
        {
            console.SetActive(!console.activeSelf);
        }
    }

    public void die()
    {
        //PLAYER DEATH LOGIC HERE
        // send game manager spawnLocation
        deathMenu.SetActive(true);
        GameController.instance.pauseGame();

        //CHANGE THIS LOGIC TO BE INSIDE CHARACTER SCRIPT?
    }

    public void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.tag == "enemy")
        {
            takeDamage(0.1f);
            Debug.Log(health);
        }
        */
    }
    
}