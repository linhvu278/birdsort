// using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public static BirdManager instance;

    private MainGameManager gameManager;
    private BirdsToSpawn birdSpawner;
    private UndoList undoList;
    private SkinChanger skinChanger;

    [SerializeField] public GameObject branchPrefab;
    private const int minNumberOfBranches = 4;
    private const int maxNumberOfBranches = 12;
    public int numberOfBranches = minNumberOfBranches;
    public int numberOfEmptyBranches = 2;

    public List<GameObject> branches = new List<GameObject>();
    [SerializeField] private Transform branchGroup;
    private List<Transform> branchPositions = new List<Transform>();
    private Stack<GameObject> branchesToSpawn = new Stack<GameObject>();
    private Stack<GameObject> birdsToSpawn;// = new Stack<GameObject>();

    public List<BirdController> birdControllers = new List<BirdController>();
    [SerializeField] private BirdController selectedBranch, targetBranch;
    [SerializeField] public List<Bird> selectedBirds = new List<Bird>();
    [SerializeField] private List<Bird> allBirds = new List<Bird>();

    void Awake(){
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = MainGameManager.instance;
        birdSpawner = BirdsToSpawn.instance;
        undoList = UndoList.instance;
        skinChanger = SkinChanger.instance;

        foreach (Transform branch in branchGroup){
            branchPositions.Add(branch);
        }

        SpawnBranches();
    }
    public void SpawnBranches(){
        if (branches.Count == 0){
            birdsToSpawn = new Stack<GameObject>(birdSpawner.GetBirdsToSpawn(numberOfBranches, numberOfEmptyBranches));
            for (int i = 0; i < numberOfBranches; i++) AddBranch();
            GetBirdControllers();
        }
    }
    public void GetBirdControllers(){
        foreach (GameObject branch in branches){
            birdControllers.Add(branch.transform.GetComponent<BirdController>());
        }
        foreach (BirdController bc in birdControllers){
            // bc.branchId = birdControllers.IndexOf(bc);
            // if (bc.branchId % 2 == 0) bc.isOddBranch = true;
            bc.GetBirdsOnBranch();
            allBirds.AddRange(bc.birdsOnBranch);
        }
    }
    public void AddBranch(){
        if (branches.Count < maxNumberOfBranches){
            branchPrefab.GetComponent<SpriteRenderer>().sprite = skinChanger.currentBranchSkin;
            GameObject branchToSpawn = Instantiate(branchPrefab, branchPositions[branches.Count]);
            branches.Add(branchToSpawn);

            branchToSpawn.GetComponent<BirdController>().branchId = branches.IndexOf(branchToSpawn);
            if (branchToSpawn.GetComponent<BirdController>().branchId %2 == 0) branchToSpawn.GetComponent<BirdController>().isOddBranch = true;

            if (branches.Count < (numberOfBranches - numberOfEmptyBranches) + 1){
                for (int x = 0; x < birdSpawner.MaxNumberOfBirdsOnBranch(); x++){
                    Instantiate(birdsToSpawn.Pop(), branchToSpawn.transform);
                }
            }
        }

    }
    public void SelectBranch(BirdController bc){
        if (selectedBranch != bc){
            if (selectedBirds.Count > 0 && bc.EmptySpacesOnBranch() > 0){
                targetBranch = bc;
                undoList.AddUndoTurn(selectedBranch, targetBranch, selectedBirds, new List<Bird>());
                MoveToTargetBranch();
            } else {
                if (bc.GetBirdsAmount() > 0) AddSelectedBirds(bc);
            }
        }
        else UnselectBranch();
    }
    void AddSelectedBirds(BirdController bc){
        UnselectBranch();
        selectedBranch = bc;
        BirdType bt = selectedBranch.birdsOnBranch.Peek().birdType;
        // BirdType bt = selectedBranch.birdsOnBranch.ElementAt(selectedBranch.birdsOnBranch.Count-1).birdType;
        foreach (Bird bird in selectedBranch.birdsOnBranch){
            if (bird.birdType == bt){
                bird.SelectBird();
                selectedBirds.Add(bird);
            } else break;
        }
    }
    void MoveToTargetBranch(){
        int i = selectedBirds.Count;
        int k = targetBranch.EmptySpacesOnBranch();
        
        if (targetBranch.GetBirdsAmount() == 0){
            while (i > 0){
                selectedBranch.birdsOnBranch.Pop();
                targetBranch.AddBird(selectedBirds[i-1], i);
                targetBranch.MoveBirds(targetBranch.birdsOnBranch.Peek(),
                                       targetBranch.GetBranchXPos(targetBranch.GetBirdsAmount() - 1),
                                       targetBranch.GetBranchYPos(targetBranch.GetBirdsAmount() - 1));
                if (selectedBranch.isOddBranch != targetBranch.isOddBranch) targetBranch.FlipBirds(targetBranch.birdsOnBranch.Peek());
                // selectedBranch.GetBirdsOnBranch();
                i--;
            }
            UnselectBranch();
            CheckBranch(targetBranch);
        } else {
            if (targetBranch.birdsOnBranch.Peek().birdType == selectedBirds[0].birdType){
                if (i <= k){
                    while (i > 0){
                        selectedBranch.birdsOnBranch.Pop();
                        targetBranch.AddBird(selectedBirds[i-1], i);
                        targetBranch.MoveBirds(targetBranch.birdsOnBranch.Peek(),
                                            targetBranch.GetBranchXPos(targetBranch.GetBirdsAmount() - 1),
                                            targetBranch.GetBranchYPos(targetBranch.GetBirdsAmount() - 1));
                        if (selectedBranch.isOddBranch != targetBranch.isOddBranch) targetBranch.FlipBirds(targetBranch.birdsOnBranch.Peek());
                        // selectedBranch.GetBirdsOnBranch();
                        i--;
                    }
                    UnselectBranch();
                } else {
                    int j = 0;
                    while (k > 0 && j < i){
                        selectedBranch.birdsOnBranch.Pop();
                        targetBranch.AddBird(selectedBirds[j], i);
                        targetBranch.MoveBirds(targetBranch.birdsOnBranch.Peek(),
                                            targetBranch.GetBranchXPos(targetBranch.GetBirdsAmount() - 1),
                                            targetBranch.GetBranchYPos(targetBranch.GetBirdsAmount() - 1));
                        if (selectedBranch.isOddBranch != targetBranch.isOddBranch) targetBranch.FlipBirds(targetBranch.birdsOnBranch.Peek());
                        // selectedBranch.GetBirdsOnBranch();
                        j++;
                        k--;
                    }
                    UnselectBranch();
                }
                CheckBranch(targetBranch);
            } else AddSelectedBirds(targetBranch);
        }
    }
    void CheckBranch(BirdController bc){
        if (bc.EmptySpacesOnBranch() == 0 && !bc.birdsOnBranch.Any(x => x.birdType != bc.birdsOnBranch.ElementAt(0).birdType)){
            foreach (Bird bird in bc.birdsOnBranch){
                bird.StartCoroutine(bird.FlyAway());
                allBirds.Remove(bird);
            }
            bc.ClearBirds();
        }
        if (allBirds.Count == 0) gameManager.WinLevel();
    }
    public void UnselectBranch(){
        // foreach (Bird bird in selectedBirds) selectedBranch.birdsOnBranch.Push(bird);
        if (selectedBirds.Count > 0){
        // if (selectedBranch != null){
            foreach (Bird bird in selectedBirds) bird.SelectBird();
            selectedBirds.Clear();
            selectedBranch = null;
        }
    }
    public void UndoMoveBirds(BirdController bc1, BirdController bc2, List<Bird> birds){
        int a = birds.Count;
        int b = bc2.EmptySpacesOnBranch();

        if (a <= b){
            while (a > 0){
                bc1.birdsOnBranch.Pop();
                bc2.AddBird(birds[a-1], a);
                bc2.MoveBirds(bc2.birdsOnBranch.Peek(),
                            bc2.GetBranchXPos(bc2.GetBirdsAmount() - 1),
                            bc2.GetBranchYPos(bc2.GetBirdsAmount() - 1));
                if (bc1.isOddBranch != bc2.isOddBranch) bc2.FlipBirds(bc2.birdsOnBranch.Peek());
                a--;
            }
        } else {
            int c = 0;
            while (b > 0 && c < a){
                bc1.birdsOnBranch.Pop();
                bc2.AddBird(birds[c], a);
                bc2.MoveBirds(bc2.birdsOnBranch.Peek(),
                            bc2.GetBranchXPos(bc2.GetBirdsAmount() - 1),
                            bc2.GetBranchYPos(bc2.GetBirdsAmount() - 1));
                if (bc1.isOddBranch != bc2.isOddBranch) bc2.FlipBirds(bc2.birdsOnBranch.Peek());
                c++;
                b--;
            }
        }
        
        // UnselectBranch();
    }
    public void ClearAllBranchesAndBirds(){
        foreach (GameObject branch in branches){
            Destroy(branch);
        }
        branches.Clear();
        foreach (GameObject bird in GameObject.FindGameObjectsWithTag("Bird")){
            Destroy(bird);
        }
    }
    public void ClearBirdControllers(){
        birdControllers.Clear();
        allBirds.Clear();
    }
    public int NumberOfBranchesLeft(){
        return branchPositions.Count - branches.Count;
    }
    void OnDestroy(){
        ClearBirdControllers();
        ClearAllBranchesAndBirds();
    }
}