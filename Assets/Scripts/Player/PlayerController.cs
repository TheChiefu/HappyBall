using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody ball;
    public float force = 100;
    public float jumpForce = 200;
    public bool isGrounded = true;

    /// <summary>
    /// Movement type based on index (for movement type swtching on the fly)
    /// 0 - Default: Global world movement, Forward is Z, Left/Right is X Axis, and Up and Y
    /// 1 - Target: Moves relative to target's local movement axis
    /// 2 - Target: But sidescrolling
    /// </summary>
    public int movementIndex = 0;

    private InputManager _im;
    [SerializeField] private SmartCam _sm;

    private void Start()
    {
        _im = InputManager.instance;
        ball = GetComponentInChildren<Rigidbody>();
        if(_sm == null)
        {
            _sm = GameManager.instance.mainCamera.GetComponent<SmartCam>();
        }
    }

    private void FixedUpdate()
    {

        //Do not allow movement mid-air
        if(isGrounded)
        {
            switch (movementIndex)
            {
                case 0:
                    GlobalMovement();
                    break;
                case 1:
                    TargetMovement(_sm.transform, false);
                    break;
                case 2:
                    TargetMovement(_sm.transform, true);
                    break;
                default:
                    break;
            }
        }
    }


    /// <summary>
    /// Move ball in 3D motion with static angles (non-depent on camera)
    /// </summary>
    private void GlobalMovement()
    {
        //Left
        if (_im.Movement.x < 0)
        {
            ball.AddForce(Vector3.right * -force);
        }

        //Right
        if (_im.Movement.x > 0)
        {
            ball.AddForce(Vector3.right * force);
        }

        //Forward
        if (_im.Movement.y < 0)
        {
            ball.AddForce(Vector3.forward * -force);
        }

        //Backward
        if (_im.Movement.y > 0)
        {
            ball.AddForce(Vector3.forward * force);
        }

        //Jump
        if (_im.Jump)
        {
            ball.AddForce(Vector3.up * jumpForce);
        }
    }


    //Move ball relative to given target
    private void TargetMovement(Transform target, bool sideScrolling)
    {
        //Allow for 3D movement when not side-scrolling
        if (!sideScrolling)
        {
            //Forward
            if (_im.Movement.y < 0)
            {
                ball.AddForce(target.forward * -force);
            }

            //Backward
            if (_im.Movement.y > 0)
            {
                ball.AddForce(target.forward * force);
            }
        }

        //Left
        if (_im.Movement.x < 0)
        {
            ball.AddForce(target.right * -force);
        }

        //Right
        if (_im.Movement.x > 0)
        {
            ball.AddForce(target.right * force);
        }

        //Jump
        if (_im.Jump)
        {
            ball.AddForce(Vector3.up * jumpForce);
        }
    }
}