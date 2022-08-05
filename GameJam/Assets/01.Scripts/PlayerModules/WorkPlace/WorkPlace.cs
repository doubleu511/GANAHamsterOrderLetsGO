using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPlace : InteractionObject
{
    public static bool IsWorkPlaceOpen = false;

    public override void Interact()
    {
        if (!IsWorkPlaceOpen)
        {
            EquipmentPanel equipmentPanel = FindObjectOfType<EquipmentPanel>();
            equipmentPanel.PanelOpen();
            IsWorkPlaceOpen = true;
        }
    }
}
