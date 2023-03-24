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

    [SerializeField] public GameObject branchPrefab;
    private const int minNumberOfBranches = 4;
    private const int maxNumberOfBranches = 8;
    public int numberOfBranches = minNumberOfBranches;
    public int numberOfEmptyBranches = 2;

    public List<GameObject> branches = new List<GameObject>();
    [SerializeField] private Transform branchGroup;
    private List<Transform> branchPositions = new List<Transform>();
    private Stack<GameObject> branchesToSpawn = new Stack<GameObject>();
    // [SerializeField] private List<GameObject> birdsToSpawn = new List<GameObject>();
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

        foreach (Transform branch in branchGroup){
            branchPositions.Add(branch);
        }

        SpawnBranches();
    }
    public void SpawnBranches(){
        if (branches.Count == 0){
            // birdsToSpawn = birdSpawner.GetBirdsToSpawn(numberOfBranches, numberOfEmptyBranches);
            birdsToSpawn = new Stack<GameObject>(birdSpawner.GetBirdsToSpawn(numberOfBranches, numberOfEmptyBranches));

            for (int i = 0; i < numberOfBranches; i++){
                GameObject branchToSpawn = Instantiate(branchPrefab, branchPositions[i]);
                // if (i % 2 != 0) branchToSpawn.GetComponent<SpriteRenderer>().flipX = true;
                branches.Add(branchToSpawn);
                // birdControllers.Add(branchToSpawn.GetComponent<BirdController>());
                
                if (branches.Count < (numberOfBranches - numberOfEmptyBranches) + 1){
                    for (int x = 0; x < birdSpawner.MaxNumberOfBirdsOnBranch(); x++){
                        // GameObject birdToAdd = birdsToSpawn[Random.Range(0, birdsToSpawn.Count)];
                        // birdsToSpawn.Remove(birdToAdd);

                        Instantiate(birdsToSpawn.Pop(), branchToSpawn.transform);
                        // Debug.Log(birdsToSpawn.Pop());
                    }
                }
            }
            GetBirdControllers();
        }
    }
    public void GetBirdControllers(){
        foreach (GameObject branch in branches){
            birdControllers.Add(branch.transform.GetComponent<BirdController>());
        }
        foreach (BirdController bc in birdControllers){
            bc.branchId = birdControllers.IndexOf(bc);
            if (bc.branchId % 2 == 0) bc.isOddBranch = true;
            bc.GetBirdsOnBranch();
            allBirds.AddRange(bc.birdsOnBranch);
        }
    }
    public void SelectBranch(BirdController bc){
        if (selectedBranch != bc){
            if (selectedBirds.Count > 0 && bc.EmptySpacesOnBranch() > 0){
                // targetBranch = birdControllers.Find(x => x.branchId == bc.branchId);
                targetBranch = bc;
                MoveToTargetBranch();
            } else {
                if (bc.GetBirdsAmount() > 0){
                    // UnselectBranch();
                    // selectedBranch = birdControllers.Find(x => x.branchId == bc.branchId);
                    // selectedBranch = bc;
                    AddSelectedBirds(bc);
                }
            }
        }
        else UnselectBranch();
    }
    void AddSelectedBirds(BirdController bc){
        // if (selectedBirds != null) selectedBirds.Clear();
        UnselectBranch();
        if (bc.GetBirdsAmount() > 0){
            selectedBranch = bc;
            for (int i = 0; i < bc.GetBirdsAmount(); i++){
                if (bc.birdsOnBranch.ElementAt(i).birdType == bc.birdsOnBranch.Peek().birdType){
                    selectedBirds.Add(bc.birdsOnBranch.ElementAt(i));
                    bc.birdsOnBranch.ElementAt(i).SelectBird();
                } else break;
            }
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
                i--;
            }
            // targetBranch.AddBirds(selectedBirds);
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
                        i--;
                    }
                    // targetBranch.AddBirds(selectedBirds);
                    UnselectBranch();
                } else {
                    int j = 0;
                    while (k > 0 && j < i){
                        targetBranch.AddBird(selectedBirds[j], i);
                        targetBranch.MoveBirds(targetBranch.birdsOnBranch.Peek(),
                                            targetBranch.GetBranchXPos(targetBranch.GetBirdsAmount() - 1),
                                            targetBranch.GetBranchYPos(targetBranch.GetBirdsAmount() - 1));
                        if (selectedBranch.isOddBranch != targetBranch.isOddBranch) targetBranch.FlipBirds(targetBranch.birdsOnBranch.Peek());
                        selectedBranch.birdsOnBranch.Pop();
                        // selectedBirds[j].SelectBird(false);
                        // selectedBirds.Remove(selectedBirds[j]);
                        j++;
                        k--;
                        // i--;
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
    void UnselectBranch(){
        if (selectedBirds.Count > 0){
            foreach (Bird bird in selectedBirds) bird.SelectBird();
            selectedBirds.Clear();
            selectedBranch = null;
        }
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
    void OnDestroy(){
        ClearBirdControllers();
        ClearAllBranchesAndBirds();
    }
}
