using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager instance;

    [SerializeField] private Button homeButton, replayButton;

    void Start(){
        homeButton.onClick.AddListener(BackToMainMenu);
        replayButton.onClick.AddListener(ReplayLevel);
    }

    void BackToMainMenu(){
        Debug.Log("back to main menu");
        // destroy list of birds to spawn
    }
    void ReplayLevel(){
        // Debug.Log("replay level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // dont destroy on load list of birds to spawn
    }
    public void NextLevel(){
        Debug.Log("next level");
        // clear list of birds to spawn
    }
    public void EnableReplayButton(bool value){
        replayButton.enabled = value;
    }
    void Awake(){
        if (instance == null) instance = this;
    }
}