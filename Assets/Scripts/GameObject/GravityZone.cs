using UnityEngine;


/// <summary>
/// Trigger zone that affects the physics object's properties while in it
/// </summary>
public class GravityZone : MonoBehaviour
{
    [Tooltip("Multiplies object's mass to simulate change in gravity. Cannot be negative")]
    [SerializeField] private float massMultiplier = 1f;
    [SerializeField] private BoxCollider field = null;
    [SerializeField] private MeshRenderer fieldArea = null;
    [SerializeField] private ParticleSystem particles = null;
    ParticleSystem.MainModule particleMain;

    //Set colors and values on start
    private void Awake()
    {
        if (field == null) GetComponent<BoxCollider>();
        if (fieldArea == null) GetComponent<MeshRenderer>();
        if (particles == null) GetComponentInChildren<ParticleSystem>();

        //Check for negative values
        if (massMultiplier < 0) massMultiplier = 0;

        //Old way was obsolete so had to do workaround
        particleMain = particles.main;
        particleMain.gravityModifierMultiplier = massMultiplier;
        particleMain.startColor = GetColor(massMultiplier);

        //Set field color
        fieldArea.material.color = GetColor((int)massMultiplier);
    }

    //Once object enters modify gravity of it
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") other.GetComponentInParent<PlayerController>().ModifyGravity(massMultiplier);
        if (other.tag == "PhysicsObject") other.GetComponent<PhysicsObject>().ModifyGravity(massMultiplier);
    }

    //Reset mass of object on exit
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") other.GetComponentInParent<PlayerController>().ResetGravity();
        if (other.tag == "PhysicsObject") other.GetComponent<PhysicsObject>().ResetGravity();
    }


    /// <summary>
    /// Gets color of field depenent on given value. Blue for low and Red for high.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private Color GetColor(float value)
    {
        if (massMultiplier > 1) return new Color(1, 0, 0, 0.5f);
        else return new Color(0, 0, 1, 0.5f);
    }

    /// <summary>
    /// Update effects in real time (editor)
    /// </summary>
    private void OnDrawGizmos()
    {
        particleMain = particles.main;
        particleMain.gravityModifierMultiplier = massMultiplier;

        fieldArea.sharedMaterial.color = GetColor(massMultiplier);
    }
}
