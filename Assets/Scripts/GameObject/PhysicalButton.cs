using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Collision based button that calls Unity UI button for event handling
/// </summary>
public class PhysicalButton : MonoBehaviour
{
    [Header("Cosmetics")]
    [SerializeField] private Animator buttonAnimator;
    [SerializeField] private AudioClip sfx;
    [SerializeField] private MultilanguageSO[] notificationText; //0 - Weight Text | 1 - Wrong Tag

    [Header("Button Requirements")]
    [SerializeField] private bool allowPlayer = true;
    [SerializeField] private bool allowPhysics = true;
    [SerializeField] private bool isRepeatable = true;
    [SerializeField] private float weightRequirement = 0;
    [SerializeField] private Button actionButton;
    private AudioSource _as;

    [Header("Etc")]
    [SerializeField] private float massOnButton = 0;

    //Assignment on start of loading
    private void Awake()
    {
        if(_as == null) _as = GetComponent<AudioSource>();
        if(_as != null) _as.clip = sfx;

        if (buttonAnimator == null) buttonAnimator = GetComponent<Animator>();
        if (buttonAnimator != null) buttonAnimator.SetBool("Repeatable", isRepeatable);

        if (actionButton == null) GetComponent<Button>();
    }

    /// <summary>
    /// Checks if object can press button or not
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    private bool CheckRequirement(GameObject gameObject)
    {
        //Returning bool
        bool passed = false;

        //Player Tag check
        if (gameObject.tag == "Player")
        {

            //Mass Requirement Check -- FIX TRY TO IMPLEMENT WAYS OF DETCING ALL MASS ON BUTTON
            if (weightRequirement > 0)
            {
                //Check if player is heavy enough
                if (massOnButton >= weightRequirement) passed = true;
                else
                {
                    //If not notify player
                    string notification = notificationText[0].GetText(GameManager.instance.languageIndex);
                    PlayerController PC = gameObject.GetComponentInParent<PlayerController>();
                    PC.notificationBox.UpdateTextBox(notification);
                    passed = false;
                }
            }

            //Check if player can press button
            if (allowPlayer) passed = true;
            else
            {
                //If it can't notify player
                string notification = notificationText[1].GetText(GameManager.instance.languageIndex);
                PlayerController PC = gameObject.GetComponentInParent<PlayerController>();
                PC.notificationBox.UpdateTextBox(notification);
                passed = false;
            }
        }

        //Physics object tag check
        if (gameObject.tag == "Physics")
        {

            if (allowPhysics) passed = true;
            else passed = false;
        }

        //Mass Requirement Check
        if (weightRequirement > 0)
        {
            if (massOnButton >= weightRequirement) passed = true;
            else passed = false;
        }


        //Return result of check
        return passed;
    }

    /// <summary>
    /// When object enters trigger zone, activate button
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>() != null)
        {
            massOnButton += other.GetComponent<Rigidbody>().mass;
        }

        //Check if object can press button, even if it can also check if it can be pressed again
        if (CheckRequirement(other.gameObject) & actionButton.interactable)
        {
            actionButton.onClick.Invoke();
            if (!isRepeatable) actionButton.interactable = false;
            buttonAnimator.SetBool("Pressed", true);
            _as.Play();
        }
    }

    /// <summary>
    /// When object leaves trigger, set button to false
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            massOnButton -= other.GetComponent<Rigidbody>().mass;
        }

        if (CheckRequirement(other.gameObject)) 
            buttonAnimator.SetBool("Pressed", false);
    }
}
