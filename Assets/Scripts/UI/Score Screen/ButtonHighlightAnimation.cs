using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlightAnimation : MonoBehaviour
{
    [SerializeField] float changeSpriteTime = 0.4f;
    [SerializeField] Sprite orangeButtonSprite = null;
    bool activeAnimation = false;

    Image image;
    Sprite baseSprite;
    Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        baseSprite = image.sprite;
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        activeAnimation = false;
        StartHighlightAnimation();
    }

    public void StartHighlightAnimation()
    {
        if (button.interactable == true && activeAnimation == false) 
        {
            activeAnimation = true;
            StartCoroutine(HighlightAnimation());
        }
    }

    private IEnumerator HighlightAnimation()
    {
        WaitForSeconds timeToChangeSprite = new WaitForSeconds(changeSpriteTime);
        while(button.interactable == true)
        {
            yield return timeToChangeSprite;
            image.sprite = orangeButtonSprite;

            yield return timeToChangeSprite;
            image.sprite = baseSprite;
        }
        activeAnimation = false;
    }
}
