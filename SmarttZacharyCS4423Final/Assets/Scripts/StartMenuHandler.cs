using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuHandler : MonoBehaviour
{
    public AudioSource audio;
    public void StartGame() {
        SceneManager.LoadScene("Level Select");
    }

    public void playButtonSound() {
        audio.Play();
    }

    public void QuitGame() {
        Application.Quit();
    }
}
