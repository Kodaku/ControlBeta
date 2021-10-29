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
    private Vector3 playerPosition;
    // private AIPlayerMovement aIPlayerMovement;
    private AIPlayerAnimations aIPlayerAnimations;
    private NavMeshAgent agent;
    private Perceivable<Vector3> positionSensor;
    private Perceivable<float>[] updatableSensors = new Perceivable<float>[2];
    private bool animationCompleted = false;
    private float gap = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        // aIPlayerMovement = GetComponent<AIPlayerMovement>();
        aIPlayerAnimations = GetComponent<AIPlayerAnimations>();
        agent = GetComponent<NavMeshAgent>();
        positionSensor = GetComponentInChildren<PositionSensor>();
        updatableSensors[(int)UpdatableIndices.HEALTH] = GetComponentInChildren<HealthSensor>();
        updatableSensors[(int)UpdatableIndices.MANA] = GetComponentInChildren<ManaSensor>();

        chargingAura.gameObject.SetActive(false);
    }

    public void SetAnimationCompleted()
    {
        this.animationCompleted = true;
    }

    //WANDER SECTION

    [Task]
    public void PickRandomDestination()
    {
        float randRadius = Random.Range(startDistanceRadius, distanceRadius) + startDistanceRadius;
        playerPosition = positionSensor.GetMeasure();

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
        playerPosition = positionSensor.GetMeasure();
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
}
