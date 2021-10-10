using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaryMessage : HumanPlayerMessage
{
    private Vector3 startExplosionPosition;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ReceiveMessage(MessageTypes messageType, string message)
    {
        base.ReceiveMessage(messageType, message);
        if(messageType == MessageTypes.BEGIN_EXPLOSION_ATTACK)
        {
            // print("From " + transform.name + ": " + message);
            startExplosionPosition = this.transform.position;
        }
        else if(messageType == MessageTypes.EXECUTE_EXPLOSION_ATTACK)
        {
            string[] splittedString = message.Split(',');
            int damage = int.Parse(splittedString[0]);
            int range = int.Parse(splittedString[1]);
            // print("Damage: " + damage);
            // print("Range: " + range);
            // print("Distance: " + Vector3.Distance(this.transform.position, startExplosionPosition));
            if(Vector3.Distance(this.transform.position, startExplosionPosition) < range)
            {
                // print("Hit");
                // GetComponent<PlayerHealth>().UpdateHealth(damage);
                buttonController.SendUpdateRequest(UpdatableIndices.HEALTH, -damage);
            }
        }
        else if(messageType == MessageTypes.UPDATE_MANA)
        {
            string[] splittedString = message.Split(',');
            int manaCount = int.Parse(splittedString[0]);
            // print("Mana Count: " + manaCount);
            string updateMethod = splittedString[1];
            if(updateMethod == "Add")
            {
                buttonController.SendUpdateRequest(UpdatableIndices.MANA, manaCount);
            }
            else if(updateMethod == "Sub")
            {
                buttonController.SendUpdateRequest(UpdatableIndices.MANA, -manaCount);
            }
        }
        else if(messageType == MessageTypes.EVADE)
        {
            buttonController.TranslateEvade();
        }
        else if(messageType == MessageTypes.APPLY_PUNCH_DAMAGE)
        {
            string[] splittedString = message.Split(',');
            int damage = int.Parse(splittedString[0]);
            // print("From " + gameObject.name + " damage: " + damage);
            buttonController.SendUpdateRequest(UpdatableIndices.HEALTH, -damage);
            buttonController.ApplyDamage();
        }
    }
}
