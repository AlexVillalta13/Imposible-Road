using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSystem : MonoBehaviour
{
    [SerializeField] SkinsScriptableObject skinsScriptableObject = null;

    protected Dictionary<string, SkinStatus> skinStatusDictionary;

    public event Action OnSkinStatusChanged;

    private void Awake()
    {
        BuildSkinStatusTable();
    }

    public void EquipSkin(string skinToEquip)
    {
        foreach (SkinStatus skinStatus in skinStatusDictionary.Values)
        {
            skinStatus.equiped = false;
        }

        skinStatusDictionary[skinToEquip].equiped = true;

        OnSkinStatusChanged();

        // TODO Change player material
    }

    public void BuySkin(string skinToBuyID)
    {
        skinStatusDictionary[skinToBuyID].owned = true;

        OnSkinStatusChanged();
    }

    public void GetNotOwnedSkins()
    {

    }

    private void BuildSkinStatusTable()
    {
        if(skinStatusDictionary != null) { return; }

        skinStatusDictionary = new Dictionary<string, SkinStatus>();
        foreach(Skin skin in skinsScriptableObject.GetSkinList())
        {
            skinStatusDictionary.Add(skin.uniqueID, new SkinStatus());
        }
        skinStatusDictionary[skinsScriptableObject.GetSkinList()[0].uniqueID].owned = true;
        skinStatusDictionary[skinsScriptableObject.GetSkinList()[0].uniqueID].equiped = true;
    }

    public SkinsScriptableObject GetSkinsScriptableObject()
    {
        return skinsScriptableObject;
    }

    public Dictionary<string, SkinStatus> GetSkinStatusDictionary()
    {
        return skinStatusDictionary;
    }
}

public class SkinStatus
{
    // TODO change to false
    public bool owned = true;
    public bool equiped = false;
}
