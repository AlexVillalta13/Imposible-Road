using Bayat.SaveSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsManager : MonoBehaviour
{
    string identifier = "OwnedGems";
    int currencyOwned = -1;

    Action<int> onGemsQuantityChanged;

    public void RegisterOnGemsQuantityChangedCallback(Action<int> callback)
    {
        onGemsQuantityChanged += callback;
    }

    public void UnregisterOnGemsQuantityChangedCallback(Action<int> callback)
    {
        onGemsQuantityChanged -= callback;
    }

    public void FireOnGemsQuantityChangedEvent()
    {
        onGemsQuantityChanged(currencyOwned);
    }

    async private void Start()
    {
        if(await SaveSystemAPI.ExistsAsync(identifier) == false)
        {
            currencyOwned = 0;
            await SaveSystemAPI.SaveAsync(identifier, currencyOwned);
        }
        currencyOwned = await SaveSystemAPI.LoadAsync<int>(identifier);
        onGemsQuantityChanged?.Invoke(currencyOwned);
    }

    public void AddGems(int amount)
    {
        currencyOwned += amount;
        SaveSystemAPI.SaveAsync(identifier, currencyOwned);

        onGemsQuantityChanged?.Invoke(currencyOwned);
    }

    public int GetGemsCount()
    {
        return currencyOwned;
    }

    public bool PaySomething(int amount)
    {
        if(currencyOwned < amount)
        {
            // can't pay
            return false;
        }
        currencyOwned -= amount;
        SaveSystemAPI.SaveAsync(identifier, currencyOwned);

        onGemsQuantityChanged?.Invoke(currencyOwned);
        return true;
    }
}
