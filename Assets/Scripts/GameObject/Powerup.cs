using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Powerup : MonoBehaviour
{
    [Header("Properties:")]
    [Tooltip("How long the powerup will last for")]
    [SerializeField] private float time = 1f;
    [Tooltip("How much effect this powerup will have")]
    [SerializeField] private float effectPower = 1f;
    [SerializeField] private bool respawnable = false;
    [SerializeField] private float respawnTime = 10f;
    
    [Tooltip("The visual gameobject represented in the world")]
    [SerializeField] private GameObject visual = null;

    [SerializeField]
    public enum PowerupType
    {
        Speed,
        Light,
        Heavy,
        Invincible,
        Multiplier,
        AddHealth
    }
    public PowerupType type;

    [Header("Cosmetic:")]
    /// <summary>
    /// Sprite Index Order:
    /// 0 - Shoes (Speed)
    /// 1 - Feather (Light Weight)
    /// 2 - Weight (Heavy Weight)
    /// 3 - Shield (Invincible)
    /// 4 - Multiplier (Multiplier)
    /// 5 - Heart (Add Health)
    /// </summary>
    [SerializeField] private Sprite[] images;
    [SerializeField] private AudioClip[] pickupSound; //Same index order as sprite
    [SerializeField] private Color[] colors; //Same index order as sprite
    [SerializeField] private Image displaySprite;
    [SerializeField] GameObject visualObject = null;  //Visual child object
    private AudioSource _as = null; 

    //Set sprite image on awake
    private void Awake()
    {
        if (displaySprite != null) displaySprite.sprite = images[(int)type];

        _as = GetComponent<AudioSource>();
        _as.clip = pickupSound[(int)type];

        switch (type)
        {
            case PowerupType.Speed:
                displaySprite.sprite = images[0];
                displaySprite.color = colors[0];
                break;

            case PowerupType.Light:
                displaySprite.sprite = images[1];
                displaySprite.color = colors[1];
                break;

            case PowerupType.Heavy:
                displaySprite.sprite = images[2];
                displaySprite.color = colors[2];
                break;

            case PowerupType.Invincible:
                displaySprite.sprite = images[3];
                displaySprite.color = colors[3];
                break;

            case PowerupType.Multiplier:
                displaySprite.sprite = images[4];
                displaySprite.color = colors[4];
                break;

            case PowerupType.AddHealth:
                displaySprite.sprite = images[5];
                displaySprite.color = colors[5];
                break;

            default:
                displaySprite.sprite = images[1];
                displaySprite.color = colors[1];
                break;
        }

    }

    /// <summary>
    /// From given player perform powerup action based on chosen enum
    /// </summary>
    /// <param name="player"></param>
    public void Collect(PlayerController player)
    {
        if(effectPower != 0)
        {
            switch (type)
            {
                case PowerupType.Speed:
                    player.StartCoroutine(player.ModifySpeed(effectPower, time));
                    break;

                case PowerupType.Light:
                    player.StartCoroutine(player.ModifyGravity(1 / effectPower, time));
                    break;

                case PowerupType.Heavy:
                    player.StartCoroutine(player.ModifyGravity(effectPower, time));
                    break;

                case PowerupType.Invincible:
                    player.StartCoroutine(player.MakeInvinsible(time));
                    break;

                case PowerupType.Multiplier:
                    player.StartCoroutine(player.ModifyMultiplier((int)effectPower, time));
                    break;

                case PowerupType.AddHealth:
                    player.Heal((int)effectPower);
                    break;

                default:
                    Debug.LogError("No powerup chosen");
                    break;
            }
        }
        else
        {
            Debug.LogError("Effect power on : " + this.name + " is set to 0");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _as.Play();
            visualObject.gameObject.SetActive(false);

            if (respawnable)
            {
                visual.SetActive(false);
                StartCoroutine(Respawn());
            }
            else Destroy(this.gameObject, _as.clip.length);
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        visual.SetActive(true);

    }
}
