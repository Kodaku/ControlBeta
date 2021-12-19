using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBruceMessage : AIPlayerMessage
{
    private Vector3 targetPosition;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
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
            positionSensor.ResetTarget();
            targetPosition = positionSensor.GetMeasure();
            string[] splittedString = message.Split(',');
            int damage = int.Parse(splittedString[0]);
            int range = int.Parse(splittedString[1]);
            if(Vector3.Distance(this.transform.position, targetPosition) < range)
            {
                // print("Hit");
                // GetComponent<PlayerHealth>().UpdateHealth(damage);
                behaviourTree.SendUpdateRequest(UpdatableIndices.HEALTH, -damage);
                behaviourTree.ApplyDamage();
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
                behaviourTree.SendUpdateRequest(UpdatableIndices.MANA, manaCount);
            }
            else if(updateMethod == "Sub")
            {
                behaviourTree.SendUpdateRequest(UpdatableIndices.MANA, -manaCount);
            }
            // print("Mana Count: " + manaCount);
        }
        else if(messageType == MessageTypes.BEGIN_SPECIAL_ATTACK)
        {
            print("Begin Special Attack");
            behaviourTree.ResetCurrentSpecialAttackTimer();
            behaviourTree.StartingSpecialAttack();
        }
        else if(messageType == MessageTypes.END_SPECIAL_ATTACK)
        {
            print("End Special Attack");
            behaviourTree.ResetCurrentSpecialAttackTimer();
            behaviourTree.EndSpecialAttack();
            // decisionMaker.CanDecideNextMove();
        }
        else if(messageType == MessageTypes.ACTION_TERMINATED)
        {
            // decisionMaker.CanDecideNextMove();
            // behaviourTree.EndSpecialAttack();
        }
        else if(messageType == MessageTypes.EVADE)
        {
            // decisionMaker.FleeFromPlayer();
        }
        else if(messageType == MessageTypes.APPLY_PUNCH_DAMAGE)
        {
            string[] splittedString = message.Split(',');
            int damage = int.Parse(splittedString[0]);
            // print("From " + gameObject.name + " damage: " + damage);
            behaviourTree.SendUpdateRequest(UpdatableIndices.HEALTH, -damage);
            behaviourTree.ApplyDamage();
        }
        else if(messageType == MessageTypes.GUARD_BREAK)
        {
            behaviourTree.ApplyGuardBreakReaction();
        }
        else if(messageType == MessageTypes.END_GUARD_BREAK)
        {
            // behaviourTree.EndGuardBreak();
        }
        else if(messageType == MessageTypes.SWITCH_CHARACTER)
        {
            behaviourTree.ResetTarget();
        }
    }
}
