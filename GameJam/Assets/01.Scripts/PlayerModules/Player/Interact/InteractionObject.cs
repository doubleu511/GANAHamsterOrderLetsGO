using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class InteractionObject : MonoBehaviour
{
    [field:SerializeField]
    public virtual string ObjectName { get; set; }

    [SerializeField]
    protected Transform keyPosTrm;
    [SerializeField]
    protected GameObject keyObjPrefab;
    protected GameObject keyObj;

    private bool isActive;
    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;
            keyObj?.SetActive(isActive);
        }
    }

    private void Awake()
    {
        keyObj = Instantiate(keyObjPrefab);
        keyObj.transform.position = keyPosTrm.position;
        Vector3 pos = keyObj.transform.position;
        keyObj.transform.DOMove(new Vector2(pos.x, pos.y + .1f), 1).SetLoops(-1,LoopType.Yoyo);
        IsActive = false;
    }

    public virtual void Interact()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerInteraction>())
        {
            IsActive = false;
        }
    }
}
