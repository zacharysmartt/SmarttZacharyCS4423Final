using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAudio : MonoBehaviour
{
    public AudioSource walkAudio;

    void update() {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D)) {
            walkAudio.enabled = true;
        }
        else {
            walkAudio.enabled = false;
        }
    }
}
