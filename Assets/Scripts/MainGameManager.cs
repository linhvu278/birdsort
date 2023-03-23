using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager instance;

    private BranchSpawner branchSpawner;

    [SerializeField] private Button homeButton, replayButton;
    [SerializeField] private TextMeshProUGUI levelText;
    private int levelId;

    void Start(){
        branchSpawner = BranchSpawner.instance;

        levelId = 1;
        
        homeButton.onClick.AddListener(BackToMainMenu);
        replayButton.onClick.AddListener(ReplayLevel);
        levelText.GetComponent<TextMeshProUGUI>();

        // levelText.text = SceneManager.GetActiveScene().name;
        levelText.text = String.Format("Level {0}", levelId);

        Application.targetFrameRate = 300;
    }

    void BackToMainMenu(){
        // destroy list of birds to spawn
        Debug.Log("back to main menu");
    }
    void ReplayLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Debug.Log("replay level");
    }
    public void NextLevel(){
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings){
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            levelText.text = String.Format("Level {0}", levelId++);
            Debug.Log("next level");
        }
    }
    public void EnableReplayButton(bool value){
        replayButton.enabled = value;
    }
    void Awake(){
        // DontDestroyOnLoad(gameObject);
        
        if (instance == null) instance = this;
        // else Destroy(gameObject);
    }
}