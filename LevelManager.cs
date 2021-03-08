using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //outlets
    public GameObject[] levelButtons = new GameObject[3];
    public Sprite locked;
    public Sprite complete;
    public Sprite incomplete;

    //state tracking
    bool[] levelsStatus = new bool[3];

    void Start() {
        for (int i=0; i<levelsStatus.Length; i++) {
            if (PlayerPrefs.GetInt("HighestCompletedLevel") > i) {
                levelsStatus[i] = true;
                levelButtons[i].GetComponent<Image>().sprite = complete;

            }
            else if (PlayerPrefs.GetInt("HighestCompletedLevel") == i) {
                levelsStatus[i] = true;
                levelButtons[i].GetComponent<Image>().sprite = incomplete;
            }
            else {
                levelsStatus[i] = false;
                levelButtons[i].SetActive(false);
            }
        }
    }

    public void LevelFunc(string levelName) {
        SceneManager.LoadScene(levelName);
    }
}
