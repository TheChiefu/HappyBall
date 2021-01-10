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

    [Header("General")]
    [SerializeField] private Vector3 offset = new Vector3(0,2,0);               //Position offset in which player is put in relative to end goal
    [SerializeField] private Cinemachine.CinemachineVirtualCamera goalCam;      //Goal Camera that superscedes Main Follow Camera in priority

    [Header("Notification:")]
    [SerializeField] private float displayTime = 2f;
    [SerializeField] private GameObject notificationCanvas;
    [SerializeField] private TextMeshProUGUI textArea;
    [SerializeField] private MultilanguageSO[] text;

    //Find LevelManager reference on start
    private void Start()
    {
       if(_lm == null) _lm = LevelManager.instance;
    }

    /// <summary>
    /// Check if player has beaten level when they enter the end goal
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //If conditions met end level other wise notify player
            if (WinConditionMet()) EndLevel(other.gameObject);
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
            if (_lm.GetScore() >= _lm.WinScore) return true;
            else
            {
                //Not enough coins
                textArea.text = string.Format("{2}: {0}/{1}", _lm.GetScore(), _lm.WinScore, text[0].GetText(GameManager.instance.languageIndex));

                StartCoroutine(DisplayNotification(displayTime));
                return false;
            }
        }

        //Switch check requirement
        if (_lm.WinSwitches > 0)
        {
            if (_lm.GetTotalSwitches() >= _lm.WinSwitches) return true;
            else
            {
                //Not enough switches activated
                textArea.text = string.Format("{2}: {0}/{1}", _lm.GetTotalSwitches(), _lm.WinSwitches, text[1].GetText(GameManager.instance.languageIndex));

                StartCoroutine(DisplayNotification(displayTime));
                return false;
            }
        }

        //When nothing is required
        return true;
    }

    /// <summary>
    /// Timed notification display, that shows player the necessary requirements left to beat the level.
    /// Based on a timer in seconds
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator DisplayNotification(float time)
    {
        notificationCanvas.SetActive(true);
        yield return new WaitForSeconds(time);
        notificationCanvas.SetActive(false);
    }

    /// <summary>
    /// Function that ends level itself and plays animation for player.
    /// Calls the LevelManager to fully end level
    /// </summary>
    /// <param name="player"></param>
    private void EndLevel(GameObject player)
    {
        //Look Camera at goal
        goalCam.enabled = true;

        //Put player in center of goal and look at camera
        player.transform.LookAt(goalCam.transform.position);
        player.transform.position = transform.position + offset;

        //Play player goal animation
        player.GetComponentInParent<PlayerController>().EndLevelAnimation();

        _lm.EndLevel();
    }
}
