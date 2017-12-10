using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keycard : MonoBehaviour {

    GameObject playerKeycard;

    public int cardID = 0;

    GameObject door;
    door _door;
    pickups card;

    public int[] storedCards;
    int storedCardPos = 0;
    int totalMapCards = 5;  // change this to find all keycards in scene and set as length

    private void Start()
    {
        storedCards = new int[totalMapCards];
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "door")
        {
            _door = other.gameObject.GetComponent<door>();

            for(int i = 0; i < storedCards.Length; i++)
            {
                cardID = storedCards[i];
                _door.accessDoor(cardID);
            }
        }
    }
    public void keycardPickup(int cardId)
    {
        storedCards[storedCardPos] = cardId;
        storedCardPos++;
    }

}
