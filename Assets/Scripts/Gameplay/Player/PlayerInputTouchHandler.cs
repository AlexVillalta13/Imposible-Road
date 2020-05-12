using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputTouchHandler : MonoBehaviour
{
    public float GetTouch()
    {
        float rotationInput;
        bool screenIsPressed = Touchscreen.current.press.isPressed;
        if (screenIsPressed && EventSystem.current.IsPointerOverGameObject() == false)
        {
            float pointerScreenPosition = Touchscreen.current.position.x.ReadValue();
            if (pointerScreenPosition > Screen.width / 2)
            {
                rotationInput = 1;
            }
            else
            {
                rotationInput = -1;
            }
            return rotationInput;
        }
        rotationInput = 0;

        return rotationInput;
    }
}
