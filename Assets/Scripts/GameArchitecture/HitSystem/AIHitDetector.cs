using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHitDetector : HitDetector
{
    private AIPlayerMessage aIPlayerMessage;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        aIPlayerMessage = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AIPlayerMessage>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    protected override void DetectCollision()
    {
        base.DetectCollision();
    }

    protected override void EvaluateHit(Collider[] hit)
    {
        base.EvaluateHit(hit);
        if(hit.Length > 0)
        {
            //Send a message to the player to apply the damage
            aIPlayerMessage.PrepareAndSendMessage(MessageTypes.APPLY_PUNCH_DAMAGE, new string[]{"Player", "Player", damage.ToString()});
            gameObject.SetActive(false);
        }
    }
}
