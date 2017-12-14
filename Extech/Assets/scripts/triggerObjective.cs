using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerObjective : MonoBehaviour {

    GameObject player;
    objectives _objectvies;

    int playerObjectiveID = 0;

    [Header("OBJECTIVE STATS")]
    public string objectivesText;
    public string objectiveCompleteText;

    public int objectiveID = 0;
    public int completedID = 1;
    [Space(10)]

    [Header("OBJECTIVE TYPE")]
    public bool isGiveTrigger = false;
    public bool isCompleteTrigger = false;
    public bool isBombObjective = false;
    [Space(10)]

    [Header("OBJECTIVE STATUS")]
    public bool objectiveComplete = false;
    public float objectiveTime = 0f;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        _objectvies = player.GetComponent<objectives>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && isGiveTrigger)
        {
            givePlayerObjective();
        }
        else if(other.gameObject.tag == "Player" && isCompleteTrigger)
        {
            playerObjectiveID = _objectvies.currentObjectiveID;

            completeObjective(playerObjectiveID);
        }

    }

    public void givePlayerObjective()
    {
        _objectvies.updateObjective(objectivesText, objectiveID);

        if (isBombObjective)
        {
            _objectvies.toggleBombObjective(objectiveTime);
        }

        this.gameObject.SetActive(false);
    }

    public void completeObjective(int id)
    {
        if(id == completedID)
        {
            _objectvies.updateObjective(objectiveCompleteText, objectiveID);
            Debug.Log("ID WAS MATCH, OBJECTIVE COMPLETE");
        }
        else if(id != completedID)
        {
            Debug.Log("ID WAS NOT MATCH");
        }
    }

}
