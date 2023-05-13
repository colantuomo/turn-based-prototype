using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventsManager.Instance.Click();
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventsManager.Instance.Cancel();
        }
    }

}
