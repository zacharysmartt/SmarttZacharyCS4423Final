using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite normalSprite;
    public Sprite openSprite;
    public bool isOpen;

    void Start() {
        sr = gameObject.GetComponent<SpriteRenderer>();
        isOpen = false;
    }

    void Update() {
        if (CapacityLimit.money >= CapacityLimit.moneyGoal) {
            Open();
        }
        else if (CapacityLimit.money < CapacityLimit.moneyGoal) {
            Close();
        }
    }

    void Open() {
        isOpen = true;
        sr.sprite = openSprite;
    }

    void Close() {
        isOpen = false;
        sr.sprite = normalSprite;
    }

}
