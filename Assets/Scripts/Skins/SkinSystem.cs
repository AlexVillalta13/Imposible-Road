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

        OnSkinStatusChanged?.Invoke();

        Material materialToEquip = skinsScriptableObject.GetSkin(skinToEquip).materialSkin;
        OnSkinEquipped?.Invoke(materialToEquip);
    }

    public void BuySkin(string skinToBuyID)
    {
        skinStatusDictionary[skinToBuyID].owned = true;

        EquipSkin(skinToBuyID);
    }

    public string GetRandomNotOwnedSkins()
    {
        List<SkinStatus> skinsNotOwned = new List<SkinStatus>();
        foreach(SkinStatus skinStatus in skinStatusDictionary.Values)
        {
            if(skinStatus.owned == false)
            {
                skinsNotOwned.Add(skinStatus);
            }
        }
        int randomNumber = UnityEngine.Random.Range(0, skinsNotOwned.Count);
        Debug.Log("Random number: " + randomNumber + " Skins count: " + skinsNotOwned.Count);
        SkinStatus randomSkinStatus = skinsNotOwned[randomNumber];
        return randomSkinStatus.skinID;
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
    
    public bool owned = false;
    public bool equiped = false;
}
