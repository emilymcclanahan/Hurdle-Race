using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) {
        // Reload scene only when colliding with player
        if(other.gameObject.GetComponent<PlayerController>()) {
            PlayerPrefs.SetInt("Coins", PlayerController.instance.numCoins);
            if (PlayerPrefs.GetInt("HighestCompletedLevel") < SceneManager.GetActiveScene().buildIndex) {
                PlayerPrefs.SetInt("HighestCompletedLevel", SceneManager.GetActiveScene().buildIndex-1);
            }
            //Load Next Level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
