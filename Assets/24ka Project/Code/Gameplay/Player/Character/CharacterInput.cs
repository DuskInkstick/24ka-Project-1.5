using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    private InputControl _inputControl;

    public event Action<Vector2> MovementVectorChanged;
    public event Action<Vector2> ViewVectorChanged;
    public event Action<bool> FocusChanged;

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
        var value = context.ReadValueAsButton();
        FocusChanged(value);
    }
}
