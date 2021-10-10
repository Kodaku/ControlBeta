using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    protected Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void Walk(bool isWalking)
    {
        animator.SetBool("IsWalking", isWalking);
    }

    public void SetBlendSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public void FastRunning(bool fastRun)
    {
        animator.SetBool("FastRun", fastRun);
    }

    public void ChargingEnergy(bool charging)
    {
        animator.SetBool("EnergyChargeStart", charging);
    }

    public void Evade()
    {
        animator.SetTrigger("Escape");
    }

    public void GuardBreak()
    {
        animator.SetTrigger("GuardBreak");
    }

    public void GuardBreakReaction()
    {
        animator.SetTrigger("GuardBreakReaction");
    }

    public void Damage()
    {
        animator.SetTrigger("LowReaction");
    }
}
