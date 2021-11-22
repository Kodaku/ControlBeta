using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerMessage : PlayerMessage
{
    // protected AIDecisionMaker decisionMaker;
    protected AIBehaviourTree behaviourTree;
    // Start is called before the first frame update
    public virtual void Start()
    {
        // decisionMaker = GetComponent<AIDecisionMaker>();
        behaviourTree = GetComponent<AIBehaviourTree>();
    }

    public override void ReceiveMessage(MessageTypes messageType, string message)
    {
        base.ReceiveMessage(messageType, message);
    }
}
