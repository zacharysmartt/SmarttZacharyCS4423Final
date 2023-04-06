using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacityLimit : MonoBehaviour
{
    public static int capacity;
    public static int capacityLimit;
    public static int treeCount;
    public static int treeLimit = 10;

    void Start() {
        capacity = 0;
        capacityLimit = 10;
        treeCount = 1;
    }
}
