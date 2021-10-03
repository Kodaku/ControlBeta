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
    public override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void SpecialAttack(PlayerStates currentState)
    {
        base.SpecialAttack(currentState);
    }

    public override void ExecuteSpecialAttack1()
    {
        base.ExecuteSpecialAttack1();
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_EXPLOSION_ATTACK, new string[]{"Player", "Player", "CAN_ESCAPE"});
    }

    private void ActivateSpecialAttack1()
    {
        specialAttack1VFX = Instantiate(specialAttack1VFX, target.transform.position, Quaternion.identity);
        specialAttack1VFX.gameObject.SetActive(true);
        // string info = PacketCreator.PrepareMessage();
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.EXECUTE_EXPLOSION_ATTACK, new string[]{"Player", "Player", "10", "5"});
        // GetComponent<PlayerMana>().DecreaseMana(10);
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{"Enemy", "Bruce", "40", "Sub"});
    }

    public override void ExecuteSpecialAttack2()
    {
        base.ExecuteSpecialAttack2();
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_EXPLOSION_ATTACK, new string[]{"Player", "Player", "CANNOT_ESCAPE"});
    }
    
    private void ActivateSpecialAttack2()
    {
        specialAttack2SpawnPoint = target.transform.position;
        specialAttack2Aura.gameObject.SetActive(true);
        specialAttack2Prepration = Instantiate(specialAttack2Prepration, specialAttack2SpawnPoint, Quaternion.identity);
        specialAttack2Prepration.gameObject.SetActive(true);
    }

    private void ActivateSpecialAttack2Effect()
    {
        specialAttack2VFX = Instantiate(specialAttack2VFX, specialAttack2SpawnPoint, Quaternion.identity);
        specialAttack2VFX.gameObject.SetActive(true);
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.EXECUTE_EXPLOSION_ATTACK, new string[]{"Player", "Player", "20", "5"});
        // GetComponent<PlayerMana>().DecreaseMana(15);
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{"Enemy", "Bruce", "70", "Sub"});
    }

    public override IEnumerator EndSpecialAttack2()
    {
        yield return base.EndSpecialAttack2();
        specialAttack2Aura.gameObject.SetActive(false);
        specialAttack2Prepration.gameObject.SetActive(false);
    }

    public override void ExecuteSpecialAttack3()
    {
        base.ExecuteSpecialAttack3();
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_EXPLOSION_ATTACK, new string[]{"Player", "Player", "CANNOT_ESCAPE"});
    }

    private void ActivateSpecialAttack3()
    {
        specialAttack3VFX = Instantiate(specialAttack3VFX, target.transform.position, Quaternion.identity);
        specialAttack3VFX.gameObject.SetActive(true);
        // string info = PacketCreator.PrepareMessage();
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.EXECUTE_EXPLOSION_ATTACK, new string[]{"Player", "Player", "30", "5"});
        // GetComponent<PlayerMana>().DecreaseMana(30);
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{"Enemy", "Bruce", "80", "Sub"});
    }

    public override IEnumerator EndSpecialAttack3()
    {
        yield return base.EndSpecialAttack3();
        StartCoroutine(EndSpecialAttack3Effect());
    }

    private IEnumerator EndSpecialAttack3Effect()
    {
        yield return new WaitForSeconds(specialAttackTimer + 3.0f);
        executingSpecialAttack = false;
        specialAttack3VFX.gameObject.SetActive(false);
    }
}
