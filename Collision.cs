using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.GetComponent<PlayerController>()) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
