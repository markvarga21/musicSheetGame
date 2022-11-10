using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void DisplayAbout() {
        SceneManager.LoadScene(4);
    }

    public void DisplayMenu() {
        SceneManager.LoadScene(0);
    }
}
