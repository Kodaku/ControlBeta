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
            Vector3 hitFXPos = hit[0].transform.position;
            hitFXPos.y += 1.3f;
            if(hit[0].transform.forward.x > 0.0f)
            {
                hitFXPos.x += 0.3f;
            }
            else if(hit[0].transform.forward.x < 0.0f)
            {
                hitFXPos.x -= 0.3f;
            }
            GameObject newHitFX = Instantiate(hitFX, hitFXPos, Quaternion.identity);
            newHitFX.gameObject.SetActive(true);
            EffectsDestroyer.instance.DestroyEffect(newHitFX);
            //Send a message to the player to apply the damage
            aIPlayerMessage.PrepareAndSendMessage(MessageTypes.APPLY_PUNCH_DAMAGE, new string[]{"Player", "Player", damage.ToString()});
            canEvaluateHit = false;
            gameObject.SetActive(false);
        }
    }
}
