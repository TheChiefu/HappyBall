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
    [SerializeField] private SmartCam _sm;
    [SerializeField] private AudioClip[] jumpSounds;
    public NotificationBox notificationBox;
    private InputManager _im;
    private AudioSource _ac;
    private LevelManager _lm;

    private void Start()
    {
        if (_im == null)        _im = InputManager.instance;
        if (_lm == null)        _lm = LevelManager.instance;
        if (_ac == null)        _ac = GetComponent<AudioSource>();
        if (_anim == null)      _anim = GetComponentInChildren<Animator>();
        if (_sm == null)        _sm = GameManager.instance.mainCamera.GetComponent<SmartCam>();
        if (ball == null)       ball = GetComponentInChildren<Rigidbody>();
        if (notificationBox == null) Debug.LogError("Attach the notification box reference to " + this.name);
    }

    //Update player movement
    private void FixedUpdate()
    {
        //Do not allow movement mid-air
        if (isGrounded)
        {
            if (_lm.cameraMode == 0) Movement(null, true);
            else if (_lm.cameraMode == 1) Movement(_sm.transform, false);
        }
    }







    /// <summary>
    /// Move ball relative or target or world axis
    /// </summary>
    /// <param name="target"></param>
    /// <param name="WorldMove"></param>
    private void Movement(Transform target, bool WorldMove)
    {
        //Forward
        if (_im.Movement.y < 0)
        {
            if (WorldMove) ball.AddForce(Vector3.forward * -force);
            else ball.AddForce(target.forward * -force);
        }

        //Backward
        if (_im.Movement.y > 0)
        {
            if (WorldMove) ball.AddForce(Vector3.forward * force);
            else ball.AddForce(target.forward * force);
        }

        //Left
        if (_im.Movement.x < 0)
        {
            if (WorldMove) ball.AddForce(Vector3.right * -force);
            else ball.AddForce(target.right * -force);
        }

        //Right
        if (_im.Movement.x > 0)
        {
            if (WorldMove) ball.AddForce(Vector3.right * force);
            else ball.AddForce(target.right * force);
        }

        //Jump
        if (_im.Jump)
        {
            if (WorldMove) ball.AddForce(Vector3.up * jumpForce);
            else ball.AddForce(Vector3.up * jumpForce);

            //Play sound on jump
            _ac.clip = jumpSounds[Random.Range(0, jumpSounds.Length)];
            _ac.Play();
        }
    }

    /// <summary>
    /// Set end of level animations and features
    /// </summary>
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