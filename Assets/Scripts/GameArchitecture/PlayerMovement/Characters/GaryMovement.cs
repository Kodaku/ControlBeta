using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaryMovement : HumanPlayerMovement
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Move(PlayerStates currentState, float[] optionalValues)
    {
        base.Move(currentState, optionalValues);
    }

    public override void StartEnergyCharge()
    {
        base.StartEnergyCharge();
        SendMessageFromMovement(MessageTypes.UPDATE_MANA, new string[]{gameObject.tag, gameObject.name, "2", "Add"});
    }

    public override void SendMessageFromMovement(MessageTypes messageTypes, string[] info)
    {
        base.SendMessageFromMovement(messageTypes, info);
        humanPlayerMessage.PrepareAndSendMessage(messageTypes, info);
    }
}
