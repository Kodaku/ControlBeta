using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaryAttacks : HumanPlayerAttack
{
    private bool executingChidori = false;
    private bool hasExecutedChidori = false;
    private float chidoriTimer = 3.0f;
    private float currentChidoriTimer;
    [SerializeField] private GameObject handSpawner;
    [SerializeField] private GameObject projectileTarget;
    [SerializeField] private GameObject projectileAura;
    [SerializeField] private GameObject chidoriAura;
    [SerializeField] private GameObject projectileEnergyAccumulation;
    [SerializeField] private float chidoriSpeed;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        InitializeTargetAndAura();
        specialAttack3VFX.gameObject.SetActive(false);
    }

    private void InitializeTargetAndAura()
    {
        projectileTarget.transform.forward = this.transform.forward;

        projectileTarget.transform.position = this.transform.position + 30.0f * projectileTarget.transform.forward;
        
        projectileAura.gameObject.SetActive(false);
        chidoriAura.gameObject.SetActive(false);
        specialAttack3VFX.gameObject.SetActive(false);
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

    void LateUpdate()
    {
        CheckChidoriExecution();
    }

    private void CheckChidoriExecution()
    {
        if(executingChidori)
        {
            currentChidoriTimer += Time.deltaTime;
            if(currentChidoriTimer >= chidoriTimer)
            {
                if(!hasExecutedChidori)
                {
                    hasExecutedChidori = true;
                    humanPlayerAnimations.ReleaseSpecialAttack3();
                }
            }
        }
    }

    public override void ExecuteSpecialAttack1()
    {
        base.ExecuteSpecialAttack1();
        specialAttack1VFX = Instantiate(specialAttack1VFX, this.transform.position, Quaternion.identity);
        specialAttack1VFX.gameObject.SetActive(true);
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_ATTACK, new string[]{"Enemy", "Bruce", "CAN_ESCAPE"});
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{"Player", "Player", "10", "Sub"});
    }

    public void SendExecutionMessage()
    {
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.EXECUTE_ATTACK, new string[]{"Enemy", "Bruce", "10", "20"});
        StartCoroutine(RepeatSendExecutionSpecialAttack1());
    }

    private IEnumerator RepeatSendExecutionSpecialAttack1()
    {
        print(checkHitTime1);
        yield return new WaitForSeconds(checkHitTime1);
        if(specialAttack1HitCounter < specialAttack1HitNumber)
        {
            specialAttack1HitCounter++;
            SendExecutionMessage();
        }
    }

    public override IEnumerator EndSpecialAttack1()
    {
        specialAttack1HitCounter = 0;
        return base.EndSpecialAttack1();
    }

    public override void ExecuteSpecialAttack2()
    {
        base.ExecuteSpecialAttack2();
        projectileTarget.transform.forward = this.transform.forward;
        projectileTarget.transform.position = this.transform.position + 30.0f * projectileTarget.transform.forward;
        normalAura.gameObject.SetActive(false);
        // string info = PacketCreator.PrepareMessage();
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_ATTACK, new string[]{"Enemy", "Bruce", "CAN_ESCAPE"});
    }

    private void SpawnProjectileAura()
    {
        projectileAura.gameObject.SetActive(true);
    }

    private void SpawnProjectileEnergyAccumulation()
    {
        projectileEnergyAccumulation = Instantiate(projectileEnergyAccumulation, handSpawner.transform.position, Quaternion.identity);
        projectileEnergyAccumulation.gameObject.SetActive(true);
    }

    private void SpawnSpecialAttack2()
    {
        specialAttack2VFX = Instantiate(specialAttack2VFX,
                    handSpawner.transform.position,
                    Quaternion.Euler(
                                    this.transform.localRotation.eulerAngles.x,
                                    this.transform.localRotation.eulerAngles.y,
                                    this.transform.localRotation.eulerAngles.z)
                    );
        specialAttack2VFX.gameObject.SetActive(true);
        // string info = PacketCreator.PrepareMessage();
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.LAUNCH_PROJECTILE, new string[]{"Enemy", "Bruce"});
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{"Player", "Player", "15", "Sub"});
        // GetComponent<PlayerMana>().DecreaseMana(15);
    }

    public override IEnumerator EndSpecialAttack2()
    {
        yield return base.EndSpecialAttack2();
        projectileAura.gameObject.SetActive(false);
        projectileEnergyAccumulation.gameObject.SetActive(false);
        normalAura.gameObject.SetActive(true);
    }

    public override void ExcecuteSpecialAttack3()
    {
        base.ExcecuteSpecialAttack3();
        currentChidoriTimer = 0.0f;
        chidoriAura.gameObject.SetActive(true);
        
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.BEGIN_ATTACK, new string[]{"Enemy", "Bruce", "CANNOT_ESCAPE"});
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{"Player", "Player", "30", "Sub"});
    }

    private void SpawnChidori()
    {
        // specialAttack3VFX = Instantiate(specialAttack3VFX, handSpawner.transform.position, Quaternion.identity);
        specialAttack3VFX.gameObject.SetActive(true);
    }

    private void StartRunningForSpecialAttack3()
    {
        executingChidori = true;
    }

    private void ApplyChidoriDamage()
    {
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.EXECUTE_ATTACK, new string[]{"Enemy", "Bruce", "50", "10"});
        StartCoroutine(RepeatApplyChidoriDamage());
    }

    private IEnumerator RepeatApplyChidoriDamage()
    {
        print(checkHitTime3);
        yield return new WaitForSeconds(checkHitTime3);
        if(specialAttack3HitCounter < specialAttack3HitNumber)
        {
            specialAttack3HitCounter++;
            ApplyChidoriDamage();
        }
    }

    public override IEnumerator EndSpecialAttack3()
    {
        yield return base.EndSpecialAttack3();
        specialAttack3HitCounter = 0;
        executingChidori = false;
        hasExecutedChidori = false;
        chidoriAura.gameObject.SetActive(false);
    }
}
