using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerAnimation : MonoBehaviour {

    public GameObject objectWithAnimation;
    Animator _animator;

    private void Start()
    {
        _animator = objectWithAnimation.GetComponent<Animator>();
        _animator.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            toggleAnimation();
        }

    }

    public void toggleAnimation()
    {
        _animator.enabled = true;
    }
}
