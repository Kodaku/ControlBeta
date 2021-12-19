using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErickAttacks : HumanPlayerAttack
{
    [SerializeField] private GameObject specialAttack1Execution;
    [SerializeField] private GameObject specialAttack2Spawner;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        InitializeTargetAndAura();
        specialAttack2VFX.gameObject.SetActive(false);
        specialAttack3VFX.gameObject.SetActive(false);
    }

    private void InitializeTargetAndAura()
    {
        // projectileTarget.transform.forward = this.transform.forward;

        // projectileTarget.transform.position = this.transform.position + 30.0f * projectileTarget.transform.forward;
        
        // projectileAura.gameObject.SetActive(false);
        // chidoriAura.gameObject.SetActive(false);
        // specialAttack3VFX.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public override void Attack(PlayerStates currentState)
    {
        base.Attack(currentState);
        // CheckChidoriExecution();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void ExecuteSpecialAttack1()
    {
        base.ExecuteSpecialAttack1();
        specialAttack1VFX = Instantiate(specialAttack1VFX, this.transform.position, Quaternion.identity);
        specialAttack1VFX.gameObject.SetActive(true);
        foreach(string targetName in SpecialAttackTargetManager.targetNames)
        {
            humanPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_ATTACK, new string[]{"Enemy", targetName, "CAN_ESCAPE"});
        }
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{gameObject.tag, gameObject.name, "300", "Sub"});
    }

    public void TriggerSpecialAttack1Effect()
    {
        specialAttack1Execution = Instantiate(specialAttack1Execution, this.transform.position + this.transform.forward * 4.0f, Quaternion.identity);
        specialAttack1Execution.gameObject.SetActive(true);
        SendExecutionMessage();
    }

    public void SendExecutionMessage()
    {
        foreach(string targetName in SpecialAttackTargetManager.targetNames)
        {
            humanPlayerMessage.PrepareAndSendMessage(MessageTypes.EXECUTE_ATTACK, new string[]{"Enemy", targetName, "150", "20"});
        }
        // StartCoroutine(RepeatSendExecutionSpecialAttack1());
    }

    private IEnumerator RepeatSendExecutionSpecialAttack1()
    {
        // print(checkHitTime1);
        yield return new WaitForSeconds(checkHitTime1);
        if(specialAttack1HitCounter < specialAttack1HitNumber)
        {
            specialAttack1HitCounter++;
            SendExecutionMessage();
        }
    }

    public override IEnumerator EndSpecialAttack1()
    {
        // specialAttack1HitCounter = 0;
        yield return base.EndSpecialAttack1();
        specialAttack1Execution.gameObject.SetActive(false);
        this.transform.position += this.transform.forward * 4.0f;
    }

    public override void ExecuteSpecialAttack2()
    {
        base.ExecuteSpecialAttack2();
        // specialAttack2VFX = Instantiate(specialAttack2VFX, specialAttack2Spawner.transform.position, Quaternion.identity);
        specialAttack2VFX.gameObject.SetActive(true);
        foreach(string targetName in SpecialAttackTargetManager.targetNames)
        {
            humanPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_ATTACK, new string[]{"Enemy", targetName, "CAN_ESCAPE"});
        }
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{gameObject.tag, gameObject.name, "400", "Sub"});
        SendExecutionMessage();
    }

    public override IEnumerator EndSpecialAttack2()
    {
        yield return base.EndSpecialAttack2();
        specialAttack2VFX.gameObject.SetActive(false);
    }

    public override void ExcecuteSpecialAttack3()
    {
        base.ExcecuteSpecialAttack3();
        // currentChidoriTimer = 0.0f;
        // chidoriAura.gameObject.SetActive(true);
        specialAttack3VFX.gameObject.SetActive(true);
        
        foreach(string targetName in SpecialAttackTargetManager.targetNames)
        {
            humanPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_ATTACK, new string[]{"Enemy", targetName, "CANNOT_ESCAPE"});
        }
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{gameObject.tag, gameObject.name, "800", "Sub"});
    }

    public void SlowMotion()
    {
        Time.timeScale = 0.2f;
    }

    public void NormalMotion()
    {
        Time.timeScale = 1.0f;
    }

    private void SpawnChidori()
    {
        // specialAttack3VFX = Instantiate(specialAttack3VFX, handSpawner.transform.position, Quaternion.identity);
        // specialAttack3VFX.gameObject.SetActive(true);
    }

    private void StartRunningForSpecialAttack3()
    {
        // executingChidori = true;
    }

    private void ApplyFinalAttackDamage()
    {
        foreach(string targetName in SpecialAttackTargetManager.targetNames)
        {
            humanPlayerMessage.PrepareAndSendMessage(MessageTypes.EXECUTE_ATTACK, new string[]{"Enemy", targetName, "500", "10"});
        }
        // StartCoroutine(RepeatApplyChidoriDamage());
    }

    // private IEnumerator RepeatApplyChidoriDamage()
    // {
    //     print(checkHitTime3);
    //     yield return new WaitForSeconds(checkHitTime3);
    //     if(specialAttack3HitCounter < specialAttack3HitNumber)
    //     {
    //         specialAttack3HitCounter++;
    //         ApplyChidoriDamage();
    //     }
    // }

    public override IEnumerator EndSpecialAttack3()
    {
        yield return base.EndSpecialAttack3();
        specialAttack3HitCounter = 0;
        // executingChidori = false;
        // hasExecutedChidori = false;
        // chidoriAura.gameObject.SetActive(false);
    }
}
