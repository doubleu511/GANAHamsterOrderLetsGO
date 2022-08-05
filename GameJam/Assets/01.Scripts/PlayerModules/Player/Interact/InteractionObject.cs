using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class InteractionObject : MonoBehaviour
{
    [field:SerializeField]
    public virtual string ObjectName { get; set; }


    public virtual void Interact()
    {

    }

}
