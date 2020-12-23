using UnityEngine;

/// <summary>
/// Simple script to have 3D text always face camera
/// </summary>
public class BillboardText : MonoBehaviour
{
    TMPro.TextMeshProUGUI text = null;

    private void Awake()
    {
        text = this.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        text.transform.LookAt(GameManager.instance.mainCamera.transform);
    }
}
