using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    int gemsHolding = 1;

    GameObject meshGameObject;

    GemsManager gemsManager;

    private void Awake()
    {
        meshGameObject = transform.GetChild(0).gameObject;
    }

    public void Init(GemsManager gemsManager)
    {
        this.gemsManager = gemsManager;
    } 

    public void ActivateMeshGameObject()
    {
        meshGameObject.SetActive(true);
    }

    public void PickupGem()
    {
        gemsManager.AddGems(gemsHolding);
        meshGameObject.SetActive(false);
    }
}
