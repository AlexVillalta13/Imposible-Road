using Bayat.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteData : MonoBehaviour
{
    public void DeleteAllData()
    {
        SaveSystemAPI.ClearAsync();
    }
}
