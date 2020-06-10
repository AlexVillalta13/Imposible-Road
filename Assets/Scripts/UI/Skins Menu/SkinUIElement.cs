using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkinButtonStatus { NotOwned, Unequiped, Equipped }

public class SkinUIElement : MonoBehaviour
{
    public string skinID;
    public string skinName;

    public SkinButtonStatus buttonStatus;

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
            buttonStatus = SkinButtonStatus.Equipped;
            //return;
        } else if(skinStatus.owned == true && skinStatus.equiped == false)
        {
            buttonStatus = SkinButtonStatus.Unequiped;
            //return;
        } else if (skinStatus.owned == false)
        {
            buttonStatus = SkinButtonStatus.NotOwned;
            //return;
        }
        SkinEquipButton.UpdateStatus(buttonStatus);
    }
}


