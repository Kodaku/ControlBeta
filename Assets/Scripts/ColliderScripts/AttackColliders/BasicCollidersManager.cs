using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollidersType
{
    LEFT_PUNCH_COMBO,
    RIGHT_PUNCH_COMBO,
    LEFT_PUNCH_STRONG,
    RIGHT_PUNCH_STRONG,
    LEFT_LEG_COMBO,
    RIGHT_LEG_COMBO,
    LEFT_LEG_STRONG,
    RIGHT_LEG_STRONG
}

public class BasicCollidersManager : MonoBehaviour
{
    public BasicColliderDetector rightHandCollider;
    public BasicColliderDetector leftHandCollider;
    public BasicColliderDetector rightLegCollider;
    public BasicColliderDetector leftLegCollider;
    public BasicColliderDetector rightHandStrongCollider;
    public BasicColliderDetector leftHandStrongCollider;
    public BasicColliderDetector rightLegStrongCollider;
    public BasicColliderDetector leftLegStrongCollider;

    public void ActivateCollider(CollidersType collidersType)
    {
        switch(collidersType)
        {
            case CollidersType.LEFT_PUNCH_COMBO:
                leftHandCollider.gameObject.SetActive(true);
                break;
            case CollidersType.RIGHT_PUNCH_COMBO:
                rightHandCollider.gameObject.SetActive(true);
                break;
            case CollidersType.LEFT_PUNCH_STRONG:
                leftHandStrongCollider.gameObject.SetActive(true);
                break;
            case CollidersType.RIGHT_PUNCH_STRONG:
                rightHandStrongCollider.gameObject.SetActive(true);
                break;
            case CollidersType.LEFT_LEG_COMBO:
                leftLegCollider.gameObject.SetActive(true);
                break;
            case CollidersType.RIGHT_LEG_COMBO:
                rightLegCollider.gameObject.SetActive(true);
                break;
            case CollidersType.LEFT_LEG_STRONG:
                leftLegStrongCollider.gameObject.SetActive(true);
                break;
            case CollidersType.RIGHT_LEG_STRONG:
                rightLegStrongCollider.gameObject.SetActive(true);
                break;
        }
        StartCoroutine(EmergencyDeactivation(collidersType));
    }

    public void DectivateCollider(CollidersType collidersType)
    {
        switch(collidersType)
        {
            case CollidersType.LEFT_PUNCH_COMBO:
                leftHandCollider.gameObject.SetActive(false);
                break;
            case CollidersType.RIGHT_PUNCH_COMBO:
                rightHandCollider.gameObject.SetActive(false);
                break;
            case CollidersType.LEFT_PUNCH_STRONG:
                leftHandStrongCollider.gameObject.SetActive(false);
                break;
            case CollidersType.RIGHT_PUNCH_STRONG:
                rightHandStrongCollider.gameObject.SetActive(false);
                break;
            case CollidersType.LEFT_LEG_COMBO:
                leftLegCollider.gameObject.SetActive(false);
                break;
            case CollidersType.RIGHT_LEG_COMBO:
                rightLegCollider.gameObject.SetActive(false);
                break;
            case CollidersType.LEFT_LEG_STRONG:
                leftLegStrongCollider.gameObject.SetActive(false);
                break;
            case CollidersType.RIGHT_LEG_STRONG:
                rightLegStrongCollider.gameObject.SetActive(false);
                break;
        }
    }

    private IEnumerator EmergencyDeactivation(CollidersType collidersType)
    {
        yield return new WaitForSeconds(0.4f);
        DectivateCollider(collidersType);
    }
}
