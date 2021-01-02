using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("General")]
    public Rigidbody ball;
    public float force = 100;
    public float jumpForce = 200;
    public float maxSpeed = 50;
    public bool isGrounded = true;

    [Header("Animation")]
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject endParticles;
    [SerializeField] private GameObject floatingTitle;

    [Header("Cosmetic")]
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
        _anim = GetComponentInChildren<Animator>();
        ball = GetComponentInChildren<Rigidbody>();
        if(_sm == null)
        {
            _sm = GameManager.instance.mainCamera.GetComponent<SmartCam>();
        }
    }

    private void Update()
    {
        LimitVelocity();
    }

    private void FixedUpdate()
    {

        //Do not allow movement mid-air
        if (isGrounded)
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


    private void LimitVelocity()
    {
        if(ball.angularVelocity.magnitude > maxSpeed)
        {
            ball.angularVelocity.Set(maxSpeed, maxSpeed, maxSpeed);
        }

        if(ball.velocity.magnitude > maxSpeed)
        {
            ball.velocity.Set(maxSpeed, maxSpeed, maxSpeed);
        }

        if (ball.angularVelocity.magnitude > -maxSpeed)
        {
            ball.angularVelocity.Set(-maxSpeed, -maxSpeed, -maxSpeed);
        }

        if (ball.velocity.magnitude > -maxSpeed)
        {
            ball.velocity.Set(-maxSpeed, -maxSpeed, -maxSpeed);
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

    public void EndLevelAnimation()
    {
        ball.velocity = Vector3.zero;
        ball.isKinematic = true;

        _anim.enabled = true;
        _anim.SetBool("BeatLevel", true);
        endParticles.SetActive(true);

        if (floatingTitle != null) floatingTitle.SetActive(false);
    }
}