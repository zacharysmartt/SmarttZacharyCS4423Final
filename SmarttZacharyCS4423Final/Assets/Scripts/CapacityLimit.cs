using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacityLimit : MonoBehaviour
{
    public static int capacity;
    public static int capacityLimit;
    public static int money;
    public static int moneyGoal;
    public static int treeCount;
    public static int treeLimit = 10;

    void Awake() {
        capacity = 0;
        capacityLimit = 10;
        money = 100;
        moneyGoal = 2000;
    }
}
