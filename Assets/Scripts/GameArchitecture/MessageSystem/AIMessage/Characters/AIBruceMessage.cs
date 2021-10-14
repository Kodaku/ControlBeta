using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBruceMessage : AIPlayerMessage
{
    private Transform target;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ReceiveMessage(MessageTypes messageType, string message)
    {
        base.ReceiveMessage(messageType, message);
        if(messageType == MessageTypes.BEGIN_ATTACK)
        {
            // print("From " + transform.name + ": " + message);
        }
        else if(messageType == MessageTypes.EXECUTE_ATTACK)
        {
            string[] splittedString = message.Split(',');
            int damage = int.Parse(splittedString[0]);
            int range = int.Parse(splittedString[1]);
            if(Vector3.Distance(this.transform.position, target.transform.position) < range)
            {
                // print("Hit");
                GetComponent<PlayerHealth>().UpdateHealth(damage);
            }
        }
        else if(messageType == MessageTypes.LAUNCH_PROJECTILE)
        {
            // print("The enemy is launching a projectile");
        }
        else if(messageType == MessageTypes.UPDATE_MANA)
        {
            string[] splittedString = message.Split(',');
            int manaCount = int.Parse(splittedString[0]);
            string updateMethod = splittedString[1];
            if(updateMethod == "Add")
            {
                decisionMaker.SendUpdateRequest(UpdatableIndices.MANA, manaCount);
            }
            else if(updateMethod == "Sub")
            {
                decisionMaker.SendUpdateRequest(UpdatableIndices.MANA, -manaCount);
            }
            // print("Mana Count: " + manaCount);
        }
        else if(messageType == MessageTypes.BEGIN_SPECIAL_ATTACK)
        {
            decisionMaker.ResetCurrentSpecialAttackTimer();
        }
        else if(messageType == MessageTypes.END_SPECIAL_ATTACK)
        {
            decisionMaker.ResetCurrentSpecialAttackTimer();
            decisionMaker.CanDecideNextMove();
        }
        else if(messageType == MessageTypes.ACTION_TERMINATED)
        {
            decisionMaker.CanDecideNextMove();
        }
        else if(messageType == MessageTypes.EVADE)
        {
            decisionMaker.FleeFromPlayer();
        }
        else if(messageType == MessageTypes.APPLY_PUNCH_DAMAGE)
        {
            string[] splittedString = message.Split(',');
            int damage = int.Parse(splittedString[0]);
            // print("From " + gameObject.name + " damage: " + damage);
            decisionMaker.SendUpdateRequest(UpdatableIndices.HEALTH, -damage);
            decisionMaker.ApplyDamage();
        }
        else if(messageType == MessageTypes.GUARD_BREAK)
        {
            decisionMaker.ApplyGuardBreakReaction();
        }
    }
}
