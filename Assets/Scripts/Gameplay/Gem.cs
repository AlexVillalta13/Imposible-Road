using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    int gemsHolding = 1;

    GemsManager gemsManager;

    // TODO Init
    // TODO Activation

    public void PickupGem()
    {
        gemsManager.AddGems(gemsHolding);
    }
}
