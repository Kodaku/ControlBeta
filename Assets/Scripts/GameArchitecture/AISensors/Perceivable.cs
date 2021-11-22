using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Perceivable<T>
{
    T GetMeasure();
    void ResetTarget();
}
