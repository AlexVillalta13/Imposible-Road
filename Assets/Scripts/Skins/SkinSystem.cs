using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSystem : MonoBehaviour
{
    [SerializeField] SkinsScriptableObject skinsScriptableObject = null;

    protected Dictionary<string, SkinStatus> skinStatusDictionary;

    public event Action OnSkinStatusChanged;
    public event Action<Material> OnSkinEquipped;

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

        Material materialToEquip = skinsScriptableObject.GetSkin(skinToEquip).materialSkin;
        OnSkinEquipped(materialToEquip);
    }

    public void BuySkin(string skinToBuyID)
    {
        skinStatusDictionary[skinToBuyID].owned = true;

        EquipSkin(skinToBuyID);
    }

    public void GetNotOwnedSkins()
    {
        List<SkinStatus> skinsNotOwned = new List<SkinStatus>();
        foreach(SkinStatus skinStatus in skinStatusDictionary.Values)
        {
            if(skinStatus.owned == false)
            {
                skinsNotOwned.Add(skinStatus);
            }
        }
        int randomNumber = UnityEngine.Random.Range(0, skinsNotOwned.Count - 1);
        SkinStatus randomSkinStatus = skinsNotOwned[randomNumber];
        BuySkin(randomSkinStatus.skinID);
    }

    private void BuildSkinStatusTable()
    {
        if(skinStatusDictionary != null) { return; }

        skinStatusDictionary = new Dictionary<string, SkinStatus>();
        foreach(Skin skin in skinsScriptableObject.GetSkinList())
        {
            SkinStatus newSkinStatus = new SkinStatus();
            newSkinStatus.skinID = skin.uniqueID;
            skinStatusDictionary.Add(skin.uniqueID, newSkinStatus);
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
    public string skinID;
    // TODO change to false
    public bool owned = true;
    public bool equiped = false;
}
