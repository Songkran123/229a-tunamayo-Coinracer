using UnityEngine;

public class CarPhysicsController : MonoBehaviour
{
    public float motorForce = 1500f;
    public float steerAngle = 30f;
    public float brakeForce = 3000f;
    public float airDragCoefficient = 0.3f;
    public float carMass = 1000f;

    public Rigidbody rb;

    public WheelCollider frontLeft, frontRight, rearLeft, rearRight;
    public Transform flTransform, frTransform, rlTransform, rrTransform;

    private float vertical, horizontal;
    private bool braking;

    void Start()
    {
        // ✅ ตั้งค่า Rigidbody
        rb.mass = carMass;
        rb.drag = 0.05f;
        rb.angularDrag = 0.1f;
        rb.centerOfMass = new Vector3(0, -0.5f, 0);

        // ✅ ปรับค่าความฝืดของล้อเพื่อลดการดริฟ
        SetupWheelFriction(frontLeft);
        SetupWheelFriction(frontRight);
        SetupWheelFriction(rearLeft);
        SetupWheelFriction(rearRight);
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        braking = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        ApplyMotor();
        ApplySteering();
        ApplyBraking();
        ApplyAirResistance();
        UpdateWheels();

        // ✅ หยุดรถสนิทเมื่อเบรกและความเร็วต่ำ
        if (braking && rb.velocity.magnitude < 0.5f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void ApplyMotor()
    {
        if (braking)
        {
            rearLeft.motorTorque = 0f;
            rearRight.motorTorque = 0f;
        }
        else
        {
            float acceleration = vertical * (motorForce / carMass);
            float motorTorque = acceleration * carMass * 0.5f;

            rearLeft.motorTorque = motorTorque;
            rearRight.motorTorque = motorTorque;
        }
    }

    void ApplySteering()
    {
        float steer = horizontal * steerAngle;
        frontLeft.steerAngle = steer;
        frontRight.steerAngle = steer;
    }

    void ApplyBraking()
    {
        float brake = braking ? brakeForce : 0f;

        frontLeft.brakeTorque = brake;
        frontRight.brakeTorque = brake;
        rearLeft.brakeTorque = brake;
        rearRight.brakeTorque = brake;
    }

    void ApplyAirResistance()
    {
        Vector3 velocity = rb.velocity;
        Vector3 airResistance = -velocity.normalized * airDragCoefficient * velocity.sqrMagnitude;
        rb.AddForce(airResistance);
    }

    void UpdateWheels()
    {
        UpdateWheel(frontLeft, flTransform);
        UpdateWheel(frontRight, frTransform);
        UpdateWheel(rearLeft, rlTransform);
        UpdateWheel(rearRight, rrTransform);
    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        col.GetWorldPose(out Vector3 pos, out Quaternion rot);
        trans.position = pos;
        trans.rotation = rot;
    }

    // ✅ ปรับความฝืดของล้อเพื่อป้องกันดริฟ
    void SetupWheelFriction(WheelCollider wheel)
    {
        WheelFrictionCurve forwardFriction = wheel.forwardFriction;
        forwardFriction.stiffness = 2.5f;
        wheel.forwardFriction = forwardFriction;

        WheelFrictionCurve sidewaysFriction = wheel.sidewaysFriction;
        sidewaysFriction.stiffness = 3f;
        wheel.sidewaysFriction = sidewaysFriction;
    }
}
