using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanHitDetector : HitDetector
{
    private HumanPlayerMessage humanPlayerMessage;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        humanPlayerMessage = GameObject.FindGameObjectWithTag("Player").GetComponent<HumanPlayerMessage>();
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
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
            // print(hit[0].gameObject.name);
            GameObject newHitFX = Instantiate(hitFX, hitFXPos, Quaternion.identity);
            newHitFX.gameObject.SetActive(true);
            EffectsDestroyer.instance.DestroyEffect(newHitFX);
            //Send a message to apply damage
            humanPlayerMessage.PrepareAndSendMessage(MessageTypes.APPLY_PUNCH_DAMAGE, new string[]{"Enemy", hit[0].gameObject.name, damage.ToString()});
            canEvaluateHit = false;
            gameObject.SetActive(false);
        }
    }
}
