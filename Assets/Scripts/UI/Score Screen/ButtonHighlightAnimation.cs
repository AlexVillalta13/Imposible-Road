using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlightAnimation : MonoBehaviour
{
    [SerializeField] float changeSpriteTime = 0.4f;
    [SerializeField] Sprite orangeButtonSprite = null;

    Image image;
    Sprite baseSprite;

    private void Awake()
    {
        image = GetComponent<Image>();
        baseSprite = image.sprite;
    }

    public void StartHighlightAnimation()
    {
        StartCoroutine(HighlightAnimation());
    }

    private IEnumerator HighlightAnimation()
    {
        WaitForSeconds timeToChangeSprite = new WaitForSeconds(changeSpriteTime);
        while(true)
        {
            yield return timeToChangeSprite;
            image.sprite = orangeButtonSprite;

            yield return timeToChangeSprite;
            image.sprite = baseSprite;
        }
    }
}
