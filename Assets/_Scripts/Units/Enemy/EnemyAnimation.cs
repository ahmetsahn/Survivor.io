using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator enemyAnimator;

    private void Start()
    {
        if (TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.OnDeath += PlayDeathAnimation;
        }
    }


    public void PlayDeathAnimation() 
    {
        enemyAnimator.Play("Death");
    }

  
}
