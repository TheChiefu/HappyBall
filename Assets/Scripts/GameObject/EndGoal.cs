using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Decides how the end goal functions and how it interacts with other objects
/// </summary>
public class EndGoal : MonoBehaviour
{
    /// <summary>
    /// Level Manager for condition requirements checking
    /// </summary>
    private LevelManager _lm;

    [SerializeField] private bool inEditor = false;


    [Header("General")]
    //Position offset in which player is put in relative to end goal
    [SerializeField] private Vector3 offset = new Vector3(0,2,0);
    [SerializeField] private Cinemachine.CinemachineVirtualCamera goalCam;
    [SerializeField] private float winToExitDelay = 2f;

    [Header("Notification:")]
    [SerializeField] private float displayTime = 2f;
    [SerializeField] private GameObject notificationCanvas;
    [SerializeField] private TextMeshProUGUI textArea;
    [SerializeField] private MultilanguageSO[] text;

    private void Start()
    {
        _lm = LevelManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //If conditions met end level other wise notify player
            if (WinConditionMet()) StartCoroutine(EndLevel(other.gameObject, winToExitDelay));
            else
            {

            }
        }
    }

    /// <summary>
    /// Check if winning conditions are met and checks multiple conditions before returning result
    /// </summary>
    /// <returns></returns>
    private bool WinConditionMet()
    {
        //Coin requirement check
        if(_lm.WinScore > 0)
        {
            if (_lm.totalScore >= _lm.WinScore) return true;
            else
            {
                //Not enough coins
                textArea.text = text[0].GetText(GameManager.instance.languageIndex) + string.Format("{0}/{1}",_lm.totalScore, _lm.WinScore);

                StartCoroutine(DisplayNotification(displayTime));
                return false;
            }
        }

        //Switch check requirement
        if (_lm.WinSwitches > 0)
        {
            if (_lm.switchesActivated >= _lm.WinSwitches) return true;
            else
            {
                //Not enough switches activated
                textArea.text = text[1].GetText(GameManager.instance.languageIndex) + string.Format("{0}/{1}", _lm.switchesActivated, _lm.WinSwitches);

                StartCoroutine(DisplayNotification(displayTime));
                return false;
            }
        }

        //When nothing is required
        return true;
    }

    private IEnumerator DisplayNotification(float time)
    {
        notificationCanvas.SetActive(true);
        yield return new WaitForSeconds(time);
        notificationCanvas.SetActive(false);
    }

    private IEnumerator EndLevel(GameObject player, float delay)
    {
        //Look Camera at goal
        goalCam.enabled = true;

        //Put player in center of goal and look at camera
        player.transform.LookAt(goalCam.transform.position);
        player.transform.position = transform.position + offset;

        //Play player goal animation
        player.GetComponentInParent<PlayerController>().EndLevelAnimation();

        yield return new WaitForSeconds(delay);

        _lm.EndLevel();
    }

    //Just in case
    public void ToggleCamera()
    {
        goalCam.enabled = !goalCam.enabled;
    }

    private void OnTriggerExit(Collider other)
    {
        goalCam.enabled = false;
    }
}
