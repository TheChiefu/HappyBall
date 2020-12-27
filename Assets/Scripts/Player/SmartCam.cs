using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartCam : MonoBehaviour
{
    public Transform target;
    public float distance;
    public Vector3 offset;

    [SerializeField]
    private InputManager _im;
    private float currentX = 0;
    private float currentY = 0;

    private void Start()
    {
        _im = InputManager.instance;
    }

    public void Update()
    {
        if (!GameManager.instance.gameIsPaused)
        {
            
        }

        if (target != null)
        {
            currentX += _im.CameraRotation.x * _im.CameraSensitivity;
            currentY += _im.CameraRotation.y * _im.CameraSensitivity;
            Orbit();
        }
    }

    /// <summary>
    /// Orbit around the target, rotates while looking around target depending on input
    /// </summary>
    private void Orbit()
    {
        Vector3 dir = new Vector3(0f, 1f, -distance);
        Quaternion rot = Quaternion.Euler(currentY, currentX, 0f);

        //Set position distance away from target alongside rotation around target
        transform.position = (target.position + rot * dir);
        transform.LookAt(target.position + offset);
    }
}
