using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayerAnimations : PlayerAnimations
{
    
    public void ExecuteSpecialAttack1()
    {
        animator.SetTrigger("AreaAttackStart");
    }

    public void EndSpecialAttack1()
    {
        animator.SetTrigger("AreaAttackEnd");
    }

    public void ExecuteSpecialAttack2()
    {
        animator.SetTrigger("SpecialAttack2Start");
    }

    public void EndSpecialAttack2()
    {
        animator.SetTrigger("SpecialAttack2End");
    }

    public void ExcecuteSpecialAttack3()
    {
        animator.SetTrigger("ChidoriStart");
    }

    public void ReleaseSpecialAttack3()
    {
        animator.SetTrigger("ChidoriRelease");
    }

    public void EndSpecialAttack3()
    {
        animator.SetTrigger("ChidoriEnd");
    }
}
