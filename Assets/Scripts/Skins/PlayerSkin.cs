using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    SkinSystem skinSystem;

    [SerializeField] MeshRenderer meshRenderer = null;

    private void Awake()
    {
        skinSystem = FindObjectOfType<SkinSystem>();
    }

    private void OnEnable()
    {
        skinSystem.OnSkinEquipped += EquipSkin;
    }

    private void OnDisable()
    {
        skinSystem.OnSkinEquipped -= EquipSkin;
    }

    private void EquipSkin(Material material)
    {
        meshRenderer.material = material;
    }


}
