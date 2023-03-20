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
    }
    void ReplayLevel(){
        // Debug.Log("replay level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel(){
        Debug.Log("next level");
    }
    public void EnableReplayButton(bool value){
        replayButton.enabled = value;
    }
    void Awake(){
        if (instance == null) instance = this;
    }
}