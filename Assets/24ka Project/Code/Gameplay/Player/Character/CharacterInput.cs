using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    private InputControl _inputControl;
    private bool _isFocused = false;
    private bool _isLongBlockPerfomed = false;

    public event Action<Vector2> MovementVectorChanged;
    public event Action<Vector2> ViewVectorChanged;
    public event Action<bool> FocusChanged;
    public event Action DashOrBlock;
    public event Action<bool> LongBlock;

    private void Awake()
    {
        _inputControl = new InputControl();    
    }

    private void OnEnable()
    {
        _inputControl.Enable();
        _inputControl.Character.Movement.performed += ChangeMovementVector;
        _inputControl.Character.Movement.canceled += ChangeMovementVector;

        _inputControl.Character.View.performed += ChangeViewVector;
        _inputControl.Character.View.canceled += ChangeViewVector;

        _inputControl.Character.Focus.started += ChangeFocus;
        _inputControl.Character.Focus.canceled += ChangeFocus;

        _inputControl.Character.DashAndBlock.started += StartStopDashOrBlock;
        _inputControl.Character.DashAndBlock.performed += PerformDashOrBlock;
        _inputControl.Character.DashAndBlock.canceled += CancelDashOrBlock;

    }

    private void OnDisable()
    {
        _inputControl.Disable();
        _inputControl.Character.Movement.performed -= ChangeMovementVector;
        _inputControl.Character.Movement.canceled -= ChangeMovementVector;

        _inputControl.Character.View.performed -= ChangeViewVector;
        _inputControl.Character.View.canceled -= ChangeViewVector;

        _inputControl.Character.Focus.started -= ChangeFocus;
        _inputControl.Character.Focus.canceled -= ChangeFocus;

        _inputControl.Character.DashAndBlock.started -= StartStopDashOrBlock;
        _inputControl.Character.DashAndBlock.performed -= PerformDashOrBlock;
        _inputControl.Character.DashAndBlock.canceled -= CancelDashOrBlock;
    }

    private void ChangeMovementVector(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        MovementVectorChanged(value);
    }

    private void ChangeViewVector(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        ViewVectorChanged(value);
    }

    private void ChangeFocus(InputAction.CallbackContext context)
    {
        _isFocused = context.ReadValueAsButton();
        FocusChanged(_isFocused);
    }

    private void StartStopDashOrBlock(InputAction.CallbackContext context)
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
    }
}
