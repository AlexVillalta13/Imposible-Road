using Bayat.SaveSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SkinSystem : MonoBehaviour
{
    string identifier = "Skin System";

    [SerializeField] SkinsScriptableObject skinsScriptableObject = null;

    protected Dictionary<string, SkinStatus> skinStatusDictionary;

    public event Action OnSkinStatusChanged;
    public event Action<Material> OnSkinEquipped;

    private void Awake()
    {
        BuildSkinStatusTable();
    }


    async private void Start()
    {
        if (await SaveSystemAPI.ExistsAsync(identifier) == false)
        {
            //BuildSkinStatusTable();
            await SaveSystemAPI.SaveAsync(identifier, skinStatusDictionary);
            return;
        }

        await LoadSkinData();
    }

    private async Task LoadSkinData()
    {
        //BuildSkinStatusTable();
        Dictionary<string, SkinStatus> loadedSkinStatus = await SaveSystemAPI.LoadAsync<Dictionary<string, SkinStatus>>(identifier);
        foreach (string key in loadedSkinStatus.Keys)
        {
            skinStatusDictionary[key] = loadedSkinStatus[key];
        }
        OnSkinStatusChanged?.Invoke();
        SearchForEquippedSkin();
    }

    public void ChangeEquipSkinStatus(string skinToEquip)
    {
        foreach (SkinStatus skinStatus in skinStatusDictionary.Values)
        {
            skinStatus.equiped = false;
        }

        skinStatusDictionary[skinToEquip].equiped = true;
        SaveSystemAPI.SaveAsync(identifier, skinStatusDictionary);

        OnSkinStatusChanged?.Invoke();

        EquipSkin(skinToEquip);
    }

    private void SearchForEquippedSkin()
    {
        foreach(SkinStatus skinStatus in skinStatusDictionary.Values)
        {
            if(skinStatus.equiped == true)
            {
                string skinID = skinStatus.skinID;
                EquipSkin(skinID);
            }
        }
    }

    private void EquipSkin(string skinToEquip)
    {
        Material materialToEquip = skinsScriptableObject.GetSkin(skinToEquip).materialSkin;
        OnSkinEquipped?.Invoke(materialToEquip);
    }

    public void BuySkin(string skinToBuyID)
    {
        skinStatusDictionary[skinToBuyID].owned = true;

        ChangeEquipSkinStatus(skinToBuyID);
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
