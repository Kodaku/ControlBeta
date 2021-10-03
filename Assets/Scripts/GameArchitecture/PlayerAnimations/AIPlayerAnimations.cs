using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerAnimations : PlayerAnimations
{
    public void ExecuteSpecialAttack1()
    {
        animator.SetTrigger("SpecialAttack1Start");
    }

    public void EndSpecialAttack1()
    {
        animator.SetTrigger("SpecialAttack1End");
    }

    public void ExecuteSpecialAttack2()
    {
        animator.SetTrigger("SpecialAttack2Start");
    }

    public void EndSpecialAttack2()
    {
        animator.SetTrigger("SpecialAttack2End");
    }

    public void ExecuteSpecialAttack3()
    {
        animator.SetTrigger("SpecialAttack3Start");
    }

    public void EndSpecialAttack3()
    {
        animator.SetTrigger("SpecialAttack3End");
    }
}
