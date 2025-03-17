using UnityEngine;
using UnityEngine.InputSystem;

public class XRInteractionInput : MonoBehaviour
{
    private XRIDefaultInputActions inputActions;

    private void OnEnable()
    {
        // 1. Instantiate the auto-generated actions
        inputActions = new XRIDefaultInputActions();

        // 2. Enable the specific action map
        inputActions.CustomRight.Enable();

        // 3. Subscribe to events from the right hand controller
        inputActions.CustomRight.Trigger.performed += OnTriggerPressed;
        inputActions.CustomRight.Trigger.canceled += OnTriggerReleased;

        inputActions.CustomRight.Grip.performed += OnGripPressed;
        inputActions.CustomRight.Grip.canceled += OnGripReleased;

        inputActions.CustomRight.Primary.performed += OnPrimaryButtonPressed;
        inputActions.CustomRight.Primary.canceled += OnPrimaryButtonReleased;

        inputActions.CustomRight.Secondary.performed += OnSecondaryButtonPressed;
        inputActions.CustomRight.Secondary.canceled += OnSecondaryButtonReleased;

        inputActions.CustomRight.Thumbstick.performed += OnThumbstickMoved;
        inputActions.CustomRight.Thumbstick.canceled += OnThumbstickReleased;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        inputActions.CustomRight.Trigger.performed -= OnTriggerPressed;
        inputActions.CustomRight.Trigger.canceled -= OnTriggerReleased;

        inputActions.CustomRight.Grip.performed -= OnGripPressed;
        inputActions.CustomRight.Grip.canceled -= OnGripReleased;

        inputActions.CustomRight.Primary.performed -= OnPrimaryButtonPressed;
        inputActions.CustomRight.Primary.canceled -= OnPrimaryButtonReleased;

        inputActions.CustomRight.Secondary.performed -= OnSecondaryButtonPressed;
        inputActions.CustomRight.Secondary.canceled -= OnSecondaryButtonReleased;

        inputActions.CustomRight.Thumbstick.performed -= OnThumbstickMoved;
        inputActions.CustomRight.Thumbstick.canceled -= OnThumbstickReleased;

        // Disable input actions
        inputActions.CustomRight.Disable();
    }

    // Callback Methods
    private void OnTriggerPressed(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Trigger Pressed");
    }

    private void OnTriggerReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Trigger Released");
    }

    private void OnGripPressed(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Grip Pressed");
    }

    private void OnGripReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Grip Released");
    }

    private void OnPrimaryButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Primary Button Pressed");
    }

    private void OnPrimaryButtonReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Primary Button Released");
    }

    private void OnSecondaryButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Secondary Button Pressed");
    }

    private void OnSecondaryButtonReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Secondary Button Released");
    }

    private void OnThumbstickMoved(InputAction.CallbackContext context)
    {
        Vector2 thumbstickValue = context.ReadValue<Vector2>();
        Debug.Log("CustomRight Thumbstick Moved: " + thumbstickValue);
    }

    private void OnThumbstickReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Thumbstick Released");
    }
}
