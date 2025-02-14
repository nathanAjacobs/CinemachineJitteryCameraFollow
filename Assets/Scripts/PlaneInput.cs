
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaneInput : MonoBehaviour
{
    PlaneInputActions inputActions;

    public event Action<Vector2> RollPitchChanged;

    private Vector2 _rollPitch;

    public Vector2 RollPitch
    {
        get => _rollPitch;
        private set
        {
            _rollPitch = value;
            RollPitchChanged?.Invoke(value);
        }
    }

    public event Action<float> YawChanged;

    private float _yaw;

    public float Yaw
    {
        get => _yaw;
        private set
        {
            _yaw = value;
            YawChanged?.Invoke(value);
        }
    }

    public event Action<float> ThrottleChanged;
    private float _throttle;

    public float Throttle
    {
        get => _throttle;
        private set
        {
            _throttle = value;
            ThrottleChanged?.Invoke(value);
        }
    }

    private void Awake()
    {
        inputActions = new PlaneInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Default.RollPitch.performed += OnRollPitch;
        inputActions.Default.Yaw.performed += OnYawPerformed;
        inputActions.Default.Throttle.performed += OnThrottleChanged;
    }

    private void OnDisable()
    {
        inputActions.Disable();

        inputActions.Default.RollPitch.performed -= OnRollPitch;
        inputActions.Default.Yaw.performed -= OnYawPerformed;
        inputActions.Default.Throttle.performed -= OnThrottleChanged;
    }

    private void OnRollPitch(InputAction.CallbackContext context)
    {
        RollPitch = context.ReadValue<Vector2>();
    }

    private void OnYawPerformed(InputAction.CallbackContext context)
    {
        Yaw = context.ReadValue<float>();
    }

    private void OnThrottleChanged(InputAction.CallbackContext context)
    {
        Throttle = context.ReadValue<float>();
    }
}