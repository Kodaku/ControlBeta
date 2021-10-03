using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Updatable
{
    void AddQuantity(int quantity);
    float GetQuantity();
}
