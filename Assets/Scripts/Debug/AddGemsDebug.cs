using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGemsDebug : MonoBehaviour
{
    GemsManager gemsManager;
    [SerializeField] int gemsToAdd = 100;

    private void Awake()
    {
        gemsManager = FindObjectOfType<GemsManager>();
    }

    public void AddGems()
    {
        gemsManager.AddGems(gemsToAdd);
    }
}
