using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuHandler : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("DemoScene");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
