using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;

    //TODO/BUG WheelsComponents header is showing up twice in the inspector 
    [Header("WheelsComponents")]   
    [SerializeField] WheelCollider BottomRightW, BottomLeftW;
    [SerializeField] WheelCollider TopRightW, TopLeftW;
    [SerializeField] Transform TopRightT, TopLeftT;
    [SerializeField] Transform BottomRightT, BottomLeftT;

    [Header("Steering and Acceleration")]
    [SerializeField] float maxSteerAngle = 30;
    [SerializeField] float steerAngleDuringBraking = 60f;
    
    [SerializeField] float motorForce = 50;
    [SerializeField] float topSpeed = 30;
    private float currentSteeringAngle = 0f;

    [Header("Braking")]
    [SerializeField] float braking = 300f;
    [SerializeField] float sidewayBrakingStiffness = 0.5f;
    [SerializeField] float sidewayNormalStiffness = 2f;
    private WheelFrictionCurve bR;
    private WheelFrictionCurve bL;
    private float brakes = 0f;


    CheckpointSystem checkPointSystem;


    private void Start()
    {
        bR = BottomRightW.sidewaysFriction;
        bL = BottomLeftW.sidewaysFriction;
        currentSteeringAngle = maxSteerAngle;
        checkPointSystem = FindObjectOfType<CheckpointSystem>();
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Brake();
        Accelerate();
        UpdateWheelPoses();
        //Debug.Log(GetComponent<Rigidbody>().velocity.magnitude); 
    }

    #region driving
    private void Brake()
    {
        
        if (Input.GetKey(KeyCode.Space) == true)
        {
            currentSteeringAngle = steerAngleDuringBraking;
            brakes = braking;
            StiffnessModifier(sidewayBrakingStiffness);           
        }
        //if (Input.GetKeyUp(KeyCode.Space) == true)
        else
        {
            StiffnessModifier(sidewayNormalStiffness);
            currentSteeringAngle = maxSteerAngle;
            brakes = 0;
        }
        //TopRightW.brakeTorque = brakes;
        //TopLeftW.brakeTorque = brakes;
        BottomRightW.brakeTorque = brakes;
        BottomLeftW.brakeTorque = brakes;
    }

    private void StiffnessModifier(float stiffness)
    {
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
    #endregion

    #region CheckpointSystem
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckPoint")
        {
            checkPointSystem.hitCheckPoint();
            other.gameObject.SetActive(false);
            Debug.Log("A checkpoint");
        }

        /*
        if (other.tag == "StartCheckPoint")
        {
            Debug.Log("start");
        }
        else if (other.tag == "EndCheckPoint")
        {
            Debug.Log("End");
        }
        else if (other.tag == "CheckPoint")
        {
            Debug.Log("A checkpoint");
        }
        */
    }
    #endregion
}
