using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] GameObject skinsMenu = null;

    public void Enable(bool state) 
    { 
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(state);
        }

        skinsMenu.SetActive(false);
    }
}
