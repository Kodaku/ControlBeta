using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecisionMaker : MonoBehaviour
{
    [SerializeField] private int punchCount = 0;
    [SerializeField] private float specialAttackTimer = 5.0f;
    private AIPlayer player;
    private Perceivable<Vector3> positionSensor;
    private Perceivable<float>[] updatableSensors = new Perceivable<float>[2];
    private Vector3 playerPosition;
    private bool executedSpecialAttack = false;
    private bool executedFinalAttack = false;
    private bool isReachingRandomDestination = false;
    private bool isActionFinished = false;
    private bool hasEvaded = false;
    private float currentSpecialAttackTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AIPlayer>();
        positionSensor = GetComponentInChildren<PositionSensor>();
        updatableSensors[(int)UpdatableIndices.HEALTH] = GetComponentInChildren<HealthSensor>();
        updatableSensors[(int)UpdatableIndices.MANA] = GetComponentInChildren<ManaSensor>();

        currentSpecialAttackTimer = specialAttackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpecialAttackTimer += Time.deltaTime;
        playerPosition = positionSensor.GetMeasure();
        float agentHealth = updatableSensors[(int)UpdatableIndices.HEALTH].GetMeasure();
        float agentMana = updatableSensors[(int)UpdatableIndices.MANA].GetMeasure();
        // print(agentMana);
        if(isActionFinished)
        {
            if(agentMana <= 500)
            {
                isActionFinished = false;
                print("Charge");
                player.SetCurrentState(PlayerStates.START_ENERGY_CHARGE);
            }
            else if(agentHealth <= 50 && Vector3.Distance(this.transform.position, playerPosition) < 1.0f)
            {
                isActionFinished = false;
                print("Escape");
                player.SetCurrentState(PlayerStates.ESCAPE);
            }
            else if(agentHealth <= 50 && agentMana == 1000 && currentSpecialAttackTimer >= specialAttackTimer)
            {
                isActionFinished = false;
                // print("Final Attack");
                player.SetCurrentState(PlayerStates.FINAL_ATTACK);
            }
            else if(agentHealth <= 50 && agentMana > 750 && agentMana < 1000 && currentSpecialAttackTimer >= specialAttackTimer)
            {
                isActionFinished = false;
                // print("Special Attack 2");
                player.SetCurrentState(PlayerStates.SPECIAL_2);
            }
            else if(agentHealth <= 50 && agentMana > 500 && agentMana <= 750 && currentSpecialAttackTimer >= specialAttackTimer)
            {
                isActionFinished = false;
                // print("Special Attack 1");
                player.SetCurrentState(PlayerStates.SPECIAL_1);
            }
            else if(agentHealth > 50 && Vector3.Distance(this.transform.position, playerPosition) < 1.0f)
            {
                isActionFinished = false;
                // print("Punch");
                player.SetCurrentState(PlayerStates.PUNCH);
            }
            else if(agentHealth > 50 && Vector3.Distance(this.transform.position, playerPosition) >= 1.0f)
            {
                isActionFinished = false;
                // print("Approach player");
                // print(playerPosition);
                player.SetCurrentState(PlayerStates.APPROACH_PLAYER, new float[]{playerPosition.x, playerPosition.y, playerPosition.z});
            }
            else
            {
                print("Idle");
                isActionFinished = false;
                player.SetCurrentState(PlayerStates.IDLE);
            }
        }
        player.NextMove();
    }

    public void ResetCurrentSpecialAttackTimer()
    {
        currentSpecialAttackTimer = 0.0f;
    }

    public void SendUpdateRequest(UpdatableIndices index, int amount)
    {
        // print("Update mana");
        player.UpdateUpdatable(index, amount);
    }

    public void CanExecuteSpecialAttack(bool canExecute)
    {
        executedSpecialAttack = canExecute;
    }

    public void CanDecideNextMove()
    {
        isActionFinished = true;
    }

    public void FleeFromPlayer()
    {
        isActionFinished = false;
        print("Flee after evade");
        player.SetCurrentState(PlayerStates.FLEE, new float[]{playerPosition.x, playerPosition.y, playerPosition.z});
    }

    public void ApplyDamage()
    {
        isActionFinished = false;
        player.SetCurrentState(PlayerStates.DAMAGE);
    }
}
