using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene(2);
    }

    public void QuitGame() {
        print("quit");
        Application.Quit();
    }
    
    public void GoHome(){
        SceneManager.LoadScene(0);
    }

    public void ShowInstructions() {
        SceneManager.LoadScene(1);
    }
}
