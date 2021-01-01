using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody ball;
    public float force = 100;
    public float jumpForce = 200;
    public bool isGrounded = true;

    private InputManager _im;
    [SerializeField] private SmartCam _sm;
    [SerializeField] private AudioClip[] jumpSounds;
    private AudioSource _ac;

    private LevelManager _lm;

    private void Start()
    {
        _im = InputManager.instance;
        _lm = LevelManager.instance;
        _ac = GetComponent<AudioSource>();
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
            switch (_lm.cameraMode)
            {
                case 0:
                    GlobalMovement();
                    break;
                case 1:
                    TargetMovement(_sm.transform);
                    break;
                case 2:
                    TargetMovement(_sm.transform);
                    break;
                case 3:
                    GlobalMovement();
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
    private void TargetMovement(Transform target)
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
            _ac.clip = jumpSounds[Random.Range(0, jumpSounds.Length)];
            _ac.Play();
            ball.AddForce(Vector3.up * jumpForce);
        }
    }
}