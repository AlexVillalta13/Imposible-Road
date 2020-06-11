using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinAquiredUI : MonoBehaviour
{
    SkinSystem skinSystem;

    [SerializeField] Image skinPreview = null;

    private void Awake()
    {
        skinSystem = FindObjectOfType<SkinSystem>();
    }

    public void SetupSkinAcquiredUI()
    {
        string skinsToGet = skinSystem.GetRandomNotOwnedSkins();

        Sprite skinPreviewSprite = skinSystem.GetSkinsScriptableObject().GetSkin(skinsToGet).skinPreviewSprite;
        skinPreview.sprite = skinPreviewSprite;
        GetNewSkin(skinsToGet);
    }

    private void GetNewSkin(string skinsToGet)
    {
        skinSystem.BuySkin(skinsToGet);
    }
}
