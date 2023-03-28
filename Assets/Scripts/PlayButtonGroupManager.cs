using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayButtonGroupManager : MonoBehaviour
{
    public static PlayButtonGroupManager instance;

    [SerializeField] private Button removeAdsButton, addBranchButton, swapButton, undoButton, skipButton;
    [SerializeField] private TextMeshProUGUI addBranchCounter, swapCounter, undoCounter, skipCounter;
    private int addBranchInt, swapInt, undoInt, skipInt;

    private MainGameManager mainGameManager;
    private BirdManager birdManager;
    private UndoList undoList;

    [SerializeField] public TextMeshProUGUI swapText;

    public bool canSwap;

    void Awake(){
        if (instance == null) instance = this;
    }
    void Start(){
        mainGameManager = MainGameManager.instance;
        birdManager = BirdManager.instance;
        undoList = UndoList.instance;

        addBranchButton.onClick.AddListener(AddBranch);
        swapButton.onClick.AddListener(Swap);
        undoButton.onClick.AddListener(Undo);
        skipButton.onClick.AddListener(Skip);

        removeAdsButton.onClick.AddListener(RemoveAds);

        addBranchInt = 100;
        swapInt = 100;
        undoInt = 100;
        skipInt = 100;

        swapText.enabled = false;
        addBranchCounter.text = addBranchInt.ToString();
        swapCounter.text = swapInt.ToString();
        undoCounter.text = undoInt.ToString();
        skipCounter.text = skipInt.ToString();
    }
    void AddBranch(){
        if (addBranchInt > 0){
            birdManager.AddBranch();
            addBranchInt--;
            if (addBranchInt == 0) addBranchButton.interactable = false;
            addBranchCounter.text = addBranchInt.ToString();
        }
    }
    void Swap(){
        if (swapInt > 0){
            canSwap = !canSwap;
            swapText.enabled = canSwap;
            swapCounter.text = swapInt.ToString();
        }
    }
    public void AfterSwap(){
        canSwap = false;
        swapText.enabled = false;
        swapInt--;
        if (swapInt == 0) swapButton.interactable = false;
        swapCounter.text = swapInt.ToString();
    }
    void Undo(){
        if (undoInt > 0 && undoList.GetUndoTurnsAmount() > 0) undoList.UndoMoveBirds();
    }
    public void AfterUndo(){
        undoInt--;
        if (undoInt == 0) undoButton.interactable = false;
        undoCounter.text = undoInt.ToString();
    }
    void Skip(){
        if (skipInt > 0){
            mainGameManager.NextLevel();
            skipInt--;
            if (skipInt == 0) skipButton.interactable = false;
            skipCounter.text = skipInt.ToString();
        }
    }
    public void SetAddBranchInt(int x){
        addBranchInt = x;
    }
    public void SetSwapInt(int x){
        if (x < 0) swapInt--;
        else swapInt = x;
    }
    public void SetUndoInt(int x){
        undoInt = x;
    }
    public void SetSkipInt(int x){
        skipInt = x;
    }
    void RemoveAds(){
        Debug.Log("ads removed");
    }
}
