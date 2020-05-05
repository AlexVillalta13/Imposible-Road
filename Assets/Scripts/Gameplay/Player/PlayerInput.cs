﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public float GetTouch()
    {
        float rotationInput;
        bool screenIsPressed = Touchscreen.current.primaryTouch.press.isPressed;
        if (screenIsPressed)
        {
            float pointerScreenPosition = Touchscreen.current.primaryTouch.position.x.ReadValue();
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