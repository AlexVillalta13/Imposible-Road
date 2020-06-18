using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUIElementsUpdater : MonoBehaviour
{
    [SerializeField] SkinUIElement skinUIElementPrefab = null;

    SkinSystem skinSystem = null;

    List<SkinUIElement> UIElementsList = new List<SkinUIElement>();

    private void Awake()
    {
        skinSystem = FindObjectOfType<SkinSystem>();
        CreateUIElements();
    }

    private void OnEnable()
    {
        UpdateUIElements();
        skinSystem.OnSkinStatusChanged += UpdateUIElements;
    }

    private void OnDisable()
    {
        skinSystem.OnSkinStatusChanged -= UpdateUIElements;
    }

    private void CreateUIElements()
    {
        foreach (Skin skin in skinSystem.GetSkinsScriptableObject().GetSkinList())
        {
            SkinUIElement UIElement = Instantiate(skinUIElementPrefab, gameObject.transform);
            UIElementsList.Add(UIElement);
            UIElement.Init(skin.uniqueID, skin.skinName, skin.skinPreviewSprite, skinSystem);
        }
    }

    private void UpdateUIElements()
    {
        foreach (SkinUIElement UIElement in UIElementsList)
        {
            SkinStatus skinStatus;

            string ID = UIElement.skinID;

            var dictionary = skinSystem.GetSkinStatusDictionary();

            skinStatus = dictionary[ID];

            UIElement.UpdateStatus(skinStatus);
        }
    }
}
