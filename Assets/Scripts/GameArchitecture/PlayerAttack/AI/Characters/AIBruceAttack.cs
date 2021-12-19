using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBruceAttack : AIPlayerAttack
{
    private Vector3 specialAttack2SpawnPoint;
    [SerializeField] private GameObject specialAttack2Aura;
    [SerializeField] private GameObject specialAttack2Prepration;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        specialAttack2Aura.gameObject.SetActive(false);

    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void SpecialAttack(PlayerStates currentState, Vector3 targetPosition)
    {
        base.SpecialAttack(currentState, targetPosition);
    }

    public override void ExecuteSpecialAttack1(Vector3 targetPosition)
    {
        base.ExecuteSpecialAttack1(targetPosition);
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_EXPLOSION_ATTACK, new string[]{"Player", "Player", "CAN_ESCAPE"});
    }

    private void ActivateSpecialAttack1()
    {
        specialAttack1VFX = Instantiate(specialAttack1VFX, target, Quaternion.identity);
        specialAttack1VFX.gameObject.SetActive(true);
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{"Enemy", this.gameObject.name, "400", "Sub"});
        
        SendSpecialAttack1Execution();
    }

    private void SendSpecialAttack1Execution()
    {
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.EXECUTE_EXPLOSION_ATTACK, new string[]{"Player", "Player", "150", "5"});
    }

    public override void ExecuteSpecialAttack2(Vector3 targetPosition)
    {
        base.ExecuteSpecialAttack2(targetPosition);
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_EXPLOSION_ATTACK, new string[]{"Player", "Player", "CANNOT_ESCAPE"});
    }
    
    private void ActivateSpecialAttack2()
    {
        specialAttack2SpawnPoint = target;
        specialAttack2Aura.gameObject.SetActive(true);
        specialAttack2Prepration = Instantiate(specialAttack2Prepration, specialAttack2SpawnPoint, Quaternion.identity);
        specialAttack2Prepration.gameObject.SetActive(true);
    }

    private void ActivateSpecialAttack2Effect()
    {
        specialAttack2VFX = Instantiate(specialAttack2VFX, specialAttack2SpawnPoint, Quaternion.identity);
        specialAttack2VFX.gameObject.SetActive(true);
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{"Enemy", this.gameObject.name, "700", "Sub"});
        SendSpecialAttack2Execution();
    }

    private void SendSpecialAttack2Execution()
    {
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.EXECUTE_EXPLOSION_ATTACK, new string[]{"Player", "Player", "250", "5"});
        // StartCoroutine(RepeatSendSpecialAttack2Execution());
    }

    private IEnumerator RepeatSendSpecialAttack2Execution()
    {
        yield return new WaitForSeconds(checkHitTime2);
        if(specialAttack2HitCounter < specialAttack2HitNumber)
        {
            specialAttack2HitCounter++;
            SendSpecialAttack2Execution();
        }
    }

    public override IEnumerator EndSpecialAttack2()
    {
        yield return base.EndSpecialAttack2();
        specialAttack2HitCounter = 0;
        specialAttack2Aura.gameObject.SetActive(false);
        specialAttack2Prepration.gameObject.SetActive(false);
    }

    public override void ExecuteSpecialAttack3(Vector3 targetPosition)
    {
        base.ExecuteSpecialAttack3(targetPosition);
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_EXPLOSION_ATTACK, new string[]{"Player", "Player", "CANNOT_ESCAPE"});
    }

    private void ActivateSpecialAttack3()
    {
        specialAttack3VFX = Instantiate(specialAttack3VFX, target, Quaternion.identity);
        specialAttack3VFX.gameObject.SetActive(true);
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{"Enemy", this.gameObject.name, "800", "Sub"});
        
        SendSpecialAttack3Execution();
    }

    private void SendSpecialAttack3Execution()
    {
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.EXECUTE_EXPLOSION_ATTACK, new string[]{"Player", "Player", "400", "5"});
        // StartCoroutine(RepeatSendSpecialAttack3Execution());
    }

    private IEnumerator RepeatSendSpecialAttack3Execution()
    {
        yield return new WaitForSeconds(checkHitTime3);
        if(specialAttack3HitCounter < specialAttack3HitNumber)
        {
            specialAttack3HitCounter++;
            SendSpecialAttack3Execution();
        }
    }

    public override IEnumerator EndSpecialAttack3()
    {
        yield return base.EndSpecialAttack3();
        StartCoroutine(EndSpecialAttack3Effect());
    }

    private IEnumerator EndSpecialAttack3Effect()
    {
        yield return new WaitForSeconds(specialAttack3Timer + 3.0f);
        specialAttack3HitCounter = 0;
        executingSpecialAttack = false;
        specialAttack3VFX.gameObject.SetActive(false);
    }
}
