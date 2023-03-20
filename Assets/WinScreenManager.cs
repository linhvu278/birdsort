using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour
{
    public static WinScreenManager instance;

    private MainGameManager mainGameManager;

    [SerializeField] private Transform winPopUp, winFilter;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private AudioSource winMusic;
    private float popUpSpeed = 1f;

    void Start(){
        mainGameManager = MainGameManager.instance;

        nextLevelButton.onClick.AddListener(NextLevel);
        EnableWinScreen(false);
    }

    void NextLevel(){
        // Debug.Log("next level");
        mainGameManager.NextLevel();
    }
    public void EnableWinScreen(bool value){
        if (value){
            winFilter.gameObject.SetActive(value);
            winPopUp.gameObject.SetActive(value);
            winPopUp.DOMove(new Vector3(0, 0, 0), popUpSpeed);
            winMusic.Play();
            // mainGameManager.EnableReplayButton(false);
        }
    }
    void Awake(){
        if (instance == null) instance = this;
    }
}