using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance = null;

    int mainMenu = 0;
    public int mainLevel = 1;
    int deathScene = 2;

    public GameObject console;


    public GameObject loadScreen;


    GameObject player;
    playerController _playerController;
    GameObject playerUI;

    GameObject playerWeapon;
    weapon _weapon;

    public GameObject spawnPoint;
    Vector3 spawnLocation;

    bool isPaused = false;

    public GameObject pauseMenuObj;
    public GameObject playerDeathMenu;
    public Vector3 hidePlayerPos;


    public float playerTime = 0f;
    public bool enablePlayerTimer = false;
    public Text playerTimeText;

    private void Awake()
    {
        instance = this;
    }

    /*
    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
    }
    */
    void Start ()
    {
        loadScreen.SetActive(true);

        playerTime = 0f;

        spawnPoint = GameObject.FindGameObjectWithTag("playerSpawnPoint");

        playerUI = GameObject.FindGameObjectWithTag("playerUI");

        if (playerUI != null)
        {
            playerUI.SetActive(true);
        }

        player = GameObject.FindGameObjectWithTag("Player");
        playerDeathMenu.SetActive(false);

        if (player != null)
        {
            _playerController = player.GetComponent<playerController>();
        }

        playerWeapon = GameObject.FindGameObjectWithTag("playerWeapon");

        if(playerWeapon != null)
        {
            _weapon = playerWeapon.GetComponent<weapon>();
        }
        
        if(spawnPoint != null)
        {
            spawnLocation = spawnPoint.transform.position;
        }
        
        if(console !=null)
        {
            console.SetActive(false);
        }
        if(pauseMenuObj != null)
        {
            pauseMenuObj.SetActive(false);
        }
        
        respawn();

        if(Time.timeScale != 1f)
        {
            Time.timeScale = 1;
        }

        enablePlayerTimer = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            loadScreen.SetActive(false);
        }

        if(enablePlayerTimer)
        {
            playerTime += Time.deltaTime;
            playerTimeText.text = "Your Time: " + playerTime.ToString("F2");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("PAUSE_GAME");
            pauseGame();
        }


        if(Input.GetKeyDown(KeyCode.C))
        {
            console.SetActive(!console.activeSelf);

            if(!console.activeSelf)
            {
                hideMouse();
            }
            else
            {
                showMouse();
            }
            
        }
    }

    public void respawn()
    {
        hideMouse();

        if(player != null)
        {
            player.transform.position = spawnLocation;
        }
        
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(mainLevel);
    }

    public void pauseGame()
    {
        if(Time.timeScale < 1)
        {
            Time.timeScale = 1;
            pauseMenuObj.SetActive(false);
        }
        else if(Time.timeScale >= 1)
        {
            Time.timeScale = 0;
            pauseMenuObj.SetActive(true);
        }

        if(pauseMenuObj.activeSelf)
        {
            _playerController.enabled = false;
            _weapon.enabled = false;
            showMouse();
        }
        else
        {
            _playerController.enabled = true;
            _weapon.enabled = true;
            hideMouse();
        }

    }

    public void hideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void showMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void playerDied()
    {
        showMouse();
        playerUI.SetActive(false);
        _playerController.playerDeath();
        playerDeathMenu.SetActive(true);
    }

    public void hidePlayer()
    {
        _playerController.enabled = false;
        _weapon.enabled = false;
        player.transform.position = hidePlayerPos;
        playerUI.SetActive(false);
    }

    public void gotoMenu()
    {
        showMouse();
        SceneManager.LoadScene(mainMenu);
    }

    public void loadGameWonScene()
    {
        showMouse();
        SceneManager.LoadScene(3);
    }


    public void loadDeathScene()
    {
        showMouse();
        SceneManager.LoadScene(deathScene);
    }

    
    public void debugAddAmmo()
    {
        _weapon.gas += 250;
    }

    public void wipePlayerData()
    {
        PlayerPrefs.DeleteAll();
    }
    
}
