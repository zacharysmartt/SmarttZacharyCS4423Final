using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (!(this.tag.Contains("Speed") || this.tag.Contains("Money"))) {
                if (CapacityLimit.capacity < CapacityLimit.capacityLimit) {
                    CapacityLimit.capacity += 1;
                    Destroy(this.gameObject);
                }
            }
            else {
                Destroy(this.gameObject);
            }
        }
    }
}
