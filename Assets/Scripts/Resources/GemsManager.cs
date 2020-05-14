using Bayat.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsManager : MonoBehaviour
{
    string identifier = "OwnedGems";
    int currencyOwned = -1;

    async private void Start()
    {
        if(await SaveSystemAPI.ExistsAsync(identifier) == false)
        {
            currencyOwned = 0;
            await SaveSystemAPI.SaveAsync(identifier, currencyOwned);
        }
        currencyOwned = await SaveSystemAPI.LoadAsync<int>(identifier);
    }

    public void AddGems(int amount)
    {
        currencyOwned += amount;
        SaveSystemAPI.SaveAsync(identifier, currencyOwned);
        //UpdateUI
    }

    public bool PaySomething(int amount)
    {
        if(currencyOwned < amount)
        {
            // can't pay
            return false;
        }
        currencyOwned -= amount;
        return true;
    }
}
