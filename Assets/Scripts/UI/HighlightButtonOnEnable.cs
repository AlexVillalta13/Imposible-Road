using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightButtonOnEnable : MonoBehaviour
{
    ButtonHighlightAnimation buttonHighlightAnimation;

    private void Awake()
    {
        buttonHighlightAnimation = GetComponent<ButtonHighlightAnimation>();
    }

    private void OnEnable()
    {
        buttonHighlightAnimation.StartHighlightAnimation();
    }
}
