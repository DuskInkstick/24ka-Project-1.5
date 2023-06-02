using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    private InputControl _inputControl;

    public event Action<Vector2> OnMovementVectorChanged;
    public event Action<bool> OnFocusChanged;

    private void Awake()
    {
        _inputControl = new InputControl();    
    }

    private void OnEnable()
    {
        _inputControl.Enable();
        _inputControl.Character.Movement.performed += ChangeMovementVector;
        _inputControl.Character.Movement.canceled += ChangeMovementVector;

        _inputControl.Character.Focus.started += ChangeFocus;
        _inputControl.Character.Focus.canceled += ChangeFocus;
    }

    private void OnDisable()
    {
        _inputControl.Disable();
        _inputControl.Character.Movement.performed -= ChangeMovementVector;
        _inputControl.Character.Movement.canceled -= ChangeMovementVector;

        _inputControl.Character.Focus.started -= ChangeFocus;
        _inputControl.Character.Focus.canceled -= ChangeFocus;
    }

    private void ChangeMovementVector(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        OnMovementVectorChanged(value);
    }

    private void ChangeFocus(InputAction.CallbackContext context)
    {
        var value = context.ReadValueAsButton();
        OnFocusChanged(value);
    }
}
