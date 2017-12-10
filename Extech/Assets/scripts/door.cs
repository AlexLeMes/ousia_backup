using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour {

    public int doorID = 0;
    public bool requiresBombObjective = false;
    Animator doorAnim;
    AudioSource doorAudio;
    public AudioClip doorSound;

    objectives _objectives;
    GameObject _player;

    private void Awake()
    {
        doorAnim = GetComponent<Animator>();
        doorAudio = GetComponent<AudioSource>();
        doorAnim.enabled = false;

        _player = GameObject.FindGameObjectWithTag("Player");
        _objectives = _player.GetComponent<objectives>();
    }

    public void accessDoor(int cardID)
    {
        if(cardID == doorID)
        {
            if(requiresBombObjective && _objectives.bombObjectiveActive)
            {
               openDoor();
            }
            else if(!requiresBombObjective)
            {
                openDoor();
            }
            
            Debug.Log("<color=green>PLAYER HAS VALID KEYCARD - openDoor</color>");
        }
    }

    public void openDoor()
    {
        doorAnim.enabled = true;
        //doorAnim.Play();
        //doorAnim.SetBool(0, true);
        doorAudio.PlayOneShot(doorSound);
    }

}
