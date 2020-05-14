using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    int gemsHolding = 1;

    MeshRenderer mesh;

    GemsManager gemsManager;

    private void Awake()
    {
        mesh = GetComponentInChildren<MeshRenderer>();
    }

    public void Init(GemsManager gemsManager)
    {
        this.gemsManager = gemsManager;
    } 

    public void ActivateMeshGameObject()
    {
        mesh.gameObject.SetActive(true);
    }

    public void PickupGem()
    {
        gemsManager.AddGems(gemsHolding);
        mesh.gameObject.SetActive(false);
    }
}
