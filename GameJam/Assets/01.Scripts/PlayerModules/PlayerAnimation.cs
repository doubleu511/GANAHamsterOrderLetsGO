using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    private readonly int hashIsMove = Animator.StringToHash("isMove");

    public void SetIsMove(bool value)
    {
        animator.SetBool(hashIsMove, value);
    }
}
