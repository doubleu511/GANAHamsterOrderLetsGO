using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module : MonoBehaviour
{
    public ModuleInfoSO moduleInfo;

    public void Equip()
    {
        gameObject.SetActive(true);
        ModuleEquip();

        EquipmentPanel equipmentPanel = FindObjectOfType<EquipmentPanel>();
        equipmentPanel.SetLore(moduleInfo, moduleInfo.moduleName);
    }

    public void Unequip()
    {
        gameObject.SetActive(false);
        ModuleUnequip();

        EquipmentPanel equipmentPanel = FindObjectOfType<EquipmentPanel>();
        equipmentPanel.SetLore(moduleInfo, null);
    }

    public abstract void ModuleEquip();
    public abstract void ModuleUpdate();
    public abstract void ModuleUnequip();
}
