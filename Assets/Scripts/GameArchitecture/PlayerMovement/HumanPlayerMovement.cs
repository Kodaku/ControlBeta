using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayerMovement : MonoBehaviour
{
    [SerializeField] protected GameObject chargingAura;
    public CharacterController controller;
    public Transform cam;
    public float speed = 10.0f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    float currentSpeed = 0.0f;
    public float rotSpeed = 110.0f;
    private float damageTimer = 1.0f;
    private float currentDamageTimer = 0.0f;
    private bool isMoving = false;
    private bool reverseMovement = false;
    private bool isCharging = false;
    private bool canEvade = false;
    private bool canApplyDamage = true;
    private bool isAnimationComplete = true;
    protected HumanPlayerAnimations humanPlayerAnimations;
    protected HumanPlayerMessage humanPlayerMessage;
    // Start is called before the first frame update
    public virtual void Start()
    {
        humanPlayerAnimations = GetComponent<HumanPlayerAnimations>();
        humanPlayerMessage = GetComponent<HumanPlayerMessage>();
        chargingAura.gameObject.SetActive(false);
    }

    public virtual void Update()
    {
        currentDamageTimer += Time.deltaTime;
        if(currentDamageTimer >= damageTimer)
        {
            currentDamageTimer = 0.0f;
            canApplyDamage = true;
        }
    }

    public virtual void Move(PlayerStates currentState, float[] optionalValues)
    {
        if(currentState == PlayerStates.START_ENERGY_CHARGE)
        {
            StartEnergyCharge();
        }
        if(currentState == PlayerStates.STOP_ENERGY_CHARGE)
        {
            StopEnergyCharging();
        }
        if(currentState == PlayerStates.MOVE_LEFT || currentState == PlayerStates.MOVE_RIGHT)
        {
            Rotate(optionalValues[0]); //rotation direction
        }
        if(currentState == PlayerStates.MOVE_UP)
        {
            Vector3 direction = new Vector3(optionalValues[0], optionalValues[1], optionalValues[2]);
            WalkAndRun(direction); //translation direction
        }
        if(currentState == PlayerStates.IDLE)
        {
            Idle();
        }
        if(currentState == PlayerStates.ESCAPE)
        {
            //Escape
            Evade();
        }
        if(currentState == PlayerStates.GUARD_BREAK)
        {
            //guard break
            GuardBreak();
        }
        if(currentState == PlayerStates.GUARD_BREAK_REACTION)
        {
            //guard break reaction
            GuardBreakReaction();
        }
        if(currentState == PlayerStates.DAMAGE)
        {
            Damage();
        }
        if(currentState == PlayerStates.SURPRISE)
        {
            Surprise();
        }
        if(currentState == PlayerStates.SUPER_DAMAGE)
        {
            SuperHit();
        }
        if(currentState == PlayerStates.DEAD)
        {
            Die();
        }
        if(canEvade)
        {
            float newPos = speed * Time.deltaTime;
            this.transform.Translate(0.0f, 0.0f, newPos);
            // canEvade = false;
        }
    }

    private void Rotate(float rotationDirection)
    {
        rotationDirection *= Time.deltaTime;
        this.transform.Rotate(0, rotationDirection, 0);
    }

    public virtual void SendMessageFromMovement(MessageTypes messageTypes, string[] info)
    {

    }

    public virtual void ActivateAura()
    {
        
    }

    private void WalkAndRun(Vector3 direction)
    {
        if(!isCharging)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            Vector3 moveDir = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            humanPlayerAnimations.Walk(true);
            humanPlayerAnimations.SetBlendSpeed(Mathf.Abs(speed));
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void Idle()
    {
        isMoving = false;
        humanPlayerAnimations.Walk(false);
        humanPlayerAnimations.FastRunning(false);
        currentSpeed = 0.0f;
    }

    public virtual void StartEnergyCharge()
    {
        chargingAura.gameObject.SetActive(true);
        isCharging = true;
        isMoving = false;
        humanPlayerAnimations.Walk(false);
        humanPlayerAnimations.ChargingEnergy(true);
    }

    private void StopEnergyCharging()
    {
        chargingAura.gameObject.SetActive(false);
        isCharging = false;
        humanPlayerAnimations.ChargingEnergy(false);
    }

    private void Evade()
    {
        humanPlayerAnimations.Evade();
        StartCoroutine(CanEvade());
    }

    private IEnumerator CanEvade()
    {
        yield return new WaitForSeconds(0.5f);
        canEvade = true;
    }

    private void ExecuteEvade()
    {
        // print("Evade");
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.EVADE, new string[]{gameObject.tag, gameObject.name});
    }

    public void TranslateEvade()
    {
        canEvade = false;
    }
    private void GuardBreak()
    {
        humanPlayerAnimations.GuardBreak();
        foreach(string targetName in SpecialAttackTargetManager.targetNames)
        {
            humanPlayerMessage.PrepareAndSendMessage(MessageTypes.GUARD_BREAK, new string[]{"Enemy", targetName});
        }
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{gameObject.tag, gameObject.name, "750", "Sub"});
    }

    private void GuardBreakReaction()
    {
        humanPlayerAnimations.GuardBreakReaction();
    }

    private void Damage()
    {
        if(canApplyDamage)
        {
            humanPlayerAnimations.Damage();
            canApplyDamage = false;
        }
    }

    private void Surprise()
    {
        humanPlayerAnimations.Surprise(true);
    }

    private void SuperHit()
    {
        humanPlayerAnimations.SuperHit();
    }
    
    private IEnumerator ResetJumping()
    {
        yield return new WaitForSeconds(0.4f);
        isCharging = false;
    }

    public void GetStrongHit()
    {
        transform.Translate(0,0,-2.0f);
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    private void Die()
    {
        if(isAnimationComplete)
        {
            isAnimationComplete = false;
            humanPlayerAnimations.Die();
        }
    }

    public void EndAnimation()
    {
        isAnimationComplete = true;
    }
}
