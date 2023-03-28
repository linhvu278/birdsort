using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WinScreenManager : MonoBehaviour
{
    public static WinScreenManager instance;

    private MainGameManager mainGameManager;
    private BirdsToSpawn birdSpawner;
    private AudioManager audioManager;

    [SerializeField] private Transform winPopUp, winFilter;
    [SerializeField] private Button nextLevelButton;
    private float winPopupSpeed = 1f;
    private Vector3 ogPos = new Vector3(0, 10f, 0);

    void Awake(){
        if (instance == null) instance = this;
    }
    void Start(){
        mainGameManager = MainGameManager.instance;
        birdSpawner = BirdsToSpawn.instance;
        audioManager = AudioManager.instance;

        nextLevelButton.onClick.AddListener(NextLevel);

        EnableWinScreen(false);
    }

    void NextLevel(){
        EnableWinScreen(false);
        mainGameManager.NextLevel();
    }
    public void EnableWinScreen(bool value){
        winFilter.gameObject.SetActive(value);
        switch (value){
            case true:
                winPopUp.DOMove(Vector3.zero, winPopupSpeed).SetEase(Ease.OutBack);
                audioManager.PlayWinSound();
                break;
            case false:
                winPopUp.position = ogPos;
                break;
        }
    }
}