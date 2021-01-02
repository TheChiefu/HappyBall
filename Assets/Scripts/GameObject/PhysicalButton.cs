using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicalButton : MonoBehaviour
{

    [SerializeField] private Animator buttonAnimator;
    [SerializeField] private Button actionButton;
    [SerializeField] private AudioClip sfx;

    [Header("Button Requirements")]
    [SerializeField] private bool allowPlayer = true;
    [SerializeField] private bool allowPhysics = true;
    [SerializeField] private bool isRepeatable = true;
    [SerializeField] private float weightRequirement = 0;

    private AudioSource _as;

    private void Awake()
    {
        _as = GetComponent<AudioSource>();
        _as.clip = sfx;

        buttonAnimator.SetBool("Repeatable", isRepeatable);
    }


    private bool CheckRequirement(GameObject gameObject)
    {
        //Returning bool
        bool passed = false;

        //Mass Requirement Check
        if(weightRequirement > 0)
        {
            if (gameObject.GetComponent<Rigidbody>().mass >= weightRequirement) passed = true;
            else passed = false;
        }

        //Player Tag check
        if (gameObject.tag == "Player")
        {
            if (allowPlayer) passed = true;
            else passed = false;
        }

        //Physics object tag check
        if (gameObject.tag == "Physics")
        {
            if (allowPhysics) passed = true;
            else passed = false;
        }


        return passed;
    }


    private void DoAction()
    {
        if (actionButton.interactable)
        {
            actionButton.onClick.Invoke();
            if (!isRepeatable) actionButton.interactable = false;
            buttonAnimator.SetBool("Pressed", true);
            _as.Play();
        }
    }

    private void ExitAction()
    {
        buttonAnimator.SetBool("Pressed", false);
    }



    // Triggers //

    private void OnTriggerEnter(Collider other)
    {
        if (CheckRequirement(other.gameObject)) DoAction();
    }

    private void OnTriggerExit(Collider other)
    {
        if(CheckRequirement(other.gameObject)) ExitAction();
    }
}
