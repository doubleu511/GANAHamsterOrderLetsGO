using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator faceAnimator;
    [SerializeField] Animator footAnimator;
    [SerializeField] Animator headAnimator;

    private readonly int hashIsIdle = Animator.StringToHash("isIdle");

    private readonly int hashIsAngry = Animator.StringToHash("isAngry");

    private readonly int hashIsWalk = Animator.StringToHash("isWalk");
    private readonly int hashIsJump = Animator.StringToHash("isJump");
    private readonly int hashIsFall = Animator.StringToHash("isFall");

    private bool isJumping = false;
    private bool isFalling = false;

    // ¸Ó¸®
    public void SetIsIdleHead(bool isIdle)
    {
        headAnimator.SetBool(hashIsIdle, isIdle);
    }

    // ¾ó±¼
    public void SetIsAngry(bool value)
    {
        faceAnimator.SetBool(hashIsAngry, value);
    }

    // ¹ß
    public void SetIsWalk(bool value)
    {
        if (isJumping || isFalling) return;
        footAnimator.SetBool(hashIsWalk, value);
    }

    public void SetIsJump(bool value)
    {
        footAnimator.SetBool(hashIsJump, value);
        headAnimator.SetBool(hashIsJump, value);
        isJumping = value;
    }

    public void SetIsFall(bool value)
    {
        footAnimator.SetBool(hashIsFall, value);
        headAnimator.SetBool(hashIsFall, value);
        isFalling = value;
    }
}
