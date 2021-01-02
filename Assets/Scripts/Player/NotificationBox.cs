using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Display box above player that shows relevant information
/// </summary>
public class NotificationBox : MonoBehaviour
{
    [SerializeField] private TextMeshPro box;
    [SerializeField] private float disableTime = 1f;


    //Disable time when necessary
    private IEnumerator DisableTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        box.enabled = false;
    }

    private void Awake()
    {
        if (box == null) box.GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Update the notification text to display any relevant information
    /// </summary>
    /// <param name="text"></param>
    public void UpdateTextBox(string text)
    {
        //Turn on box
        box.enabled = true;

        //Update text
        box.text = text;

        //Disable text from timer if necssary
        if (disableTime > 0) StartCoroutine(DisableTimer(disableTime));
        box.enabled = false;
    }
}
