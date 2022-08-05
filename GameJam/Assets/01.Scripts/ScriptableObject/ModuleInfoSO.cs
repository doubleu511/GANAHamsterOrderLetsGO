using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Module Scriptable Object", menuName = "ScriptableObjects/Module Scriptable Object")]
public class ModuleInfoSO : ScriptableObject
{
    public string moduleName;
    public Define.ItemSlot moduleSlot;
    public Sprite moduleIconSpr;

    [TextArea(3, 5)]
    public string moduleLore;
}
