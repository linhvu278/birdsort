// using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public static BirdManager instance;

    private WinScreenManager winScreenManager;

    public List<GameObject> branches = new List<GameObject>();
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
        winScreenManager = WinScreenManager.instance;

        // branches = GameObject.FindGameObjectsWithTag("Branch").ToList();

        // if (branches.Count > 0){
        //     for (int a = 0; a < branches.Count; a++){
        //         birdControllers.Add(branches[a].GetComponent<BirdController>());
        //         Debug.Log("i love penis");
        //     }
        // }
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
                // targetBranch.birdsOnBranch.Push(selectedBirds[i-1]);
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
                        // targetBranch.birdsOnBranch.Push(selectedBirds[i-1]);
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
                        // targetBranch.birdsOnBranch.Push(selectedBirds[i-1]);
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
        if (allBirds.Count == 0) winScreenManager.EnableWinScreen(true);
    }
    void UnselectBranch(){
        if (selectedBirds.Count > 0){
            foreach (Bird bird in selectedBirds) bird.SelectBird();
            selectedBirds.Clear();
            selectedBranch = null;
        }
    }
}
