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

    [SerializeField] Image skinPreviewImage = null;
    [SerializeField] SkinEquipButton SkinEquipButton = null;

    SkinSystem skinSystem;

    public void Init(string skinID, string skinName, Sprite skinPreviewSprite, SkinSystem skinSystem)
    {
        this.skinID = skinID;
        this.skinName = skinName;
        skinPreviewImage.sprite = skinPreviewSprite;
        this.skinSystem = skinSystem;
    }

    public void SetUISkin(Sprite skinPreviewSprite)
    {
        skinPreviewImage.sprite = skinPreviewSprite;
    }

    public void UpdateStatus(SkinStatus skinStatus)
    {
        if(skinStatus.equiped == true)
        {
            buttonStatus = SkinButtonStatus.Equipped;
        } 
        else if(skinStatus.owned == true && skinStatus.equiped == false)
        {
            buttonStatus = SkinButtonStatus.Unequiped;
        } 
        else if (skinStatus.owned == false)
        {
            buttonStatus = SkinButtonStatus.NotOwned;
        }
        SkinEquipButton.UpdateStatus(buttonStatus);
    }

    public void EquipSkin()
    {
        skinSystem.EquipSkin(skinID);
    }
}


