using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;

    public WheelCollider TopRightW, TopLeftW;
    public WheelCollider BottomRightW, BottomLeftW;
    public Transform TopRightT, TopLeftT;
    public Transform BottomRightT, BottomLeftT;
    public float maxSteerAngle = 30;
    public float motorForce = 50;

    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
    }
    public void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        TopLeftW.steerAngle = m_steeringAngle;
        TopRightW.steerAngle = m_steeringAngle;

    }
    public void Accelerate()
    {
        TopLeftW.motorTorque = m_verticalInput * motorForce;
        TopRightW.motorTorque = m_verticalInput * motorForce;
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
        Accelerate();
        UpdateWheelPoses();
    }

}
