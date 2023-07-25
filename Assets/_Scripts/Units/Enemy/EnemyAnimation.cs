using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation
{
    private readonly Animator animator;

    public EnemyAnimation(Animator animator)
    {
        this.animator = animator;
    }

    public void PlayDeathAnimation() 
    {
        animator.SetTrigger("Death");
    }

  
}
