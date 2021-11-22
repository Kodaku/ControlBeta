using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    private HumanPlayerAnimations humanPlayerAnimations;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        humanPlayerAnimations = GetComponent<HumanPlayerAnimations>();
        // agent.SetDestination(target.transform.position);
        // target = GameObject.FindGameObjectWithTag(targetTag);
    }

    public void FollowTarget()
    {
        agent.SetDestination(target.transform.position);
        print(agent.destination);
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            // target = GameObject.FindGameObjectWithTag(targetTag);
            humanPlayerAnimations.Walk(false);
        }
        else
        {
            if(agent.velocity.magnitude > 0.1f)
            {
                humanPlayerAnimations.Walk(true);
            }
            else
            {
                humanPlayerAnimations.Walk(false);
            }
            humanPlayerAnimations.SetBlendSpeed(agent.velocity.magnitude);
        }
    }
}
