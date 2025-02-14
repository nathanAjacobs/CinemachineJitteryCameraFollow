using UnityEngine;

public class PlaneController : MonoBehaviour
{
    Rigidbody _rigidBody;
    PlaneInput _planeInput;

    public float MaxThrust = 1000f;
    public float ThrottleAccelarationRate = 1;

    public float LiftFactor = 1f;

    public float RollFactor = 1;
    public float PitchFactor = 1;
    public float YawFactor = 1;

    private float _currentThrottle;

    public Vector3 Velocity => _rigidBody.linearVelocity;
    public float Throttle => _currentThrottle;

    public Vector3 Position => _rigidBody.position;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _planeInput = GetComponent<PlaneInput>();
    }

    private void Update()
    {
        _currentThrottle += _planeInput.Throttle * ThrottleAccelarationRate * Time.deltaTime;
        _currentThrottle = Mathf.Clamp01(_currentThrottle);
    }

    private void FixedUpdate()
    {
        Vector3 up = _rigidBody.rotation * Vector3.up;
        Vector3 forward = _rigidBody.rotation * Vector3.forward;
        Vector3 right = _rigidBody.rotation * Vector3.right;

        _rigidBody.AddTorque(-_planeInput.RollPitch.x * _rigidBody.mass * RollFactor * forward, ForceMode.Force);
        _rigidBody.AddTorque(_planeInput.RollPitch.y * _rigidBody.mass * PitchFactor * right, ForceMode.Force);
        _rigidBody.AddTorque(_planeInput.Yaw * _rigidBody.mass * YawFactor * up, ForceMode.Force);

        _rigidBody.AddForce(_currentThrottle * MaxThrust * forward, ForceMode.Force);

        _rigidBody.AddForce(_rigidBody.linearVelocity.magnitude * LiftFactor * up, ForceMode.Force);
    }
}
