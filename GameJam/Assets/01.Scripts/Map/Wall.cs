using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (GameManager.Player.IsGround || ModuleBoosterHead.isBooster)
            {
                coll.sharedMaterial = GameManager.Game.staticPMat;
            }
            else
            {
                Global.Sound.Play("SFX/sfx_FallGround", Define.Sound.Effect);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            coll.sharedMaterial = GameManager.Game.bouncePMat;
        }
    }
}
