using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBroken : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Broken()
    {
        animator.SetTrigger("IsBroken");
    }

    public void ResetBroken()
    {
        animator.ResetTrigger("IsBroken");
    }

}
