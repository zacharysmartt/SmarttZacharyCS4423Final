using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningHandler : MonoBehaviour
{
    public Text warningText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void capacityError() {
        /*if (CapacityLimit.capacity >= CapacityLimit.capacityLimit) {
            warningText.text = "Limit exceeded. Sell inventory to make room for more.";
            yield return new WaitForSeconds(10f);
            warningText.text = "";
        }*/
    }
}
