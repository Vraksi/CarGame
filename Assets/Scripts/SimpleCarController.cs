using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;
    [Header("WheelsComponents")]
    [SerializeField] WheelCollider TopRightW, TopLeftW;
    [SerializeField] WheelCollider BottomRightW, BottomLeftW;
    [SerializeField] Transform TopRightT, TopLeftT;
    [SerializeField] Transform BottomRightT, BottomLeftT;
    
    [Header("Steering and Acceleration")]
    [SerializeField] float maxSteerAngle = 30;
    [SerializeField] float steerAngleDuringBraking = 60f;
    private float currentSteeringAngle = 0f;
    [SerializeField] float motorForce = 50;
    [SerializeField] float topSpeed = 30;

    [Header("Braking")]
    [SerializeField] float braking = 300f;
    [SerializeField] float sidewayBrakingStiffness = 0.5f;
    [SerializeField] float sidewayNormalStiffness = 2f;
    private WheelFrictionCurve bR;
    private WheelFrictionCurve bL;
    private float brakes = 0f;


    private void Start()
    {
        bR = BottomRightW.sidewaysFriction;
        Debug.Log(bR.asymptoteSlip);
        bL = BottomLeftW.sidewaysFriction;
        currentSteeringAngle = maxSteerAngle;
    }

    private void Brake()
    {
        if (Input.GetKeyUp(KeyCode.Space) == true)
        {
            StiffnessModifier(sidewayNormalStiffness);
            currentSteeringAngle = maxSteerAngle;
            brakes = 0;
        }
        if (Input.GetKey(KeyCode.Space) == true)
        {
            currentSteeringAngle = steerAngleDuringBraking;
            brakes = braking;
            StiffnessModifier(sidewayBrakingStiffness);           
        }        
        //TopRightW.brakeTorque = brakes;
        //TopLeftW.brakeTorque = brakes;
        BottomRightW.brakeTorque = brakes;
        BottomLeftW.brakeTorque = brakes;
    }

    private void StiffnessModifier(float stiffness)
    {
        //bR = BottomRightW.sidewaysFriction;
        //bL = BottomLeftW.sidewaysFriction;
        bR.stiffness = stiffness;
        bL.stiffness = stiffness;
        BottomRightW.sidewaysFriction = bR;
        BottomLeftW.sidewaysFriction = bL;
    }

    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
    }

    public void Steer()
    {
        m_steeringAngle = currentSteeringAngle * m_horizontalInput;
        TopLeftW.steerAngle = m_steeringAngle;
        TopRightW.steerAngle = m_steeringAngle;

    }

    public void Accelerate()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > topSpeed)
        {            
            TopLeftW.motorTorque = 0;
            TopRightW.motorTorque = 0;
        }
        else
        {
            TopLeftW.motorTorque = m_verticalInput * motorForce;
            TopRightW.motorTorque = m_verticalInput * motorForce;            
        }
             
    }

    public void UpdateWheelPoses()
    {
        UpdateWheelPose(TopLeftW, TopLeftT);
        UpdateWheelPose(TopRightW, TopRightT);
        UpdateWheelPose(BottomLeftW, BottomLeftT);
        UpdateWheelPose(BottomRightW, BottomRightT);
    }

    public void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Brake();
        Accelerate();
        UpdateWheelPoses();
        
        Debug.Log(GetComponent<Rigidbody>().velocity.magnitude); 
    }

}
