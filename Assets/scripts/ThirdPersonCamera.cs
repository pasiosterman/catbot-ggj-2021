using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Camera))]
public class ThirdPersonCamera : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    public float cameraSensitity = 1.0f;

    public Transform FollowTransform;
    public float xOffset = 0.0f;
    public float distance = 0.0f;
    public float height = 1.5f;
    public float flipOffsetSpeed = 10.0f;

    public float maxVertocalRotation = 0.0f;

    private Vector3 _targetPosition = Vector3.zero;
    private Quaternion _horizontalCameraRotation = Quaternion.identity;
    private Quaternion _verticalCameraRotation = Quaternion.identity;
    private Vector3 _lookAtPoint = Vector3.zero;


    private float _targetXOffset = 0.0f;
    private float _currentXOffset = 0.0f;

    public void Start()
    {
        Cursor.visible = false;
        _currentXOffset = xOffset;
        _targetXOffset = _currentXOffset;
    }

    private void LateUpdate()
    {
        CameraUpdate();
    }

    void CameraUpdate()
    {
        if (FollowTransform != null)
            ApplyPositionAndRotation();

        if (_currentXOffset != _targetXOffset)
            _currentXOffset = Mathf.Lerp(_currentXOffset, _targetXOffset, Time.deltaTime * flipOffsetSpeed);
    }

    /// <summary>
    /// Rotate camera with given horizontal and vertical values.
    /// Vertical and horizontal rotation seperate values to avoid odd rotation drifting on z-axis.
    /// </summary>
    /// <param name="horizontal">Amount of horizontal rotation to apply (degrees)</param>
    /// <param name="vertical">Amount of vertical rotation to apply (degrees)</param>
    public void RotateCamera(float horizontal, float vertical)
    {
        //Rotate camera on x-axis
        _horizontalCameraRotation = _horizontalCameraRotation * Quaternion.AngleAxis(horizontal, Vector3.up);

        //Get target rotation on vertical axis
        Quaternion targetVerticalRot = _verticalCameraRotation * Quaternion.AngleAxis(vertical, Vector3.left);
        float verticalAngle = Quaternion.Angle(Quaternion.identity, targetVerticalRot);

        //Check if target rotation is within MaxYRot limits. 
        if (verticalAngle < maxVertocalRotation)
        {
            _verticalCameraRotation = targetVerticalRot;
        }
        else if (vertical > 0)
        {
            _verticalCameraRotation = Quaternion.Euler(Vector3.left * maxVertocalRotation);
        }
        else if (vertical < 0)
        {
            _verticalCameraRotation = Quaternion.Euler(Vector3.left * -maxVertocalRotation);
        }
    }

    [ContextMenu("Flip Horisontal offset")]
    public void FlipHorizontalOffset()
    {
        _targetXOffset = -_targetXOffset;
    }

    private void ApplyPositionAndRotation()
    {
        Vector3 followPosition = FollowTransform.position;

        //Combines rotations and uses the result to rotate the offset.
        //Note: height added later to prevent it from messing up the offset during rotation.
        Quaternion finalRot = _horizontalCameraRotation * _verticalCameraRotation;
        Vector3 offset = finalRot * new Vector3(_currentXOffset, 0.0f, -distance);

        //mainly for debugging camera lookat point and distance.
        _lookAtPoint = finalRot * new Vector3(_currentXOffset, 0, 0);
        _lookAtPoint = followPosition + _lookAtPoint + (Vector3.up * height);

        //transform.position = followPosition + offset + (Vector3.up * height);
        //transform.rotation = finalRot;

        transform.position = Vector3.Lerp(transform.position, followPosition + offset + (Vector3.up * height), Time.deltaTime * 6);
        transform.rotation = Quaternion.Slerp(transform.rotation, finalRot, Time.deltaTime * 6.0f);
    }

    public Ray RayFromCenter
    {
        get
        {
            return AttachedCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        }
    }

    public bool RayCast(out RaycastHit hit, LayerMask hitmask)
    {
        if (Physics.Raycast(RayFromCenter, out hit, hitmask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Camera _attachedCamera;
    public Camera AttachedCamera
    {
        get
        {
            if (_attachedCamera == null)
                _attachedCamera = GetComponent<Camera>();

            return _attachedCamera;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_lookAtPoint, 0.05f);
    }

}