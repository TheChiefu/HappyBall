using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int Value;
    [SerializeField] private AudioClip[] sounds;

    [Header("Visual Properties:")]
    [SerializeField] private GameObject visualCoin;
    private AudioSource AS;

    const string tooltop = 
        "Coin Color Values:\n" +
        "0: Negative or Zero value Coins\n" +
        "1: Valued at 1\n" +
        "2: Valued at 5\n" +
        "3: Valued at 10\n" +
        "4: Valued at 20\n" +
        "5: Valued at 50\n" +
        "6: Valued at 100";
    [Tooltip(tooltop)]
    [SerializeField] private Color[] coinColors;
    
    [Header("Floating Properties")]

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
        SetCoinProperties();
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

    private void SetCoinProperties()
    {
        MeshRenderer renderer = visualCoin.GetComponent<MeshRenderer>();

        // Set coin properties dependent on value

        if (Value <= 0)
        {
            renderer.material.color = coinColors[0];
            AS.clip = sounds[0];
        }
        else if (Value > 0 && Value <= 1)
        {
            renderer.material.color = coinColors[1];
            AS.clip = sounds[1];
        }
        else if (Value > 1 && Value <= 5)
        {
            renderer.material.color = coinColors[2];
            AS.clip = sounds[2];
        }
        else if (Value > 5 && Value <= 10)
        {
            renderer.material.color = coinColors[3];
            AS.clip = sounds[3];
        }
        else if (Value > 10 && Value <= 20)
        {
            renderer.material.color = coinColors[4];
            AS.clip = sounds[4];
        }
        else if (Value > 20 && Value <= 50)
        {
            renderer.material.color = coinColors[5];
            AS.clip = sounds[5];
        }
        else if (Value > 50)
        {
            renderer.material.color = coinColors[6];
            AS.clip = sounds[6];
        }
        else
        {
            renderer.material.color = coinColors[1];
            AS.clip = sounds[1];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AS.Play();
            LevelManager.instance.UpdateScore(Value);

            visualCoin.SetActive(false);

            Destroy(this.gameObject, AS.clip.length);
        }
    }
}
