using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject chargingAura;
    public float speed = 6.0f;
    float currentSpeed = 0.0f;
    public float rotSpeed = 110.0f;
    private float damageTimer = 1.0f;
    private float currentDamageTimer = 0.0f;
    private bool isMoving = false;
    private bool reverseMovement = false;
    private bool isCharging = false;
    private bool canEvade = false;
    private bool canApplyDamage = true;
    private HumanPlayerAnimations humanPlayerAnimations;
    private HumanPlayerMessage humanPlayerMessage;
    // Start is called before the first frame update
    void Start()
    {
        humanPlayerAnimations = GetComponent<HumanPlayerAnimations>();
        humanPlayerMessage = GetComponent<HumanPlayerMessage>();
        chargingAura.gameObject.SetActive(false);
    }

    void Update()
    {
        currentDamageTimer += Time.deltaTime;
        if(currentDamageTimer >= damageTimer)
        {
            currentDamageTimer = 0.0f;
            canApplyDamage = true;
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     Rotate();
    // }
    // void FixedUpdate()
    // {
    //     Move();
    //     Jump();
    // }

    public void Move(PlayerStates currentState, float[] optionalValues)
    {
        if(currentState == PlayerStates.START_ENERGY_CHARGE)
        {
            chargingAura.gameObject.SetActive(true);
            StartEnergyCharge();
            humanPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{"Player", "Player", "1", "Add"});
        }
        if(currentState == PlayerStates.STOP_ENERGY_CHARGE)
        {
            chargingAura.gameObject.SetActive(false);
            StopEnergyCharging();
        }
        if(currentState == PlayerStates.MOVE_LEFT || currentState == PlayerStates.MOVE_RIGHT)
        {
            Rotate(optionalValues[0]); //rotation direction
        }
        if(currentState == PlayerStates.MOVE_UP)
        {
            WalkAndRun(optionalValues[0]); //translation direction
            Rotate(optionalValues[1]); //rotation direction
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

    private void WalkAndRun(float translationDirection)
    {
        // if(!isMoving)
        //     translationDirection = AdjustTranslationDirection(translationDirection);
        if(translationDirection > 0.0f && !isCharging)
        {
            translationDirection *= Time.fixedDeltaTime * speed;
            currentSpeed += translationDirection;
            currentSpeed = Mathf.Clamp(currentSpeed, 0.0f, 6.0f);
            isMoving = true;
            if(currentSpeed > 5.0f)
                humanPlayerAnimations.FastRunning(true);
            else
            {
                humanPlayerAnimations.Walk(true);
                humanPlayerAnimations.FastRunning(false);
            }

            humanPlayerAnimations.SetBlendSpeed(Mathf.Abs(currentSpeed));

            if(!reverseMovement)
                this.transform.Translate(0, 0, translationDirection);
            else
                this.transform.Translate(0, 0, -translationDirection);
        }
        if(translationDirection > 0.0f && !isCharging)
        {
            
        }
        // else
        // {
        //     isMoving = false;
        //     humanPlayerAnimations.Walk(false);
        //     currentSpeed = 0.0f;
        // }
    }

    private void Idle()
    {
        isMoving = false;
        humanPlayerAnimations.Walk(false);
        humanPlayerAnimations.FastRunning(false);
        currentSpeed = 0.0f;
    }

    private void StartEnergyCharge()
    {
        isCharging = true;
        isMoving = false;
        humanPlayerAnimations.Walk(false);
        humanPlayerAnimations.ChargingEnergy(true);
    }

    private void StopEnergyCharging()
    {
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
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.EVADE, new string[]{"Player", "Player"});
    }

    public void TranslateEvade()
    {
        canEvade = false;
    }
    private void GuardBreak()
    {
        humanPlayerAnimations.GuardBreak();
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.GUARD_BREAK, new string[]{"Enemy", "Bruce"});
        humanPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{"Player", "Player", "750", "Sub"});
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
    
    private IEnumerator ResetJumping()
    {
        yield return new WaitForSeconds(0.4f);
        isCharging = false;
    }

    private float AdjustTranslationDirection(float translationDirection)
    {
        float newTranslation = translationDirection;
        float yRot = transform.localRotation.eulerAngles.y;
        float zScale = transform.localScale.z;
        if(translationDirection < 0.0f)
        {
            if((yRot >= 0.0f && yRot < 90.0f) || (yRot >= 270.0f && yRot <= 360.0f))
            {
                reverseMovement = false;
                ChangeLocalScale(-1);
            }
            else
            {
                reverseMovement = true;
                ChangeLocalScale(1);
            }
        }
        else if(translationDirection > 0.0f)
        {
            if((yRot >= 90.0f && yRot < 270.0f))
            {
                reverseMovement = true;
                ChangeLocalScale(-1);
            }
            else
            {
                reverseMovement = false;
                ChangeLocalScale(1);
            }
        }
        return newTranslation;
    }

    private void ChangeLocalScale(float newScale)
    {
        Vector3 temp = this.transform.localScale;
        temp.z = newScale;
        this.transform.localScale = temp;
    }

    public void GetStrongHit()
    {
        transform.Translate(0,0,-2.0f);
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
