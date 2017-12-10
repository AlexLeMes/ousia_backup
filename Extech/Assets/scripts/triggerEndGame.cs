using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class triggerEndGame : MonoBehaviour {

    //public GameObject winMenu;

    //public Text playerTimeText;
    //public float playerTime;

    public GameObject gameWonMenu;
    public GameObject animatedDropShip;
    public GameObject staticDropShip;
    Animator dropShipAnim;

    public GameObject sceneicCamera;
    public GameObject playersCamera;

    public GameObject player;
    public Vector3 hidePlayerPos;


    private void Awake()
    {
        gameWonMenu.SetActive(false);
        sceneicCamera.SetActive(false);
        animatedDropShip.SetActive(false);

        staticDropShip.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameController.instance.enablePlayerTimer = false;
            gameWon();
        }
    }

    public void gameWon()
    {
        Invoke("gotoMainMenu", 11f);

        GameController.instance.hidePlayer();

        sceneicCamera.SetActive(true);
        playersCamera.SetActive(false);

        staticDropShip.SetActive(false);
        animatedDropShip.SetActive(true);
        

        dropShipAnim = animatedDropShip.GetComponent<Animator>();

        dropShipAnim.enabled = true;

        gameWonMenu.SetActive(true);
        GameController.instance.showMouse();
    }

    void gotoMainMenu()
    {
        GameController.instance.gotoMenu();
    }

}
