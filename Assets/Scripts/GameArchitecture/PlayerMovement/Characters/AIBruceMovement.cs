using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBruceMovement : AIPlayerMovement
{
    [SerializeField] private float startDistanceRadius = 10.0f;
    [SerializeField] private float distanceRadius = 20.0f;
    [SerializeField] private float gap = 5.0f;
    [SerializeField] private GameObject chargingAura;
    private Vector3 playerPosition;
    private Vector3 startPoint;
    private bool reachedFleePoint = false;
    private bool reachedWanderPoint = true;
    private float notMovingTimer = 3.0f;
    private float currentNotMovingTimer = 0.0f;


    public override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        chargingAura.gameObject.SetActive(false);
        SetFleeDestination(playerPosition);
    }

    public override void SetTarget(Vector3 newTarget)
    {
        base.SetTarget(newTarget);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Move()
    {
        base.Move();
        //for emergency purpose
        if(!reachedWanderPoint)
        {
            // print(currentNotMovingTimer);
            currentNotMovingTimer += Time.deltaTime;
            if(currentNotMovingTimer > notMovingTimer)
            {
                // print("Reset random destination");
                reachedWanderPoint = true;
                currentNotMovingTimer = 0.0f;
            }
        }
        if(reachedWanderPoint)
        {
            SetNewRandomDestination();
        }
        if(Vector3.Distance(this.transform.position, target) < 1.0f)
        {
            reachedWanderPoint = true;
            aIPlayerMessage.PrepareAndSendMessage(MessageTypes.ACTION_TERMINATED, new string[]{"Enemy", "Bruce"});
        }
    }

    public override void Flee(Vector3 playerPosition)
    {
        base.Flee(playerPosition);
        if(reachedFleePoint)
        {
            // print("Set new flee destination");
            SetFleeDestination(playerPosition);
        }
        if(Vector3.Distance(this.transform.position, startPoint) < 1.0f)
        {
            reachedFleePoint = true;
            // print("Reached flee point");
            aIPlayerMessage.PrepareAndSendMessage(MessageTypes.ACTION_TERMINATED, new string[]{"Enemy", "Bruce"});
        }
    }

    public override void ApproachPlayer(Vector3 playerPosition)
    {
        base.ApproachPlayer(playerPosition);
        // print(playerPosition);
        notMovingTimer += Time.deltaTime;
        if(notMovingTimer >= currentNotMovingTimer)
        {
            notMovingTimer = 0.0f;
            aIPlayerMessage.PrepareAndSendMessage(MessageTypes.ACTION_TERMINATED, new string[]{"Enemy", "Bruce"});
        }
        if(Vector3.Distance(this.transform.position, playerPosition) < 1.0f)
        {
            reachedFleePoint = true;
            // print("Reached player");
            aIPlayerMessage.PrepareAndSendMessage(MessageTypes.ACTION_TERMINATED, new string[]{"Enemy", "Bruce"});
        }
    }

    public override void SetNewRandomDestination()
    {
        reachedWanderPoint = false;
        float randRadius = Random.Range(startDistanceRadius, distanceRadius) + startDistanceRadius;

        Vector3 randDir = Random.insideUnitSphere * randRadius; //getting a random direction
        randDir += playerPosition;
        randDir.y = transform.position.y;

        NavMeshHit navMeshHit;

        NavMesh.SamplePosition(randDir, out navMeshHit, randRadius, -1); //if it goes outside the navigashonable area it will stop and calculate another direction
        SetTarget(navMeshHit.position);
    }

    public override void SetFleeDestination(Vector3 playerPosition)
    {
        base.SetFleeDestination(playerPosition);
        reachedFleePoint = false;
        float currentX = this.transform.position.x;
        float currentRadius = Vector3.Distance(this.transform.position, playerPosition);
        float destinationX = currentX - startDistanceRadius + currentRadius - Random.Range(0, gap);
        float destinationZ = this.transform.position.z;
        startPoint = new Vector3(destinationX, this.transform.position.y, destinationZ);
        SetTarget(startPoint);
    }

    public override void EndEvade()
    {
        base.EndEvade();
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.EVADE, new string[]{"Enemy", "Bruce"});
    }

    public override void StartEnergyCharge()
    {
        base.StartEnergyCharge();
        if(currentChargingTimer >= chargingTimer)
        {
            StopEnergyCharge();
            currentChargingTimer = 0.0f;
        }
        else
        {
            aIPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{"Enemy", "Bruce", "1", "Add"});
            aIPlayerAnimations.ChargingEnergy(true);
            chargingAura.gameObject.SetActive(true);
        }
    }

    public override void StopEnergyCharge()
    {
        base.StopEnergyCharge();
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.ACTION_TERMINATED, new string[]{"Enemy", "Bruce"});
        chargingAura.gameObject.SetActive(false);
    }

    public override void EndDamage()
    {
        base.EndDamage();
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.ACTION_TERMINATED, new string[]{"Enemy", "Bruce"});
    }

    public override void GuardBreak()
    {
        base.GuardBreak();
        if(!isGuardBreaking)
            aIPlayerMessage.PrepareAndSendMessage(MessageTypes.UPDATE_MANA, new string[]{"Enemy", "Bruce", "750", "Sub"});
    }
    
    public override void EndGuardBreak()
    {
        base.EndGuardBreak();
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.END_GUARD_BREAK, new string[]{"Enemy", "Bruce"});
    }

    public override void EndGuardBreakReaction()
    {
        base.EndGuardBreakReaction();
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.ACTION_TERMINATED, new string[]{"Enemy", "Bruce"});
    }

    public override void EndSurprise()
    {
        base.EndSurprise();
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.ACTION_TERMINATED, new string[]{"Enemy", "Bruce"});
    }

    public override void EndSuperHit()
    {
        base.EndSuperHit();
        aIPlayerMessage.PrepareAndSendMessage(MessageTypes.ACTION_TERMINATED, new string[]{"Enemy", "Bruce"});
    }
}
