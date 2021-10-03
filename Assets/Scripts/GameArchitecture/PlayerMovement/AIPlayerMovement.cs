using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayerMovement : MonoBehaviour
{
    protected Vector3 target;
    protected NavMeshAgent agent;
    protected AIPlayerAnimations aIPlayerAnimations;
    protected AIPlayerMessage aIPlayerMessage;
    protected float chargingTimer = 3.0f;
    protected float currentChargingTimer = 0.0f;
    private bool isEvading = false;
    protected bool isCharging = false;
    public virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        // target = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        aIPlayerAnimations = GetComponent<AIPlayerAnimations>();
        aIPlayerMessage = GetComponent<AIPlayerMessage>();
    }

    public virtual void SetTarget(Vector3 newTarget)
    {
        target = newTarget;
        agent.SetDestination(target);
        // print(target);
    }

    // Update is called once per frame
    public virtual void Move()
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
        // agent.SetDestination(target);
    }

    public virtual void Flee(Vector3 playerPosition)
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
        // agent.SetDestination(target);
    }

    public virtual void ApproachPlayer(Vector3 playerPosition)
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
        agent.SetDestination(playerPosition);
    }

    public virtual void SetFleeDestination(Vector3 playerPosition)
    {
        
    }

    public void Evade()
    {
        if(!isEvading)
        {
            print("Evading");
            isEvading = true;
            aIPlayerAnimations.Evade();
            StartCoroutine(ResetEvasion());
        }
    }

    public virtual void EndEvade()
    {
        
    }

    private IEnumerator ResetEvasion()
    {
        yield return new WaitForSeconds(2.0f);
        isEvading = false;
    }

    public virtual void StartEnergyCharge()
    {
        currentChargingTimer += Time.deltaTime;
    }

    public virtual void StopEnergyCharge()
    {
        aIPlayerAnimations.ChargingEnergy(false);
    }
}
