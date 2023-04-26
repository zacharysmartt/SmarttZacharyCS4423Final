using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{

    int currentMin = 0;
    int startMin = 9;
    float currentSec = 0f;
    float startSec = 59f;
    public Text minText;
    public Text secText;

    // Start is called before the first frame update
    void Start()
    {
        currentMin = startMin;
        currentSec = startSec;
        minText.text = currentMin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currentSec -= 1 * Time.deltaTime;
        secText.text = ":" + currentSec.ToString("00");

        if(currentSec <= 0) {
            if(currentMin == 0) {
                currentSec = 0f;
            }
            else {
                currentSec = 59f;
                currentMin--;
                minText.text = currentMin.ToString();
            }
        }
    }
}
