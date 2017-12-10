using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickups : MonoBehaviour {

    GameObject _player;
    public character _character;


    private keycard _keycard;

    public float healAmmount = 0;
    public int ammoType = 0; // 0 = gas   --- plasma is infinite, so no ID for it
    public int ammoToGive = 0;
    public int cardID;

    public bool healthPickup = false;
    public bool ammoPickup = false;
    public bool keycard = false;
   
    public void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _character = _player.GetComponent<character>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("pickup - hit player");

            if (healthPickup)
            {
                if (_character != null && _character.health != _character.MaxHealth)
                {
                    _character.heal(healAmmount);
                }  
            }

            if(ammoPickup)
            {
                if (_character != null)
                {
                    _character.giveAmmo(ammoToGive, ammoType);
                }
            }
            if(keycard)
            {
                _keycard = other.gameObject.GetComponent<keycard>();

                if(_keycard != null)
                {
                    _keycard.keycardPickup(cardID);
                }
            }

            Destroy(this.gameObject);

        }
    }
}
