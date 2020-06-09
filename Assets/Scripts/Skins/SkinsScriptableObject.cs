using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin List", menuName = "Impossible Road/Skin List", order = 1)]
public class SkinsScriptableObject : ScriptableObject
{
    [SerializeField] private List<Skin> skins = null;

    public List<Skin> GetSkinList()
    {
        return skins;
    }

    public Skin GetSkin(string skinID)
    {
        foreach(Skin skin in skins)
        {
            if(skinID == skin.uniqueID)
            {
                return skin;
            }
        }
        return null;
    }
}

[Serializable]
public class Skin : ISerializationCallbackReceiver
{
    public string skinName;
    public string uniqueID;
    
    public Sprite skinPreviewSprite;
    public Material materialSkin;

    public void OnAfterDeserialize() {}

    public void OnBeforeSerialize()
    {
        if(String.IsNullOrEmpty(uniqueID))
        {
            uniqueID = System.Guid.NewGuid().ToString();
        }
    }
}