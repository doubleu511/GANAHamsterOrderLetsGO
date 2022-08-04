using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Module Scriptable Object", menuName = "ScriptableObjects/Module Scriptable Object")]
public class ModuleInfoSO : ScriptableObject
{
    public string moduleName;

    public Sprite moduleIconSpr;
}
