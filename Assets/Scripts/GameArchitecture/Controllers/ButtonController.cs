using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{
    private Player player;
    private float rotSpeed = 110.0f;
    private int releaseCount = 0;
    private bool isPowerActive = false;
    public bool isGary;
    public bool isErick;

    void Awake()
    {
        player = GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ProcessInput()
    {
        if(!PauseMenu.GameIsPaused && !DialogueDirector.IsShowingDialogue && !GameManager.IsPreparingFight && !GameManager.IsPlayerDead)
        {
            float rotationDirection = Input.GetAxisRaw("Horizontal");
            float translationDirection = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(rotationDirection, 0.0f, translationDirection).normalized;
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            if(direction.magnitude > 0.0f)
            {
                player.SetCurrentState(PlayerStates.MOVE_UP, new float[]{direction.x, direction.y, direction.z});
            }
            else if(translationDirection <= 0.0f && rotationDirection == 0.0f)
            {
                player.SetCurrentState(PlayerStates.IDLE);
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                player.SetCurrentState(PlayerStates.ESCAPE);
            }
            if(Input.GetKeyDown(KeyCode.G))
            {
                player.SetCurrentState(PlayerStates.GUARD_BREAK);
            }
            if(Input.GetKey(KeyCode.Space) && isPowerActive)
            {
                //Charge
                releaseCount = 0;
                player.SetCurrentState(PlayerStates.START_ENERGY_CHARGE);
            }
            else if(Input.GetKeyUp(KeyCode.Space) && releaseCount == 0 && isPowerActive)
            {
                releaseCount = 1;
                player.SetCurrentState(PlayerStates.STOP_ENERGY_CHARGE);
            }

            if(Input.GetKeyDown(KeyCode.Z))
            {
                //Punch
                player.SetCurrentState(PlayerStates.PUNCH);
            }

            if(Input.GetKeyDown(KeyCode.X))
            {
                //Strong punch
                player.SetCurrentState(PlayerStates.STRONG_PUNCH);
            }

            if(Input.GetKeyDown(KeyCode.Q) && isPowerActive)
            {
                //First special attack
                player.SetCurrentState(PlayerStates.SPECIAL_1);
            }

            if(Input.GetKeyDown(KeyCode.P) && isPowerActive)
            {
                //Second special attack
                player.SetCurrentState(PlayerStates.SPECIAL_2);
            }

            if(Input.GetKeyDown(KeyCode.C) && isPowerActive)
            {
                //Final attack
                player.SetCurrentState(PlayerStates.FINAL_ATTACK);
            }
        }
        else if(GameManager.IsPlayerDead)
        {
            player.SetCurrentState(PlayerStates.DEAD);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "SceneTrigger")
            player.SetCurrentState(PlayerStates.IDLE);
    }

    public void SendUpdateRequest(UpdatableIndices index, int amount)
    {
        player.UpdateUpdatable(index, amount);
    }

    public void TranslateEvade()
    {
        player.TranslateForEvade();
    }

    public void ApplyDamage()
    {
        player.SetCurrentState(PlayerStates.DAMAGE);
    }

    public void ApplyGuardBreakReaction()
    {
        player.SetCurrentState(PlayerStates.GUARD_BREAK_REACTION);
    }

    public void Surprise()
    {
        player.SetCurrentState(PlayerStates.SURPRISE);
    }

    public void SuperHit()
    {
        player.SetCurrentState(PlayerStates.SUPER_DAMAGE);
    }

    public void ActivatePower()
    {
        isPowerActive = true;
    }

    public void DeactivatePower()
    {
        isPowerActive = false;
    }
}
