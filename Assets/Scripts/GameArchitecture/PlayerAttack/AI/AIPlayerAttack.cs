using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayerAttack : PlayerAttack
{
    private NavMeshAgent agent;
    private AIPlayerAnimations aIPlayerAnimations;
    protected AIPlayerMessage aIPlayerMessage;
    private float attackTimer = 0.35f;
    protected bool executingSpecialAttack = false;
    [SerializeField] protected GameObject specialAttack1VFX;
    [SerializeField] protected GameObject specialAttack2VFX;
    [SerializeField] protected GameObject specialAttack3VFX;

    // Start is called before the first frame update
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        aIPlayerAnimations = GetComponent<AIPlayerAnimations>();
        aIPlayerMessage = GetComponent<AIPlayerMessage>();

        // target = GameObject.FindGameObjectWithTag("Player");

        // agent.SetDestination(target.transform.position);

        ReadAttacks();
        BuildAttackLists();
        InitializeIndexesAndTimers();

        checkHitTime1 = specialAttack1Timer / (specialAttack1HitNumber - 1);
        checkHitTime2 = specialAttack2Timer / (specialAttack2HitNumber - 1);
        checkHitTime3 = specialAttack3Timer / (specialAttack3HitNumber - 1);
    }

    public override void InitializeIndexesAndTimers()
    {
        base.InitializeIndexesAndTimers();
        currentAttackTimer = attackTimer;
    }

    // Update is called once per frame
    public virtual void SpecialAttack(PlayerStates currentState)
    {
        if(currentState == PlayerStates.SPECIAL_1 && !executingSpecialAttack && !isReacting)
        {
            executingSpecialAttack = true;
            aIPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_SPECIAL_ATTACK, new string[]{"Enemy", "Bruce"});
            aIPlayerAnimations.ExecuteSpecialAttack1();
            ExecuteSpecialAttack1();
            StartCoroutine(EndSpecialAttack1());
            
        }
        if(currentState == PlayerStates.SPECIAL_2 && !executingSpecialAttack && !isReacting)
        {
            executingSpecialAttack = true;
            aIPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_SPECIAL_ATTACK, new string[]{"Enemy", "Bruce"});
            aIPlayerAnimations.ExecuteSpecialAttack2();
            ExecuteSpecialAttack2();
            StartCoroutine(EndSpecialAttack2());
        }
        if(currentState == PlayerStates.FINAL_ATTACK && !executingSpecialAttack && !isReacting)
        {
            executingSpecialAttack = true;
            aIPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_SPECIAL_ATTACK, new string[]{"Enemy", "Bruce"});
            aIPlayerAnimations.ExecuteSpecialAttack3();
            ExecuteSpecialAttack3();
            StartCoroutine(EndSpecialAttack3());
        }
    }

    public virtual void FixedUpdate()
    {
        CheckReact();
    }

    public virtual void Attack()
    {
        // CheckReact();
        currentAttackTimer -= Time.deltaTime;
        if(currentAttackTimer <= 0.0f && !isReacting)
        {
            if(!canActivateStrongAttackTimer)
            {
                ComboAttack();
                aIPlayerMessage.PrepareAndSendMessage(MessageTypes.ACTION_TERMINATED, new string[]{"Enemy", "Bruce"});
            }
            else if(canActivateStrongAttackTimer)
            {
                StrongAttack();
                aIPlayerMessage.PrepareAndSendMessage(MessageTypes.ACTION_TERMINATED, new string[]{"Enemy", "Bruce"});
            }
            currentAttackTimer = attackTimer;
        }
        ResetComboTimer();
        ResetStrongAttackTimer();
    }

    public virtual void ExecuteSpecialAttack1()
    {

    }

    private IEnumerator EndSpecialAttack1()
    {
        yield return new WaitForSeconds(specialAttack1Timer);
        executingSpecialAttack = false;
        aIPlayerAnimations.EndSpecialAttack1();
        specialAttack1VFX.gameObject.SetActive(false);
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.END_SPECIAL_ATTACK, new string[]{"Enemy", "Bruce"});
    }

    public virtual void ExecuteSpecialAttack2()
    {

    }

    public virtual IEnumerator EndSpecialAttack2()
    {
        yield return new WaitForSeconds(specialAttack2Timer);
        executingSpecialAttack = false;
        aIPlayerAnimations.EndSpecialAttack2();
        specialAttack2VFX.gameObject.SetActive(false);
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.END_SPECIAL_ATTACK, new string[]{"Enemy", "Bruce"});
    }

    public virtual void ExecuteSpecialAttack3()
    {

    }

    public virtual IEnumerator EndSpecialAttack3()
    {
        yield return new WaitForSeconds(specialAttack3Timer);
        aIPlayerAnimations.EndSpecialAttack3();
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.END_SPECIAL_ATTACK, new string[]{"Enemy", "Bruce"});
    }
}
