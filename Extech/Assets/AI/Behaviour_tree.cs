using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript;



public class Behaviour_tree : MonoBehaviour
{
    public float height;

    public float attackDamage = 1f;
    public Animator enemy;
    public bool attack;
    public Node root;
    public GameObject player;
    public GameObject[] waypoints;
    public int target;
    public float speed;
    public bool playerspotted;
    public bool canmove;
    public float maxdistance = 15;
    public bool waypoint;
    public float angle;
    public float vision = 40;
    public RaycastHit hit;
    public RaycastHit hitInfo;
    public character pct;
    public character ect;
    public Vector3 dir;
    public Vector3 tar;
    public float enemieawarness;
    public float slowradius;
    public float deceleration;
    public Vector3 steeringforce;
    public float movespeed;
    public bool idle;
    public float avoidanceforce = 20;
    public float chase = 5;
    public float lockrot = 0;
    public float DISTANCETOWAYPOINT = 2;
    public Rigidbody rb;
    public bool isreverse = false;

    public bool isDead = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        pct = player.GetComponent<character>();
        ect = GetComponent<character>();

        enemy = GetComponent<Animator>();
    }

    public void Start()
    {
        target = 0;

        selector_node selector = new selector_node();

        root = selector;

       
        sequenc_node sequenc = new sequenc_node();
        selector.children.Add(sequenc);
        sequenc.children.Add(new Chase());
        sequenc.children.Add(new attack());
        selector.children.Add(new flee());
        selector.children.Add(new patrol());
        selector.children.Add(new idle());

        root.BT = this;

        root.Start();

    }
    // Use this for initialization
    public void Update()
    {

        transform.rotation = Quaternion.Euler(lockrot, transform.rotation.eulerAngles.y, lockrot);
        
        root.Execute();
    }
    void OnDrawGizmos()
    {
        {
            
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, maxdistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, vision);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, angle);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, enemieawarness);
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, slowradius);
        }

    }



}
