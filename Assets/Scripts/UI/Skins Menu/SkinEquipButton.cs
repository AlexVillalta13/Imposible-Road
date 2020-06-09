using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinEquipButton : MonoBehaviour
{
    public enum Status { NotOwned, Unequiped, Equipped}

    public Status buttonStatus;

    public void UpdateStatus(Status status)
    {
        buttonStatus = status;
        switch(status)
        {
            case Status.Equipped:
                // Equipped
                break;
            case Status.Unequiped:
                // Unequiped
                break;
            case Status.NotOwned:
                // Not Owned
                break;
        }
    }
}
