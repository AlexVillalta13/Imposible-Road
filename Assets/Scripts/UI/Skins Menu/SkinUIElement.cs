using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinUIElement : MonoBehaviour
{
    public string skinID;

    [SerializeField] Image skinPreviewImage;
    [SerializeField] SkinEquipButton SkinEquipButton;

    public void SetUISkin(Sprite skinPreviewSprite)
    {
        skinPreviewImage.sprite = skinPreviewSprite;
    }

    public void UpdateStatus(SkinStatus skinStatus)
    {
        if(skinStatus.equiped == true)
        {
            SkinEquipButton.UpdateStatus(SkinEquipButton.Status.Equipped);
            return;
        } else if(skinStatus.owned == true && skinStatus.equiped == false)
        {
            SkinEquipButton.UpdateStatus(SkinEquipButton.Status.Unequiped);
            return;
        } else if (skinStatus.owned == false)
        {
            SkinEquipButton.UpdateStatus(SkinEquipButton.Status.NotOwned);
            return;
        }
    }
}
