using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : GenericManageableClass<PlayerManager>
{
    private Animator Animator { get; set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void AnimationToDeath(bool isDead)
    {
        Animator.SetBool("Death", isDead);
    }

    public void AnimationToFlex()
    {
        Animator.SetTrigger("Flex");
    }
}