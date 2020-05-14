﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScreenHandler : MonoBehaviour
{
    public void Enable(bool state)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(state);
        }
    }
}