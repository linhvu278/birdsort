using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager instance;

    // private BranchSpawner branchSpawner;
    private BirdManager birdManager;
    private BirdsToSpawn birdSpawner;
    private WinScreenManager winScreenManager;
    private SettingsScreenManager settingsScreenManager;
    private AudioManager audioManager;

    [SerializeField] private Button /*homeButton ,settingsButton*/ pauseButton, replayButton;
    [SerializeField] private TextMeshProUGUI levelText;

    private int levelId;
    private const int numberOfLevels = 14;
    private bool isVibrationOn;

    void Awake(){
        // DontDestroyOnLoad(gameObject);
        
        if (instance == null) instance = this;
        // else Destroy(gameObject);
    }
    void Start(){
        // branchSpawner = BranchSpawner.instance;
        birdManager = BirdManager.instance;
        birdSpawner = BirdsToSpawn.instance;
        winScreenManager = WinScreenManager.instance;
        settingsScreenManager = SettingsScreenManager.instance;
        audioManager = AudioManager.instance;

        levelId = 1;

        // homeButton = GameObject.FindGameObjectWithTag("HomeButton").GetComponent<Button>();
        // settingsButton = GameObject.FindGameObjectWithTag("SettingsButton").GetComponent<Button>();
        pauseButton = GameObject.FindGameObjectWithTag("PauseButton").GetComponent<Button>();
        replayButton = GameObject.FindGameObjectWithTag("ReplayButton").GetComponent<Button>();
        levelText = GameObject.FindGameObjectWithTag("LevelText").GetComponent<TextMeshProUGUI>();
        
        // homeButton.onClick.AddListener(BackToMainMenu);
        // settingsButton.onClick.AddListener(SettingsMenu);
        pauseButton.onClick.AddListener(SettingsMenu);
        replayButton.onClick.AddListener(ReplayLevel);
        levelText.GetComponent<TextMeshProUGUI>();

        // levelText.text = SceneManager.GetActiveScene().name;
        levelText.text = String.Format("Level {0}", levelId);

        Application.targetFrameRate = 300;
    }

    void ReplayLevel(){
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // birdSpawner.ClearBirdsList();

        birdManager.ClearBirdControllers();
        birdManager.ClearAllBranchesAndBirds();
        birdManager.SpawnBranches();
    }
    public void NextLevel(){
        // if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings){
        if (levelId < numberOfLevels){
            levelId++;
            levelText.text = String.Format("Level {0}", levelId);
            if (levelId %2 != 0) birdManager.numberOfBranches++;

            birdSpawner.ClearBirdsList();

            birdManager.ClearBirdControllers();
            birdManager.ClearAllBranchesAndBirds();
            birdManager.SpawnBranches();
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void EnableReplayButton(bool value){
        replayButton.enabled = value;
    }
    void SettingsMenu(){
        // open settings menu
        settingsScreenManager.EnableSettingsScreen(true);
    }
    public void WinLevel(){
        winScreenManager.EnableWinScreen(true);
        audioManager.PlayWinSound();
        if (isVibrationOn) Handheld.Vibrate();
    }
    public void LoseLevel(){
        // lose
    }
    void BackToMainMenu(){
        // destroy list of birds to spawn
        Debug.Log("back to main menu");
    }
    public void ToggleVibration(bool value){
        isVibrationOn = value;
    }
}