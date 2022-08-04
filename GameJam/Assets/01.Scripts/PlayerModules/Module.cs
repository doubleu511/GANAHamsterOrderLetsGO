using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module : MonoBehaviour
{
    public ModuleInfoSO moduleInfo;

    public abstract void ModuleEquip();
    public abstract void ModuleUpdate();
    public abstract void ModuleUnequip();
}
