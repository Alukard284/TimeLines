using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeController : MonoBehaviour
{
    [Header("Input")]
    private float moveInput;
    private float steerInput;
    private float currentVelosityOffset;

    [Header("References")]
    public Rigidbody sphereRB;
    public Rigidbody bikeBody;
    public GameObject handle;
    public TrailRenderer skidMarks;
    public AudioSource engineSound;
    public AudioSource skidSound;
    RaycastHit hit;
    public GameObject frontTyre;
    public GameObject backTyre;

    [Header("Movement Settings")]
    public float maxSpeed;
    public float acceleration;
    public float steerStrength;
    [Range(1, 10)]public float brakingFactor;
    private float rayLength;
    public LayerMask derivableSurface;
    public float tiltAngle;
    public float bikeXTiltIncrement = .09f;
    public float zTiltAngle = 45f;
    public float gravity;
    public Vector3 velocity;
    public float handleRotVal = 15f;
    public float handleRotSpeed = .15f;
    public float skidWidth = 0.62f;
    public float minSkidVelosity = 10f;
    [Range(0, 1)] public float minPitch;
    [Range(1, 5)] public float maxPitch;
    public float tyreRotSpeed = 10000f;

    private void Start()
    {
        sphereRB.transform.parent = null;
        bikeBody.transform.parent = null;

        rayLength = sphereRB.GetComponent<SphereCollider>().radius + 0.2f;
        //visuals
        skidMarks.startWidth = skidWidth;
        skidMarks.emitting = false;
        //sfx
        skidSound.mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        BikePlayerInput();
    }

    private void BikePlayerInput()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
        transform.position = sphereRB.position;

        velocity = bikeBody.transform.InverseTransformDirection(bikeBody.velocity);
        currentVelosityOffset = velocity.z / maxSpeed;
        
        
    }

    void FixedUpdate()
    {
        Movement();
        SkidMarks();
        //sfx
        EngineSound();
        frontTyre.transform.Rotate(Vector3.right, Time.deltaTime * tyreRotSpeed * currentVelosityOffset);
        backTyre.transform.Rotate(Vector3.right, Time.deltaTime * tyreRotSpeed * currentVelosityOffset);
    }

    void Movement()
    {
        if (Grounded())
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                Acceleration();
                Rotation();
            }
            Brake();
        }
        else 
        {
            Gravity();
        }
        BikeTilt();
    }

    void Acceleration()
    {
        sphereRB.velocity = Vector3.Lerp(sphereRB.velocity, maxSpeed * moveInput * transform.forward, Time.fixedDeltaTime * acceleration);
    }

    void Rotation()
    {
        transform.Rotate(0, steerInput * moveInput * currentVelosityOffset * steerStrength * Time.fixedDeltaTime, 0, Space.World);
        //visuals
        handle.transform.localRotation = Quaternion.Slerp(handle.transform.localRotation, Quaternion.Euler(handle.transform.localRotation.eulerAngles.x, handleRotVal * steerInput, handle.transform.localRotation.eulerAngles.z), handleRotSpeed);

    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            sphereRB.velocity *= brakingFactor / 10;
        }
    }

    void BikeTilt()
    {
        float xRot = (Quaternion.FromToRotation(bikeBody.transform.up, hit.normal) * bikeBody.transform.rotation).eulerAngles.x;
        float zRot = 0;
        if (currentVelosityOffset > 0)
        {
            zRot = -zTiltAngle * steerInput * currentVelosityOffset;
        }
        Quaternion targetRot = Quaternion.Slerp(bikeBody.transform.rotation, Quaternion.Euler(xRot, transform.eulerAngles.y, zRot), bikeXTiltIncrement);
        Quaternion newRotation = Quaternion.Euler(targetRot.eulerAngles.x, transform.eulerAngles.y, targetRot.eulerAngles.z);
        bikeBody.MoveRotation(newRotation);
    }

    bool Grounded()
    {
        if (Physics.Raycast(sphereRB.position, Vector3.down, out hit, rayLength, derivableSurface))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Gravity()
    {
        sphereRB.AddForce(gravity * Vector3.down, ForceMode.Acceleration);
    }

    void SkidMarks()
    {
        if (Grounded() && Mathf.Abs(velocity.x) > minSkidVelosity)
        {
            skidMarks.emitting = true;
            skidSound.mute = false;
        }
        else 
        { 
            skidMarks.emitting = false;
            skidSound.mute = true;
        }
    }

    void EngineSound()
    {
        engineSound.pitch = Mathf.Lerp(minPitch, maxPitch, Mathf.Abs(currentVelosityOffset));
    }

}
