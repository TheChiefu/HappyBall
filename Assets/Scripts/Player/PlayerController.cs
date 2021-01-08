using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("General")]
    [SerializeField] private Rigidbody ball;
    [SerializeField] private float force = 100;
    [SerializeField] private float jumpForce = 200;
    [SerializeField] private float maxSpeed = 50;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool isDead = false;
    [SerializeField] private int health = 3;

    //Move Type Values
    // 0 - Move with camera
    // 1 - World movement (X,Y,Z) to global directions
    // 2 - World movement in 2D
    [Range(0,2)]
    [SerializeField] private int moveType = 0;

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


    //Inital Value Holders
    [SerializeField] private float initalForce;
    [SerializeField] float initalJumpForce;
    [SerializeField] private float initalMass;
    [SerializeField] private bool canHurt = true;

    private void Start()
    {
        if (_im == null)        _im = InputManager.instance;
        if (_lm == null)        _lm = LevelManager.instance;
        if (_ac == null)        _ac = GetComponent<AudioSource>();
        if (_anim == null)      _anim = GetComponentInChildren<Animator>();
        if (_sm == null)
        { 
            //Check for GameManager
            if(_sm == null) _sm = GameManager.instance.mainCamera.GetComponent<SmartCam>();

            //Then check in scene
            if (_sm == null) _sm = Camera.main.GetComponent<SmartCam>(); Debug.LogError("Found in main camera search, could not find in GameManager");
        }
        if (ball == null)       ball = GetComponentInChildren<Rigidbody>();
        if (notificationBox == null)
        {
            //Attempt to find in scene
            notificationBox = GameObject.FindGameObjectWithTag("Notification").GetComponent<NotificationBox>();

            //If still can't find box push error
            if(notificationBox == null) Debug.LogError("Attach the notification box reference to " + this.name);
        }

        if(ball != null)
        {
            initalForce = force;
            initalJumpForce = jumpForce;
            initalMass = ball.mass;
        }
    }

    //Update player movement
    private void FixedUpdate()
    {
        //Do not allow movement mid-air
        if (isGrounded)
        {
            switch (moveType)
            {
                case 0:
                    Movement(null, true, true);
                    break;
                case 1:
                    Movement(_sm.transform, false, true);
                    break;
                case 2:
                    Movement(null, true, false);
                    break;
            }
        }
    }






    /// <summary>
    /// Damage player by given amount
    /// </summary>
    /// <param name="amount"></param>
    public void Damage(int amount)
    {
        Debug.Log("Ow");

        //Integer underflow check
        if (canHurt)
        {
            health = checked(health - amount);
            HUD_Hearts.instance.UpdateHearts(-amount);
        }

        if (health <= 0)
        {
            Died();
            isDead = true;
        }
        else
        {
            isDead = false;
        }
    }

    /// <summary>
    /// Add given amount of health to player
    /// </summary>
    /// <param name="amount"></param>
    public void Heal(int amount)
    {
        //Accounts for integer overflow
        health = checked(health + amount);
        HUD_Hearts.instance.UpdateHearts(amount);
    }

    private void Died()
    {
        Debug.Log("U Ded");
    }




    // Player Power Up Conditions //

    /// <summary>
    /// Modify the gravity of this object for a given amount of time
    /// Note: Multiplies ball mass by amount
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="time"></param>
    public IEnumerator ModifyGravity(float amount, float time)
    {
        ball.mass *= amount;
        yield return new WaitForSeconds(time);
        ball.mass = initalMass;
    }

    /// <summary>
    /// Modify the force player can exert over a given amount a of time
    /// Adds ball mass to amount
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator ModifySpeed(float amount , float time)
    {
        force += amount;
        yield return new WaitForSeconds(time);
        force = initalForce;
    }

    /// <summary>
    /// Make player invinsible for given amount of time
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator MakeInvinsible(float time)
    {
        canHurt = false;
        yield return new WaitForSeconds(time);
        canHurt = true;
    }


    /// <summary>
    /// Move ball relative or target or world axis. First check for target to move with it's local axis. Then check for world movement, and finally 2D movement
    /// </summary>
    /// <param name="target"></param>
    /// <param name="WorldMove"></param>
    /// <param name="canMoveIn3D"></param>
    private void Movement(Transform target, bool WorldMove, bool canMoveIn3D)
    {
        //For fixed camera angles
        if(canMoveIn3D)
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

    /// <summary>
    /// Change movement type of player given an integer value
    /// </summary>
    /// <param name="value"></param>
    public void ChangeMovementType(int value)
    {
        if(value < 0 || value > 3) Debug.LogError("Value is outside the movement range");
        else
        {
            moveType = value;
        }
    }










    // Getters and Setters //


    /// <summary>
    /// Returns current health value on player
    /// </summary>
    /// <returns></returns>
    public int GetHealth()
    {
        return this.health;
    }

    /// <summary>
    /// Can set or check if player is grounded or not
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        return this.isGrounded;
    }
    public void IsGrounded(bool value)
    {
        this.isGrounded = value;
    }
}