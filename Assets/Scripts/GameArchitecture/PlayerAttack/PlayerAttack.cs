using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] protected GameObject target;
    private Vector3 startExplosionPosition;
    public Attack[] attacks;
    protected List<Attack> comboAttacks;
    protected List<Attack> strongAttacks;
    protected List<Attack> specialAttacks1;
    protected List<Attack> specialAttacks2;
    protected List<Attack> finalAttacks;
    protected Animator animator;
    public string characterName;
    protected int attackIndex;
    protected int comboIndex;
    protected float comboTimer = 0.4f;
    protected float strongAttackTimer = 1.5f;
    protected float defaultAttackTimer = 0.3f;
    [SerializeField] protected float reactTimer = 1.0f;
    protected float currentReactTimer;
    protected float currentAttackTimer;
    protected float currentComboTimer;
    protected float currentStrongAttackTimer;
    protected bool canActivateComboTimer = false;
    protected bool canActivateStrongAttackTimer = false;
    protected bool isAttacking = false;
    protected bool isReacting = false;
    
    protected void ReadAttacks()
    {
        for(int i = 0; i < attacks.Length; i++)
        {
            Attack attack = attacks[i];
            attack.characterName = characterName;
            StreamReader sr = new StreamReader(Application.dataPath + "/Scripts/" + attack.characterName + "Scripts/TextFiles/" + attack.attacksFileName + ".txt");
            string fileContents = sr.ReadToEnd();
            sr.Close();
            string[] triggers = fileContents.Split("\n"[0]);
            foreach(string trigger in triggers)
            {
                attack.attacksTrigger.Add(trigger);
            }
        }
    }

    protected void BuildAttackLists()
    {
        comboAttacks = BuildAttacksList(AttackType.COMBO);
        strongAttacks = BuildAttacksList(AttackType.STRONG);
        specialAttacks1 = BuildAttacksList(AttackType.SPECIAL_1);
        specialAttacks2 = BuildAttacksList(AttackType.SPECIAL_2);
        finalAttacks = BuildAttacksList(AttackType.FINAL);
    }

    private List<Attack> BuildAttacksList(AttackType attackType)
    {
        List<Attack> attacksList = new List<Attack>();
        for(int i = 0; i < attacks.Length; i++)
        {
            if(attacks[i].attackType == attackType)
            {
                attacksList.Add(attacks[i]);
            }
        }
        return attacksList;
    }

    public virtual void InitializeIndexesAndTimers()
    {
        attackIndex = 0;
        comboIndex = 0;
        currentComboTimer = comboTimer;
        currentStrongAttackTimer = strongAttackTimer;
        currentReactTimer = reactTimer;
    }

    protected void CheckReact()
    {
        if(isReacting)
        {
            currentReactTimer -= Time.deltaTime;
            if(currentReactTimer < 0.0f)
            {
                isReacting = false;
                currentReactTimer = reactTimer;
            }
        }
    }

    protected void ResetComboTimer()
    {
        if(canActivateComboTimer)
        {
            currentComboTimer -= Time.deltaTime;
            if(currentComboTimer <= 0.0f)
            {
                currentComboTimer = comboTimer;
                canActivateComboTimer = false;
                comboIndex = 0;
            }
        }
    }

    protected void ResetStrongAttackTimer()
    {
        if(canActivateStrongAttackTimer)
        {
            currentStrongAttackTimer -= Time.deltaTime;
            if(currentStrongAttackTimer <= 0.0f)
            {
                currentStrongAttackTimer = strongAttackTimer;
                canActivateStrongAttackTimer = false;
                canActivateComboTimer = false;
                currentComboTimer = comboTimer;
                comboIndex = 0;
            }
        }
    }

    protected void InitializeAttackIndex(List<Attack> attacks)
    {
        attackIndex = Random.Range(0,attacks.Count);
    }

    protected void TriggerAnimation(List<Attack> attacks)
    {
        animator.SetTrigger(attacks[attackIndex].attacksTrigger[comboIndex].Trim());
    }

    protected void ComboAttack()
    {
        if(comboIndex == 0)
        {
            InitializeAttackIndex(comboAttacks);
        }
        TriggerAnimation(comboAttacks);
        comboIndex++;
        canActivateComboTimer = true;
        currentComboTimer = comboTimer;
        if(comboIndex == comboAttacks[attackIndex].attacksTrigger.Count)
        {
            canActivateComboTimer = false;
            canActivateStrongAttackTimer = true;
            comboIndex = 0;
            currentStrongAttackTimer = strongAttackTimer;
        }
    }

    protected void StrongAttack()
    {
        if(comboIndex == 0)
        {
            InitializeAttackIndex(strongAttacks);
        }
        TriggerAnimation(strongAttacks);
        canActivateComboTimer = true;
        canActivateStrongAttackTimer = false;
        currentComboTimer = comboTimer;
    }

    public void IsReacting()
    {
        isReacting = true;
        currentReactTimer = reactTimer;
    }

}
