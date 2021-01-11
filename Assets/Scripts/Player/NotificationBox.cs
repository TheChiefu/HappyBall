using UnityEngine;

/// <summary>
/// Display box above player that shows relevant information
/// </summary>
public class NotificationBox : MonoBehaviour
{
    [Header("Settings:")]
    [SerializeField] private TMPro.TextMeshProUGUI box;
    [SerializeField] private float disableTime = 1f;

    private void Awake()
    {
        if (box == null) box.GetComponent<TMPro.TextMeshProUGUI>();
        if (box != null) box.text = string.Empty;
    }




    /// <summary>
    /// Disbles GUI over period of given time
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private System.Collections.IEnumerator DisableTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        box.text = string.Empty;
    }

    /// <summary>
    /// Update the notification text to display any relevant information
    /// </summary>
    /// <param name="text"></param>
    public void UpdateTextBox(string text)
    {
        //Update text
        box.text = text;

        //Disable text from timer if necssary
        if (disableTime > 0) StartCoroutine(DisableTimer(disableTime));
    }
}
