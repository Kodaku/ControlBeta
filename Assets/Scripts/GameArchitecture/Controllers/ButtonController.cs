using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private Player player;
    private float rotSpeed = 110.0f;
    private int releaseCount = 0;

    void Awake()
    {
        player = GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rotationDirection = Input.GetAxisRaw("Horizontal") * rotSpeed;
        float translationDirection = Input.GetAxisRaw("Vertical");
        //Rotate the player
        if(rotationDirection > 0.0f)
        {
            //Rotate right
            player.SetCurrentState(PlayerStates.MOVE_RIGHT, new float[]{rotationDirection});
        }
        else if(rotationDirection < 0.0f)
        {
            //Rotate left
            player.SetCurrentState(PlayerStates.MOVE_LEFT, new float[]{rotationDirection});

        }
        if(translationDirection > 0.0f && Input.GetKey(KeyCode.B))
        {
            player.SetCurrentState(PlayerStates.MOVE_UP, new float[]{translationDirection, rotationDirection});
        }
        //Move the player
        else if(translationDirection > 0.0f)
        {
            //Move up
            player.SetCurrentState(PlayerStates.MOVE_UP, new float[]{translationDirection, rotationDirection});
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
        if(Input.GetKey(KeyCode.Space))
        {
            //Charge
            releaseCount = 0;
            player.SetCurrentState(PlayerStates.START_ENERGY_CHARGE);
        }
        else if(Input.GetKeyUp(KeyCode.Space) && releaseCount == 0)
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

        if(Input.GetKeyDown(KeyCode.Q))
        {
            //First special attack
            player.SetCurrentState(PlayerStates.SPECIAL_1);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            //Second special attack
            player.SetCurrentState(PlayerStates.SPECIAL_2);
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            //Final attack
            player.SetCurrentState(PlayerStates.FINAL_ATTACK);
        }

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
}
