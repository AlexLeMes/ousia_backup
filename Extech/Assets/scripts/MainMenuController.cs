using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    /*
  TODO:

*/


    public GameObject creditsMenu;

    private void Start()
    {
        creditsMenu.SetActive(false);
    }

    public void toggleCredits()
    {
        creditsMenu.SetActive(!creditsMenu.activeSelf);
    }
   
    public void quitGame()
    {
        Application.Quit();
    }
}
