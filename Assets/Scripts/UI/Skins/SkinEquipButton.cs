using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinEquipButton : MonoBehaviour
{
    [SerializeField] GameObject notOwnedSkinButton = null;
    [SerializeField] GameObject unequippedSkinButton = null;
    [SerializeField] GameObject equippedSkinButton = null;


    public void UpdateStatus(SkinButtonStatus status)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        switch (status)
        {
            case SkinButtonStatus.Equipped:
                equippedSkinButton.SetActive(true);
                break;
            case SkinButtonStatus.Unequiped:
                unequippedSkinButton.SetActive(true);
                break;
            case SkinButtonStatus.NotOwned:
                notOwnedSkinButton.SetActive(true);
                break;
        }
    }
}
