using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemsDisplay : MonoBehaviour
{
    TextMeshProUGUI text;

    GemsManager gemsManager;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        gemsManager = FindObjectOfType<GemsManager>();
    }

    private void OnEnable()
    {
        gemsManager.RegisterOnGemsQuantityChangedCallback(UpdateGemsText);
        gemsManager.FireOnGemsQuantityChangedEvent();
    }

    private void OnDisable()
    {
        gemsManager.UnregisterOnGemsQuantityChangedCallback(UpdateGemsText);
    }

    private void UpdateGemsText(int gemsOwned)
    {
        text.text = gemsOwned + " <sprite=\"diamond\" index=0 tint=1>";
    }
}
