using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public enum Character {
        girl,
        boy,
        adventureGirl,
        adventureBoy,
        ninjaGirl,
        ninjaBoy,
        knight,
        cat,
        dog,
        dinosaur,
        robot,
        pumpkinHead,
        santa
    }

    //outlets
    public GameObject pauseMenu;
    public GameObject levelSelectMenu;
    public GameObject settingsMenu;
    public GameObject confirmMenu;
    public GameObject charactersMenu;
    public GameObject soundButton;
    public Sprite SoundOn;
    public Sprite SoundOff;
    public GameObject pauseButton;

    //state Tracking
    public Character character;
    public GameObject[] highlights = new GameObject[13];
    public GameObject[] locks = new GameObject[13];
    public bool[] charactersUnlocked = new bool[13];
    public int numCoins;
    public Text numCoinsText;


    void Start()
    {
        pauseMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        settingsMenu.SetActive(false);
        charactersMenu.SetActive(false);
        confirmMenu.SetActive(false);
        if (PlayerPrefs.GetInt("Mute") == 1) {
            PlayerController.instance.isMuted = true;
            PlayerController.instance.audioSource.enabled = false;
            soundButton.GetComponent<Image>().sprite = SoundOff;
        }
        else {
            PlayerController.instance.isMuted = false;
            PlayerController.instance.audioSource.enabled = true;
            soundButton.GetComponent<Image>().sprite = SoundOn;
        }
        if(PlayerPrefsX.GetBoolArray("charactersUnlocked").Length != 0) {
            charactersUnlocked = PlayerPrefsX.GetBoolArray("charactersUnlocked");
        }
        for(int i=0; i< charactersUnlocked.Length; i++) {
            if(charactersUnlocked[i]) {
                locks[i].SetActive(false);
            }
            if (i == PlayerPrefs.GetInt("Character")) {
                highlights[i].SetActive(true);
            }
            else {
                highlights[i].SetActive(false);
            }
        }
        numCoins = PlayerPrefs.GetInt("Coins");
        numCoinsText.text = "You have: " + numCoins.ToString() + "¢";
    }

    public void Pause() {
        PlayerController.instance.isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        levelSelectMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void SoundButton() {
        if (PlayerController.instance.isMuted) {
            PlayerController.instance.isMuted = false;
            PlayerPrefs.SetInt("Mute", 0);
            PlayerController.instance.audioSource.enabled = true;
            soundButton.GetComponent<Image>().sprite = SoundOn;
        }
        else {
            PlayerController.instance.isMuted = true;
            PlayerPrefs.SetInt("Mute", 1);
            PlayerController.instance.audioSource.enabled = false;
            soundButton.GetComponent<Image>().sprite = SoundOff;
        }
    }

    public void ResumeGame() {
        PlayerController.instance.isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        pauseButton.SetActive(true);
        charactersMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void LevelSelect() {
        levelSelectMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Settings() {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        confirmMenu.SetActive(false);
        charactersMenu.SetActive(false);
    }

    public void ConfirmReset() {
        confirmMenu.SetActive(true);
    }

    public void ResetPlayerPrefs() {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Level1");
    }

    public void CharactersMenu() {
        charactersMenu.SetActive(true);
    }

    public void SwitchCharacters(int character) {
        int numCoins = PlayerPrefs.GetInt("Coins");
        if (charactersUnlocked[character]) {
            PlayerController.instance.animator.SetInteger("Character", character);
            highlights[character].SetActive(true);
            highlights[PlayerPrefs.GetInt("Character")].SetActive(false);
            PlayerPrefs.SetInt("Character", character);
        }
        else if (numCoins >= 5){
            numCoins -= 5;
            PlayerController.instance.numCoins -=5;
            PlayerPrefs.SetInt("Coins", numCoins);
            numCoinsText.text = "You have: " + numCoins.ToString() + "¢";
            locks[character].SetActive(false);
            charactersUnlocked[character] = true;
        }
        PlayerPrefsX.SetBoolArray("charactersUnlocked", charactersUnlocked);
    }
}
