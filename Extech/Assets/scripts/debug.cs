using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class debug : MonoBehaviour {

    public InputField commandLine;
    string command;

    public void clearCommandLine()
    {
        commandLine.text = "";
    }

    public void checkCommand()
    {
        command = commandLine.text;

        switch (command)
        {
            case "wipe player data":
                GameController.instance.wipePlayerData();
                break;
            case "respawn":
                GameController.instance.respawn();
				break;
            case "add ammo":
                Debug.Log("<color=purple>CONSOLE: add ammo</color>");
                GameController.instance.debugAddAmmo();
                break;
            case "restart level":
                Debug.Log("<color=purple>CONSOLE: restart level</color>");
                GameController.instance.restartLevel();
                break;
            case "restart game":
                Debug.Log("<color=purple>CONSOLE: restart game</color>");
                SceneManager.LoadScene(0);
                break;
            default:
                Debug.Log("<color=red>CONSOLE: UNKOWN COMMAND</color>");
                commandLine.text = "UNKOWN COMMAND";
                break;
        }

        GameController.instance.hideMouse();
    }
}
