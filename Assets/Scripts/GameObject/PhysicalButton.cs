using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicalButton : MonoBehaviour
{

    [SerializeField] private Animator buttonAnimator;
    [SerializeField] private Button actionButton;
    [SerializeField] private AudioClip sfx;
    [SerializeField] private bool isRepeatable;

    private AudioSource _as;

    private void Awake()
    {
        _as = GetComponent<AudioSource>();
        _as.clip = sfx;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            actionButton.onClick.Invoke();
            if (!isRepeatable) actionButton.interactable = false;
            buttonAnimator.SetBool("Pressed", true);
            _as.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            buttonAnimator.SetBool("Pressed", false);
        }

    }
}
