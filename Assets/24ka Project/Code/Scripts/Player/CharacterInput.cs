using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    private InputControl _inputControl;

    public event Action<Vector2> OnMovementVectorChanged;

    private void Awake()
    {
        _inputControl = new InputControl();    
    }

    private void OnEnable()
    {
        _inputControl.Enable();
        _inputControl.Character.Movement.performed += ChangeMovementVector;
        _inputControl.Character.Movement.canceled += ChangeMovementVector;
    }

    private void OnDisable()
    {
        _inputControl.Disable();
        _inputControl.Character.Movement.performed -= ChangeMovementVector;
        _inputControl.Character.Movement.canceled -= ChangeMovementVector;
    }

    private void ChangeMovementVector(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        OnMovementVectorChanged(value);
    }
}
