using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    private AIPlayerMovement playerMovement;
    private AIPlayerAttack playerAttack;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        currentState = PlayerStates.FLEE;
        playerMovement = GetComponent<AIPlayerMovement>();
        playerAttack = GetComponent<AIPlayerAttack>();
    }

    // Update is called once per frame
    public void NextMove()
    {
        switch(currentState)
        {
            case PlayerStates.FLEE:
            {
                // print("FLEE");
                if(optionalValues != null)
                {
                    float playerX = optionalValues[0];
                    float playerY = optionalValues[1];
                    float playerZ = optionalValues[2];
                    playerMovement.Flee(new Vector3(playerX, playerY, playerZ));
                }
                else
                {
                    playerMovement.Flee(new Vector3(0.0f, 0.0f, 0.0f));
                }
                break;
            }
            case PlayerStates.IDLE:
            {
                // print("IDLE");
                playerMovement.Move();
                break;
            }
            case PlayerStates.ESCAPE:
            {
                playerMovement.Evade();
                break;
            }
            case PlayerStates.START_ENERGY_CHARGE:
            {
                playerMovement.StartEnergyCharge();
                break;
            }
            case PlayerStates.APPROACH_PLAYER:
            {
                // print("APPROACH");
                float playerX = optionalValues[0];
                float playerY = optionalValues[1];
                float playerZ = optionalValues[2];
                playerMovement.ApproachPlayer(new Vector3(playerX, playerY, playerZ));
                break;
            }
            case PlayerStates.DAMAGE:
            {
                playerMovement.Damage();
                break;
            }
            case PlayerStates.PUNCH: case PlayerStates.STRONG_PUNCH:
            {
                //attack
                playerAttack.Attack();
                break;
            }
            case PlayerStates.SPECIAL_1:
            case PlayerStates.SPECIAL_2: case PlayerStates.FINAL_ATTACK:
            {
                // print("Special Attack from AI Player");
                playerAttack.SpecialAttack(currentState);
                break;
            }
        }
    }

    public override void UpdateUpdatable(UpdatableIndices index, int amount)
    {
        base.UpdateUpdatable(index, amount);
    }
}
