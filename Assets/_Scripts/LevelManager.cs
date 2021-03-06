﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {
    public int currentStage = 0;

    public static LevelManager instance = null;

    public void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void LoadNextStage() {
        if (currentStage < 5) {
            SceneManager.LoadScene("Stage" + ++currentStage);
        } else
            SceneManager.LoadScene("Win");
    }

    // Loads the scene with the given name
    public void LoadLevel(string level) {
        SceneManager.LoadScene(level);
    }

    // Quit game
    public void Quit() {
        Application.Quit();
    }
}
