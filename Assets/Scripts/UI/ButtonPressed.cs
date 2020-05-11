using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPressed : MonoBehaviour
{
    [SerializeField] RectTransform giftImageRectTransform = null;

    public void MoveDownImageWhenPressed()
    {
        giftImageRectTransform.sizeDelta = new Vector2(200, 180);
    }

    public void MoveUpImageWhenPressed()
    {
        giftImageRectTransform.sizeDelta = new Vector2(200, 200);
    }
}
