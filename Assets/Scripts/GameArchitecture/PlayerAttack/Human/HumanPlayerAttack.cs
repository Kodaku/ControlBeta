using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HumanPlayerAttack : PlayerAttack
{
    protected HumanPlayerAnimations humanPlayerAnimations;
    protected HumanPlayerMessage humanPlayerMessage;
    protected bool executingSpecialAttack = false;
    [SerializeField] protected GameObject specialAttack1VFX;
    [SerializeField] protected GameObject specialAttack2VFX;
    [SerializeField] protected GameObject specialAttack3VFX;
    [SerializeField] protected GameObject normalAura;
    [SerializeField] protected float specialAttack1Timer;
    [SerializeField] private float specialAttack2Timer;
    [SerializeField] private float specialAttack3Timer;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        humanPlayerAnimations = GetComponent<HumanPlayerAnimations>();
        humanPlayerMessage = GetComponent<HumanPlayerMessage>();
        ReadAttacks();
        BuildAttackLists();
        InitializeIndexesAndTimers();
    }

    public override void InitializeIndexesAndTimers()
    {
        base.InitializeIndexesAndTimers();
        currentAttackTimer = defaultAttackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        CheckReact();
    }
    public virtual void Attack(PlayerStates currentState)
    {
        if(currentState == PlayerStates.PUNCH && !isReacting && !executingSpecialAttack)
        {
            if(!canActivateStrongAttackTimer)
            {
                ComboAttack();
            }
        }
        if(currentState == PlayerStates.STRONG_PUNCH && !isReacting && !executingSpecialAttack)
        {
            if(canActivateStrongAttackTimer)
            {
                StrongAttack();
            }
        }
        if(currentState == PlayerStates.SPECIAL_1 && !isReacting && !executingSpecialAttack)
        {
            executingSpecialAttack = true;
            ExecuteSpecialAttack1();
            StartCoroutine(EndSpecialAttack1());
        }
        if(currentState == PlayerStates.SPECIAL_2 && !isReacting && !executingSpecialAttack)
        {
            executingSpecialAttack = true;
            ExecuteSpecialAttack2();
            StartCoroutine(EndSpecialAttack2());
        }
        if(currentState == PlayerStates.FINAL_ATTACK && !isReacting && !executingSpecialAttack)
        {
            executingSpecialAttack = true;
            ExcecuteSpecialAttack3();
            StartCoroutine(EndSpecialAttack3());
        }
        ResetComboTimer();
        ResetStrongAttackTimer();
    }

    public virtual void ExecuteSpecialAttack1()
    {
        humanPlayerAnimations.ExecuteSpecialAttack1();
    }

    public virtual IEnumerator EndSpecialAttack1()
    {
        yield return new WaitForSeconds(specialAttack1Timer);
        specialAttack1VFX.gameObject.SetActive(false);
        humanPlayerAnimations.EndSpecialAttack1();
        executingSpecialAttack = false;
    }

    public virtual void ExecuteSpecialAttack2()
    {
        humanPlayerAnimations.ExecuteSpecialAttack2();
    }

    public virtual IEnumerator EndSpecialAttack2()
    {
        yield return new WaitForSeconds(specialAttack2Timer);
        specialAttack2VFX.gameObject.SetActive(false);
        humanPlayerAnimations.EndSpecialAttack2();
        executingSpecialAttack = false;
    }

    public virtual void ExcecuteSpecialAttack3()
    {
        humanPlayerAnimations.ExcecuteSpecialAttack3();
    }

    public virtual IEnumerator EndSpecialAttack3()
    {
        yield return new WaitForSeconds(specialAttack3Timer);
        specialAttack3VFX.gameObject.SetActive(false);
        humanPlayerAnimations.EndSpecialAttack3();
        executingSpecialAttack = false;
    }
}
