using UnityEngine;
using UnityEngine.InputSystem;

public class ExampleControllerInput : MonoBehaviour
{
    private XRInputActions inputActions;

    private void OnEnable()
    {
        // 1. Instantiate the auto-generated actions
        inputActions = new XRInputActions();

        // 2. Enable the specific action map
        inputActions.CustomRight.Enable();
        inputActions.CustomLeft.Enable(); // If you also want to enable the left controller

        // 3. Subscribe to events from the right hand controller
        inputActions.CustomRight.Trigger.performed += OnRightTriggerPressed;
        inputActions.CustomRight.Trigger.canceled += OnRightTriggerReleased;

        inputActions.CustomRight.Grip.performed += OnRightGripPressed;
        inputActions.CustomRight.Grip.canceled += OnRightGripReleased;

        inputActions.CustomRight.Primary.performed += OnRightPrimaryButtonPressed;
        inputActions.CustomRight.Primary.canceled += OnRightPrimaryButtonReleased;

        inputActions.CustomRight.Secondary.performed += OnRightSecondaryButtonPressed;
        inputActions.CustomRight.Secondary.canceled += OnRightSecondaryButtonReleased;

        inputActions.CustomRight.Thumbstick.performed += OnRightThumbstickMoved;
        inputActions.CustomRight.Thumbstick.canceled += OnRightThumbstickReleased;

        // 4. Subscribe to events from the left hand controller (if needed)
        inputActions.CustomLeft.Trigger.performed += OnLeftTriggerPressed; // If you also want to handle the left controller trigger
        inputActions.CustomLeft.Trigger.canceled += OnLeftTriggerReleased; // If you also want to handle the left controller trigger

        inputActions.CustomLeft.Grip.performed += OnLeftGripPressed;
        inputActions.CustomLeft.Grip.canceled += OnLeftGripReleased;

        inputActions.CustomLeft.Primary.performed += OnLeftPrimaryButtonPressed;
        inputActions.CustomLeft.Primary.canceled += OnLeftPrimaryButtonReleased;

        inputActions.CustomLeft.Secondary.performed += OnLeftSecondaryButtonPressed;
        inputActions.CustomLeft.Secondary.canceled += OnLeftSecondaryButtonReleased;

        inputActions.CustomLeft.Thumbstick.performed += OnLeftThumbstickMoved;
        inputActions.CustomLeft.Thumbstick.canceled += OnLeftThumbstickReleased;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        inputActions.CustomRight.Trigger.performed -= OnRightTriggerPressed;
        inputActions.CustomRight.Trigger.canceled -= OnRightTriggerReleased;

        inputActions.CustomLeft.Trigger.performed -= OnLeftTriggerPressed;
        inputActions.CustomLeft.Trigger.canceled -= OnLeftTriggerReleased;

        inputActions.CustomRight.Grip.performed -= OnRightGripPressed;
        inputActions.CustomRight.Grip.canceled -= OnRightGripReleased;

        inputActions.CustomRight.Primary.performed -= OnRightPrimaryButtonPressed;
        inputActions.CustomRight.Primary.canceled -= OnRightPrimaryButtonReleased;

        inputActions.CustomRight.Secondary.performed -= OnRightSecondaryButtonPressed;
        inputActions.CustomRight.Secondary.canceled -= OnRightSecondaryButtonReleased;

        inputActions.CustomRight.Thumbstick.performed -= OnRightThumbstickMoved;
        inputActions.CustomRight.Thumbstick.canceled -= OnRightThumbstickReleased;

        //left
        inputActions.CustomLeft.Trigger.performed -= OnLeftTriggerPressed; // If you also want to handle the left controller trigger
        inputActions.CustomLeft.Trigger.canceled -= OnLeftTriggerReleased; // If you also want to handle the left controller trigger

        inputActions.CustomLeft.Grip.performed -= OnLeftGripPressed;
        inputActions.CustomLeft.Grip.canceled -= OnLeftGripReleased;

        inputActions.CustomLeft.Primary.performed -= OnLeftPrimaryButtonPressed;
        inputActions.CustomLeft.Primary.canceled -= OnLeftPrimaryButtonReleased;

        inputActions.CustomLeft.Secondary.performed -= OnLeftSecondaryButtonPressed;
        inputActions.CustomLeft.Secondary.canceled -= OnLeftSecondaryButtonReleased;

        inputActions.CustomLeft.Thumbstick.performed -= OnLeftThumbstickMoved;
        inputActions.CustomLeft.Thumbstick.canceled -= OnLeftThumbstickReleased;

        // Disable input actions
        inputActions.CustomRight.Disable();
        inputActions.CustomLeft.Disable(); // If you also want to disable the left controller
    }

    // Callback Methods
    private void OnRightTriggerPressed(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Trigger Pressed");
    }

    private void OnRightTriggerReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Trigger Released");
    }

    private void OnLeftTriggerPressed(InputAction.CallbackContext context)
    {
        Debug.Log("CustomLeft Trigger Pressed");
    }

    private void OnLeftTriggerReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomLeft Trigger Released");
    }

    private void OnRightGripPressed(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Grip Pressed");
    }

    private void OnRightGripReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Grip Released");
    }

    private void OnLeftGripPressed(InputAction.CallbackContext context)
    {
        Debug.Log("CustomLeft Grip Pressed");
    }

    private void OnLeftGripReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomLefr Grip Released");
    }


    private void OnRightPrimaryButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Primary Button Pressed");
    }

    private void OnRightPrimaryButtonReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Primary Button Released");
    }
    private void OnLeftPrimaryButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("CustomLeft Primary Button Pressed");
    }

    private void OnLeftPrimaryButtonReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomLeft Primary Button Released");
    }


    private void OnRightSecondaryButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Secondary Button Pressed");
    }

    private void OnRightSecondaryButtonReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Secondary Button Released");
    }
    private void OnLeftSecondaryButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("CustomLeft Secondary Button Pressed");
    }

    private void OnLeftSecondaryButtonReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomLeft Secondary Button Released");
    }
    private void OnRightThumbstickMoved(InputAction.CallbackContext context)
    {
        Vector2 thumbstickValue = context.ReadValue<Vector2>();
        Debug.Log("CustomRight Thumbstick Moved: " + thumbstickValue);
    }

    private void OnLeftThumbstickMoved(InputAction.CallbackContext context)
    {
        Vector2 thumbstickValue = context.ReadValue<Vector2>();
        Debug.Log("CustomLeft Thumbstick Moved: " + thumbstickValue);
    }
    private void OnRightThumbstickReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomRight Thumbstick Released");
    }
    private void OnLeftThumbstickReleased(InputAction.CallbackContext context)
    {
        Debug.Log("CustomLeft Thumbstick Released");
    }
}

