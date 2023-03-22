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

    void Start(){
        mainGameManager = MainGameManager.instance;
        birdSpawner = BirdsToSpawn.instance;

        nextLevelButton.onClick.AddListener(NextLevel);
        EnableWinScreen(false);
    }

    void NextLevel(){
        // Debug.Log("next level");
        birdSpawner.ClearBirdsList();
        mainGameManager.NextLevel();
    }
    public void EnableWinScreen(bool value){
        if (value){
            winFilter.gameObject.SetActive(value);
            winPopUp.gameObject.SetActive(value);
            winPopUp.DOMove(new Vector3(0, 0, 0), popUpSpeed).SetEase(Ease.OutBack);
            winMusic.Play();
            // mainGameManager.EnableReplayButton(false);
        }
    }
    void Awake(){
        if (instance == null) instance = this;
    }
}