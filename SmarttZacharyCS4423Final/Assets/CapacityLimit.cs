using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacityLimit : MonoBehaviour
{
    public static int capacity;
    public static int capacityLimit;

    void Start() {
        capacity = 0;
        capacityLimit = 10;
    }
}
