using UnityEngine;

/// <summary>
/// Generic coin class
/// </summary>
public class Coin : MonoBehaviour
{
    [SerializeField] private int Value;
    [SerializeField] private AudioClip[] sounds;

    [Header("Visual Properties:")]
    [SerializeField] private GameObject visualCoin;
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
    [SerializeField] private bool isFloating = false;

    /// <summary>
    /// Speed in which coin rotates
    /// </summary>
    [SerializeField] private float rotationSpeed = 100;

    /// <summary>
    /// Speed in which how fast the coin goes up and down
    /// </summary>
    [SerializeField] private float floatFrequency = 1;

    /// <summary>
    /// Speed in which coin floats up and down
    /// </summary>
    [SerializeField] private float floatAmplitude = 1;


    //Unseen Properties in Editor
    private AudioSource _as;
    private Rigidbody _rb;
    private Vector3 tempPos = Vector3.zero;


    //Set properties of coin on runtime
    private void Awake()
    {
        if(_as == null) _as = GetComponent<AudioSource>();
        if(_rb == null) _rb = GetComponent<Rigidbody>();
        SetCoinProperties();
    }

    //Update on fixed time scale
    private void FixedUpdate()
    {
        _rb.useGravity = !isFloating;
        VisualBob(visualCoin);
    }







    /// <summary>
    /// Transform rotation and position of target
    /// </summary>
    /// <param name="target"></param>
    private void VisualBob(GameObject target)
    {
        //Rotate coin
        target.transform.Rotate(new Vector3(0f, Time.deltaTime * rotationSpeed, 0f), Space.Self);

        //Update vertical position based on sine wave
        tempPos = transform.position;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;
        target.transform.position = tempPos;
    }


    /// <summary>
    /// Set coin properties dependent on value
    /// </summary>
    private void SetCoinProperties()
    {
        MeshRenderer renderer = visualCoin.GetComponent<MeshRenderer>();

        if (Value <= 0)
        {
            renderer.material.color = coinColors[0];
            _as.clip = sounds[0];
        }
        else if (Value > 0 && Value <= 1)
        {
            renderer.material.color = coinColors[1];
            _as.clip = sounds[1];
        }
        else if (Value > 1 && Value <= 5)
        {
            renderer.material.color = coinColors[2];
            _as.clip = sounds[2];
        }
        else if (Value > 5 && Value <= 10)
        {
            renderer.material.color = coinColors[3];
            _as.clip = sounds[3];
        }
        else if (Value > 10 && Value <= 20)
        {
            renderer.material.color = coinColors[4];
            _as.clip = sounds[4];
        }
        else if (Value > 20 && Value <= 50)
        {
            renderer.material.color = coinColors[5];
            _as.clip = sounds[5];
        }
        else if (Value > 50)
        {
            renderer.material.color = coinColors[6];
            _as.clip = sounds[6];
        }
        else
        {
            renderer.material.color = coinColors[1];
            _as.clip = sounds[1];
        }
    }

    /// <summary>
    /// Determines what to do when coin is collected
    /// </summary>
    public void Collect(int multiplier)
    {
        //Play sound, update score, visually disable coin, and then destory
        _as.Play();
        HUD_Manager.instance.UpdateScore(Value * multiplier);
        visualCoin.SetActive(false);
        Destroy(this.gameObject, _as.clip.length);
    }
}
