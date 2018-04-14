using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject target;

	//Where the enemy wants to move to next (seek)
    public List<GameObject> flags;

    int targetNumber = 0;

    float lockPos = 0.0f;

    public float maxSpeed = 6.0f;
    public float maxForce = 3.0f;
    public float mass = 1.0f;
    public float radius = 1.0f;

    public float seekWt = 75.0f;

    protected CharacterController characterController;
    protected Vector3 acceleration; //change in velocity per second
    protected Vector3 velocity;     //change in position per second
    protected Vector3 dv;           //desired velocity
    public Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    // Use this for initialization
    void Start()
    {
        target = flags[0];
        acceleration = Vector3.zero;
        velocity = transform.forward;

        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(targetNumber);
        CalcSteeringForce();

        //update velocity
        velocity += acceleration * Time.deltaTime;
        //velocity.z = 1;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        //orient the transform to face where we going
        if (velocity != Vector3.zero)
        {
            transform.forward = velocity.normalized;
        }
        // the CharacterController moves us subject to physical constraints
        characterController.Move(velocity * Time.deltaTime);

		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, lockPos);

        //reset acceleration for next cycle
        acceleration = Vector3.zero;

    }

    protected void CalcSteeringForce()
    {
        Vector3 force = Vector3.zero;

        //seek target
        force += seekWt * Seek(target.transform.position);

        //limit force to maxForce and apply
        force = Vector3.ClampMagnitude(force, maxForce);
        ApplyForce(force);

    }

    protected void ApplyForce(Vector3 steeringForce)
    {
        acceleration += steeringForce / mass;
    }

    protected Vector3 Seek(Vector3 targetPos)
    {
        //find dv, desired velocity
        dv = targetPos - transform.position;
        if (dv.x < 0.01f && dv.y < 0.01f && dv.x * -1.0f < 0.01f && dv.y * -1.0f < 0.01f)
        {
            Debug.Log(flags.Count);
            targetNumber++;
            if(targetNumber > flags.Count - 1)
            {
                targetNumber = 0;
            }
            target = flags[targetNumber];
            dv = target.transform.position - transform.position;
        }
        dv = dv.normalized * maxSpeed;  //scale by maxSpeed
        dv -= velocity;
        //dv.z = 0;                       // only steer in the x/z plane
        return dv;
    }
}
