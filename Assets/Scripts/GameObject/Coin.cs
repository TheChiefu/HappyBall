using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int Value;
    [SerializeField]
    private AudioClip SoundClip;
    private AudioSource AS;
    
    /// <summary>
    /// Speed in which coin rotates
    /// </summary>
    [SerializeField] private float rotationSpeed = 100;

    /// <summary>
    /// Speed in which coin floats up and down
    /// </summary>
    [SerializeField] private float floatFrequency = 1;

    /// <summary>
    /// Speed in which coin floats up and down
    /// </summary>
    [SerializeField] private float floatAmplitude = 1;


    private void Awake()
    {
        AS = GetComponent<AudioSource>();
    }



    Vector3 positionOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void Start()
    {
        positionOffset = transform.position;
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * rotationSpeed, 0f), Space.World);

        tempPos = positionOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;

        transform.position = tempPos;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AS.clip = SoundClip;
            AS.Play();
            LevelManager.instance.UpdateScore(Value);

            Destroy(this.gameObject, AS.clip.length);
        }
    }
}
