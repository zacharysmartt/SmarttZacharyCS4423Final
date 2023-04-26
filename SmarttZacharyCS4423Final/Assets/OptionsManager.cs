using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    void Start() {
        CloseOptions();
    }
    public void OpenOptions() {
        GetComponent<Canvas>().enabled = true;
    }

    public void CloseOptions() {
        GetComponent<Canvas>().enabled = false;
    }
}
