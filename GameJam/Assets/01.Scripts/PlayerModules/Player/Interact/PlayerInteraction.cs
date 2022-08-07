using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float interactRadius = 1f;
    private InteractionObject interactObj;

    [SerializeField]
    protected GameObject keyObj;

    private bool isKeyActive;
    public bool IsKeyActive
    {
        get
        {
            return isKeyActive;
        }
        set
        {
            isKeyActive = value;
            keyObj?.SetActive(isKeyActive);
            if(isKeyActive)
            {
                DOTween.Kill(keyObj);
                keyObj.transform.position = transform.GetChild(0).position;
                keyObj.transform.DOLocalMoveY(.1f, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            }    
        }
    }

    private void Awake()
    {
        IsKeyActive = false;

    }

    private void Update()
    {
        if (interactObj != null)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                // UI 불러오기
                print("INTERACT : " + interactObj.ObjectName);
                interactObj.Interact();
            }
        }
    }

    private void LateUpdate()
    {
        if (keyObj != null)
            keyObj.transform.localScale = new Vector3(GameManager.Player.transform.localScale.x, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<InteractionObject>())
        {
            interactObj = collision.GetComponent<InteractionObject>();
            IsKeyActive = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<InteractionObject>())
        {
            interactObj = null;
            IsKeyActive = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position , interactRadius);
    }
}
