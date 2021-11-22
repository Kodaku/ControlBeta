using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using UnityEngine.AI;

public class AIBehaviourTree : MonoBehaviour
{
    [SerializeField] private float startDistanceRadius = 10.0f;
    [SerializeField] private float distanceRadius = 20.0f;
    [SerializeField] private GameObject chargingAura;
    // private Vector3 playerPosition;
    // private AIPlayerMovement aIPlayerMovement;
    private AIPlayerAnimations aIPlayerAnimations;
    private AIPlayerAttack aIPlayerAttack;
    private NavMeshAgent agent;
    private Perceivable<Vector3> positionSensor;
    private Perceivable<float>[] updatableSensors = new Perceivable<float>[2];
    private Updatable[] updatables = new Updatable[2];
    private bool animationCompleted = true;
    private bool isPowerActive = false;
    private bool isReacting = false;
    private bool isDamage = false;
    private bool isGuardBreak = false;
    private bool isDead = false;
    public bool isLast;
    public bool isLastInWave;
    public bool isBoos;
    private float gap = 5.0f;
    private float currentSpecialAttackTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        // aIPlayerMovement = GetComponent<AIPlayerMovement>();
        aIPlayerAnimations = GetComponent<AIPlayerAnimations>();
        aIPlayerAttack = GetComponent<AIPlayerAttack>();
        agent = GetComponent<NavMeshAgent>();
        positionSensor = GetComponentInChildren<PositionSensor>();
        // print(positionSensor);
        updatableSensors[(int)UpdatableIndices.HEALTH] = GetComponentInChildren<HealthSensor>();
        updatableSensors[(int)UpdatableIndices.MANA] = GetComponentInChildren<ManaSensor>();
        if(!isBoos)
        {
            updatables[(int)UpdatableIndices.HEALTH] = GetComponent<AIHealthBar>();
        }
        else
        {
            updatables[(int)UpdatableIndices.HEALTH] = GetComponent<PlayerHealth>();
        }
        updatables[(int)UpdatableIndices.MANA] = GetComponent<PlayerMana>();
        if(chargingAura)
        {
            chargingAura.gameObject.SetActive(false);
        }
    }

    public void SetAnimationCompleted()
    {
        animationCompleted = true;
    }

    //REPLACE FROM DECISION MAKER
    public void SendUpdateRequest(UpdatableIndices index, int amount)
    {
        // print("Update mana");
        updatables[(int)index].AddQuantity(amount);
        float health = updatableSensors[(int)UpdatableIndices.HEALTH].GetMeasure();
        if(health <= 0.0f)
        {
            isDead = true;
        }
    }

    public void ResetCurrentSpecialAttackTimer()
    {
        currentSpecialAttackTimer = 0.0f;
    }

    public void ApplyDamage()
    {
        isReacting = true;
        isDamage = true;
    }

    public void ApplyGuardBreakReaction()
    {
        isReacting = true;
        isGuardBreak = true;
    }

    public void ResetTarget()
    {
        positionSensor = GetComponentInChildren<PositionSensor>();
        positionSensor.ResetTarget();
    }


    //WANDER SECTION

    [Task]
    public void PickRandomDestination()
    {
        float randRadius = Random.Range(startDistanceRadius, distanceRadius) + startDistanceRadius;
        Vector3 playerPosition = positionSensor.GetMeasure();

        Vector3 randDir = Random.insideUnitSphere * randRadius; //getting a random direction
        randDir += playerPosition;
        randDir.y = transform.position.y;

        NavMeshHit navMeshHit;

        NavMesh.SamplePosition(randDir, out navMeshHit, randRadius, -1); //if it goes outside the navigashonable area it will stop and calculate another direction
        agent.SetDestination(navMeshHit.position);
        Task.current.Succeed();
    }

    [Task]
    public void MoveToDestination()
    {
        if(Task.isInspected)
        {
            Task.current.debugInfo = string.Format("t={0:0.00}", Time.time);
        }
        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            aIPlayerAnimations.Walk(false);
            Task.current.Succeed();
        }
        else
        {
            if(agent.velocity.magnitude > 0.1f)
            {
                aIPlayerAnimations.Walk(true);
            }
            else
            {
                aIPlayerAnimations.Walk(false);
            }
            aIPlayerAnimations.SetBlendSpeed(agent.velocity.magnitude);
        }
    }

    //FLEE SECTION
    [Task]
    public bool HealthLessThan(int dangerValue)
    {
        float currentHealth = updatableSensors[(int)UpdatableIndices.HEALTH].GetMeasure();
        return currentHealth < dangerValue;
    }

    [Task]
    public bool InDanger(int minDistance)
    {
        Vector3 playerPosition = positionSensor.GetMeasure();
        Vector3 distance = playerPosition - this.transform.position;
        return (distance.magnitude < minDistance);
    }

    [Task]
    public void Escape()
    {
        if(Task.current.isStarting)
        {
            aIPlayerAnimations.Evade();
        }

        if(animationCompleted)
        {
            animationCompleted = false;
            Task.current.Succeed();
        }
    }

    [Task]
    public void PickFleeDestination()
    {
        Vector3 playerPosition = positionSensor.GetMeasure();
        float currentX = this.transform.position.x;
        float currentRadius = Vector3.Distance(this.transform.position, playerPosition);
        float destinationX = currentX - startDistanceRadius + currentRadius - Random.Range(0, gap);
        float destinationZ = this.transform.position.z;
        Vector3 fleeDest = new Vector3(destinationX, this.transform.position.y, destinationZ);
        agent.SetDestination(fleeDest);
        Task.current.Succeed();
    }

    // CHARGE SECTION

    [Task]
    public void Charge()
    {
        float currentMana = updatableSensors[(int)UpdatableIndices.MANA].GetMeasure();
        if(Task.current.isStarting)
        {
            chargingAura.gameObject.SetActive(true);
        }
        aIPlayerAnimations.ChargingEnergy(true);
        if(currentMana > 750)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    public void EndCharge()
    {
        aIPlayerAnimations.ChargingEnergy(false);
        chargingAura.gameObject.SetActive(false);
        Task.current.Succeed();
    }

    [Task]
    public bool IsPlayerDead()
    {
        return GameManager.IsPlayerDead;
    }

    //IDLE SECTION
    [Task]
    public void Idle()
    {
        aIPlayerAnimations.Walk(false);
        Task.current.Succeed();
    }

    //SIMPLE FIGHT SECTION
    [Task]
    public bool IsFightStarted()
    {
        return GameManager.IsFightStarted;
    }
    [Task]
    public bool IsFightEnded()
    {
        return GameManager.IsFightEnded;
    }
    [Task]
    public bool ReactAnimationCompleted()
    {
        return animationCompleted;
    }

    [Task]
    public void SearchPlayer()
    {
        Vector3 playerPosition = positionSensor.GetMeasure();
        agent.SetDestination(playerPosition);
        Task.current.Succeed();
    }

    [Task]
    public bool ReachedPlayer()
    {
        return agent.remainingDistance < 1.0f;
    }
    [Task]
    public void Attack()
    {
        aIPlayerAttack.Attack();
        Task.current.Succeed();
    }

    //REACTION SECTION
    [Task]
    public void React()
    {
        isReacting = false;
        if(Task.current.isStarting)
        {
            if(isDamage)
            {
                isDamage = false;
                aIPlayerAnimations.Damage();
            }
            else if(isGuardBreak)
            {
                isGuardBreak = false;
                aIPlayerAnimations.GuardBreakReaction();
            }
        }
        if(animationCompleted)
            Task.current.Succeed();
    }

    [Task]
    public bool IsReacting()
    {
        return isReacting;
    }

    //DEATH SECTION
    [Task]
    public bool IsDead()
    {
        return isDead;
    }

    [Task]
    public void Die()
    {
        aIPlayerAnimations.Die();
        if(isLast)
        {
            print("Is Last");
            GameManager.IsPreparingFight = false;
            GameManager.IsFightStarted = false;
            GameManager.IsFightEnded = true;
            GameManager.ShowWinLoseScreen();
            GameManager.ShowWinLoseText(true);
        }
        else if(isLastInWave)
        {
            GameManager.SpawnWave();
        }
        Task.current.Succeed();
    }
    [Task]
    public void Disappear()
    {
        this.gameObject.SetActive(false);
    }
}
