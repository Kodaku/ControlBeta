using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
    private HumanPlayerMovement playerMovement;
    private HumanPlayerAttack playerAttack;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        playerMovement = GetComponent<HumanPlayerMovement>();
        playerAttack = GetComponent<HumanPlayerAttack>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        switch(currentState)
        {
            case PlayerStates.IDLE: case PlayerStates.MOVE_LEFT: case PlayerStates.MOVE_RIGHT:
            case PlayerStates.START_ENERGY_CHARGE: case PlayerStates.STOP_ENERGY_CHARGE: case PlayerStates.MOVE_UP: case PlayerStates.ESCAPE: case PlayerStates.GUARD_BREAK: case PlayerStates.GUARD_BREAK_REACTION: case PlayerStates.DAMAGE: case PlayerStates.SURPRISE: case PlayerStates.SUPER_DAMAGE: case PlayerStates.DEAD:
            {
                playerMovement.Move(currentState, optionalValues);
                break;
            }
            case PlayerStates.PUNCH: case PlayerStates.STRONG_PUNCH: case PlayerStates.SPECIAL_1:
            case PlayerStates.SPECIAL_2: case PlayerStates.FINAL_ATTACK:
            {
                //attack
                playerAttack.Attack(currentState);
                break;
            }
        }
    }

    public override void TranslateForEvade()
    {
        playerMovement.TranslateEvade();
    }
}
