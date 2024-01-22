using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselMovementBehaviour : MonoBehaviour
{
    [Header("Wanted Speeds")]
    [SerializeField]
    float wantedSpeed = 0f;
    [SerializeField]
    float wantedRotationSpeed = 0f;

    [Header("Movement Settings")]
    [SerializeField]
    private float speed = 0f;
    [SerializeField]
    private float accelerationSpeed = 10.0f;
    [SerializeField]
    private float rotationSpeed = 0f;
    [SerializeField]
    private float rotationAccelerationSpeed = 10.0f;
    //[SerializeField]
    private float maxSpeed = 10f;
    //[SerializeField]
    private float maxRotationSpeed = 10f;
    //[SerializeField]
    private float drag = 0.023f;
    //[SerializeField]
    private float rotationDrag = 0.1f;

    [SerializeField]
    private ControlsUIBehaviour controlsUI;


    [SerializeField]
    private float standardDrag = 0.023f;
    [SerializeField]
    private float standardRotationDrag = 0.1f;
    [SerializeField]
    private float standardMaxSpeed = 10f;
    [SerializeField]
    private float standardMaxRotationSpeed = 10f;


    [SerializeField]
    private float maxDamageDrag = 0.05f;
    [SerializeField]
    private float maxDamageRotationDrag = 0.5f;


    private void Awake()
    {
        drag = standardDrag;
        rotationDrag = standardRotationDrag;
        maxSpeed = standardMaxSpeed;
        maxRotationSpeed = standardMaxRotationSpeed;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyDrag();

        controlsUI.CalculateSpeed(speed);

        Accelerate(wantedSpeed);
        Rotate(wantedRotationSpeed);
        
    }


    void Accelerate(float wantedSpeed)
    {

        float speedGoal = Mathf.Clamp(wantedSpeed, 0, maxSpeed);

        if (speedGoal > speed)
        {

            speed += accelerationSpeed * Time.fixedDeltaTime;

            speed = Mathf.Clamp(speed, 0, speedGoal);
        }

        transform.Translate(transform.right * speed * Time.fixedDeltaTime, Space.World);
    }


    void Rotate(float wantedRotationSpeed)
    {
        float rotationGoal = Mathf.Clamp(wantedRotationSpeed, -maxRotationSpeed, maxRotationSpeed);

        if (rotationGoal > rotationSpeed)
        {
            rotationSpeed += rotationAccelerationSpeed * Time.fixedDeltaTime;
            rotationSpeed = Mathf.Clamp(rotationSpeed, -maxRotationSpeed, rotationGoal);
        }
        else if (rotationGoal < rotationSpeed)
        {
            rotationSpeed -= rotationAccelerationSpeed * Time.fixedDeltaTime;
            rotationSpeed = Mathf.Clamp(rotationSpeed, rotationGoal, maxRotationSpeed);
        }


        transform.Rotate(transform.up, rotationSpeed * Time.fixedDeltaTime);


    }


    void ApplyDrag()
    {
        speed -= drag;
        speed = Mathf.Clamp(speed, 0, maxSpeed);

        if (rotationSpeed > 0)
        {
            rotationSpeed -= rotationDrag;
            rotationSpeed = Mathf.Clamp(rotationSpeed, 0, maxRotationSpeed);
        }
        else if (rotationSpeed < 0)
        {
            rotationSpeed += rotationDrag;
            rotationSpeed = Mathf.Clamp(rotationSpeed, -maxRotationSpeed, 0);
        }


    }




    // Get the wanted speed and rotation speed from the XR interaction script

    public void SetWantedSpeed(float handleValue)
    {
        // Value between -1 0 and 1
        // normalised to 0 and 1
        // formula: (value - min) / (max - min)
        // 0 = no speed
        // 1 = max speed

        handleValue = (handleValue + 1) / 2;
        wantedSpeed = handleValue * maxSpeed;

    }

    public void SetWantedRotationSpeed(float rotationSpeed)
    {
        // Value between 0 and 1
        // 0 = max rotation speed to the left
        // 1 = max rotation speed to the right
        // 0.5 = no rotation

        wantedRotationSpeed = -maxRotationSpeed + rotationSpeed * 2 * maxRotationSpeed;
    }


    public void ApplyDamage(float health, float maxHealth)
    {
        // Value between 0 and maxHealth
        // 0 = max damage
        // maxHealth = no damage

        float damage = maxHealth - health;

        float modifier = damage / maxHealth;

        drag = standardDrag + maxDamageDrag * modifier;

        rotationDrag = standardRotationDrag + maxDamageRotationDrag * modifier;

        maxSpeed = standardMaxSpeed - standardMaxSpeed * modifier;

        maxRotationSpeed = standardMaxRotationSpeed - standardMaxRotationSpeed * modifier;



    }

}
