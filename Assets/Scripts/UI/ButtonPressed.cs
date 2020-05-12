using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPressed : MonoBehaviour
{
    [SerializeField] RectTransform giftImageRectTransform = null;
    float height;
    float tenPercentageToAdd;

    private void Awake()
    {
        height = giftImageRectTransform.rect.height;
        tenPercentageToAdd = height * 10f / 100f;
    }

    public void MoveDownImageWhenPressed()
    {
        giftImageRectTransform.sizeDelta = new Vector2(200, height - tenPercentageToAdd);
    }

    public void MoveUpImageWhenPressed()
    {
        giftImageRectTransform.sizeDelta = new Vector2(200, height);
    }
}
