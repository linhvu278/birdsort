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

    [SerializeField] private Transform winPopUp, winFilter;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private AudioSource winMusic;
    private float popUpSpeed = 1f;
    // [SerializeField] private Vector3 ogPos;
    private Vector3 ogPos = new Vector3(0, 10f, 0);

    void Awake(){
        if (instance == null) instance = this;
    }
    void Start(){
        mainGameManager = MainGameManager.instance;
        birdSpawner = BirdsToSpawn.instance;

        nextLevelButton.onClick.AddListener(NextLevel);
        
        // EnableWinScreen(false);
        // ogPos = transform.position;
        Debug.Log(ogPos);
    }

    void NextLevel(){
        // winFilter.gameObject.SetActive(false);
        // winPopUp.gameObject.SetActive(false);
        // winPopUp.DOMove(ogPos, popUpSpeed).SetEase(Ease.OutBack);

        EnableWinScreen(false);
        winPopUp.position = ogPos;
        mainGameManager.NextLevel();
    }
    public void EnableWinScreen(bool value){
        switch (value){
            case true:
                winFilter.gameObject.SetActive(true);
                // winPopUp.gameObject.SetActive(true);
                winPopUp.DOMove(Vector3.zero, popUpSpeed).SetEase(Ease.OutBack);
                winMusic.Play();
                // mainGameManager.EnableReplayButton(false);
                break;
            case false:
                winFilter.gameObject.SetActive(false);
                // winPopUp.gameObject.SetActive(false);
                // winPopUp.DOMove(ogPos, popUpSpeed).SetEase(Ease.OutBack);
                // winPopUp.position = ogPos;
                break;
        }
    }
}