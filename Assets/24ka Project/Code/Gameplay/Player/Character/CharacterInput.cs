using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    private InputControl _inputControl;
    private bool _isFocused = false;

    public event Action<Vector2> MovementVectorChanged;
    public event Action<Vector2> LookVectorChanged;
    public event Action<bool> FocusChanged;
    public event Action Jumped;
    public event Action ActivatedDefence;

    private void Awake()
    {
        _inputControl = new InputControl();    
    }

    private void OnEnable()
    {
        _inputControl.Enable();
        _inputControl.Character.Movement.performed += ChangeMovementVector;
        _inputControl.Character.Movement.canceled += ChangeMovementVector;

        _inputControl.Character.Look.performed += ChangeLookVector;
        _inputControl.Character.Look.canceled += ChangeLookVector;

        _inputControl.Character.Focus.started += ChangeFocus;
        _inputControl.Character.Focus.canceled += ChangeFocus;

        _inputControl.Character.Jump.started += Jump;
        _inputControl.Character.Defense.started += ActivateDefense;
    }

    private void OnDisable()
    {
        _inputControl.Disable();
        _inputControl.Character.Movement.performed -= ChangeMovementVector;
        _inputControl.Character.Movement.canceled -= ChangeMovementVector;

        _inputControl.Character.Look.performed -= ChangeLookVector;
        _inputControl.Character.Look.canceled -= ChangeLookVector;

        _inputControl.Character.Focus.started -= ChangeFocus;
        _inputControl.Character.Focus.canceled -= ChangeFocus;

        _inputControl.Character.Jump.started -= Jump;
        _inputControl.Character.Defense.started -= ActivateDefense;
    }

    private void ChangeMovementVector(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        MovementVectorChanged(value);
    }

    private void ChangeLookVector(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        LookVectorChanged(value);
    }

    private void ChangeFocus(InputAction.CallbackContext context)
    {
        _isFocused = context.ReadValueAsButton();
        FocusChanged(_isFocused);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        Jumped.Invoke();
    }

    private void ActivateDefense(InputAction.CallbackContext context)
    {
        ActivatedDefence.Invoke();
    }

   /* private void StartStopDashOrBlock(InputAction.CallbackContext context)
    {
        if(_isFocused == false)
            DashOrBlock.Invoke();
    }

    private void CancelDashOrBlock(InputAction.CallbackContext context)
    {
        if (_isFocused && _isLongBlockPerfomed)
        {
            _isLongBlockPerfomed = false;
            LongBlock.Invoke(false);
        }
        else if(_isFocused && _isLongBlockPerfomed == false)
        {
            DashOrBlock.Invoke();
        }
    }
         
    private void PerformDashOrBlock(InputAction.CallbackContext context)
    {
        _isLongBlockPerfomed = true;

        if(_isFocused)
            LongBlock.Invoke(true);
    }*/
}
