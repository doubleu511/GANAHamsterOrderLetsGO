using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float interactRadius = 1f;
    private InteractionObject interactObj;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactObj = collision.GetComponent<InteractionObject>();
        if (interactObj != null)
        {
            interactObj.IsActive = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position , interactRadius);
    }
}
