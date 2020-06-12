using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUIElementsUpdater : MonoBehaviour
{
    [SerializeField] SkinUIElement skinUIElementPrefab = null;

    SkinSystem skinSystem = null;
    SkinsScriptableObject skins = null;

    List<SkinUIElement> UIElementsList = new List<SkinUIElement>();

    private void Awake()
    {
        Debug.Log("Awake");

        skinSystem = FindObjectOfType<SkinSystem>();
        //skins = skinSystem.GetSkinsScriptableObject();
        CreateUIElements();
        UpdateUIElements();
    }

    private void Start()
    {
        Debug.Log("Started");

        //CreateUIElements();
        //UpdateUIElements();
    }

    private void OnEnable()
    {
        Debug.Log("Enabled");

        UpdateUIElements();
        skinSystem.OnSkinStatusChanged += UpdateUIElements;

        
    }

    private void OnDisable()
    {
        skinSystem.OnSkinStatusChanged -= UpdateUIElements;
    }

    private void CreateUIElements()
    {
        Debug.Log("CreateUIElements");
        foreach (Skin skin in skinSystem.GetSkinsScriptableObject().GetSkinList())
        {
            SkinUIElement UIElement = Instantiate(skinUIElementPrefab, gameObject.transform);
            UIElementsList.Add(UIElement);
            UIElement.Init(skin.uniqueID, skin.skinName, skin.skinPreviewSprite, skinSystem);
        }
    }

    private void UpdateUIElements()
    {
        Debug.Log("UpdateUIElements: " + UIElementsList.Count);

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
