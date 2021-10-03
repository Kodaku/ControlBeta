using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitsCollector : MonoBehaviour
{
    private int strongHitsCount = 0;
    private bool alreadyHit = false;
    private bool strongHitCounted = false;

    public int GetStrongHitsCount()
    {
        return strongHitsCount;
    }

    public void IncrementStrongHitsCount()
    {
        strongHitsCount++;
    }

    public void ResetStrongHitsCount()
    {
        strongHitsCount = 0;
    }

    public void HitCombo()
    {
        alreadyHit = true;
        StartCoroutine(WaitForComboHit());
    }

    public void CountStrongHit()
    {
        strongHitCounted = true;
        StartCoroutine(WaitForStrongHit());
    }

    public bool IsStrongHitCounted()
    {
        return strongHitCounted;
    }

    public bool IsAlreadyHit()
    {
        return alreadyHit;
    }

    private IEnumerator WaitForComboHit()
    {
        yield return new WaitForSeconds(0.1f);
        alreadyHit = false;
    }

    private IEnumerator WaitForStrongHit()
    {
        yield return new WaitForSeconds(0.1f);
        strongHitCounted = false;
    }

}
