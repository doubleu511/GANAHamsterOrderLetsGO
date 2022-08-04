using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator faceAnimator;
    [SerializeField] Animator footAnimator;

    private readonly int hashIsMove = Animator.StringToHash("isMove");

    public void SetIsMove(bool value)
    {
        faceAnimator.SetBool(hashIsMove, value);
        footAnimator.SetBool(hashIsMove, value);
    }
}
