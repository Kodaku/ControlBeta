using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerMessage : PlayerMessage
{
    protected AIDecisionMaker decisionMaker;
    // Start is called before the first frame update
    public virtual void Start()
    {
        decisionMaker = GetComponent<AIDecisionMaker>();
    }

    public override void ReceiveMessage(MessageTypes messageType, string message)
    {
        base.ReceiveMessage(messageType, message);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
