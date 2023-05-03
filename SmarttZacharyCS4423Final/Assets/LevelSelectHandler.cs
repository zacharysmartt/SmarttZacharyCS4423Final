using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectHandler : MonoBehaviour
{
    public AudioSource audio;

    public void playButtonSound() {
        audio.Play();
    }
    
    public void LoadFirstLevel() {
        SceneManager.LoadScene("DemoScene");
    }

    public void LoadSecondLevel() {
        SceneManager.LoadScene("Level2");
    }

    public void LoadThirdLevel() {
        SceneManager.LoadScene("Level3");
    }

    public void BackButton() {
        SceneManager.LoadScene("Title Screen");
    }
}
