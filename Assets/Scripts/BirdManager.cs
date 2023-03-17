// using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public static BirdManager instance;

    public List<GameObject> branches = new List<GameObject>();
    private List<BirdController> birdControllers = new List<BirdController>();
    private BirdController selectedBc;
    [SerializeField] private GameObject selectedBranch, targetBranch;
    [SerializeField] public List<Bird> selectedBirds = new List<Bird>();
    [SerializeField] private List<Bird> allBirds = new List<Bird>();

    void Awake(){
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // foreach (GameObject branch in GameObject.FindGameObjectsWithTag("Branch").ToList()){
        foreach(GameObject branch in branches){
            birdControllers.Add(branch.GetComponent<BirdController>());
        }
        foreach (BirdController bc in birdControllers){
            bc.branchId = birdControllers.IndexOf(bc);
            if (bc.branchId % 2 == 0) bc.isOddBranch = true;
            allBirds.AddRange(bc.birdsOnBranch);
        }

        // branches.AddRange(GameObject.FindGameObjectsWithTag("Branch"));
        // for (int i = 0; i < branches.Count; i++){
        //     BirdController bc = branches[i].GetComponent<BirdController>();
        //     birdControllers.Add(bc);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectBranch(GameObject newBranch){
        if (selectedBranch != newBranch){
            if (selectedBranch != null && newBranch.GetComponent<BirdController>().EmptySpacesOnBranch() >= selectedBirds.Count){
                targetBranch = branches.Find(x => x == newBranch);
                MoveToTargetBranch(targetBranch);
            } else {
                if (newBranch.GetComponent<BirdController>().GetBirdsAmount() > 0){
                    UnselectBranch();
                    selectedBranch = branches.Find(x => x == newBranch);
                    AddSelectedBirds(birdControllers[branches.IndexOf(selectedBranch)]);
                }
            }
        }
        else return;
    }
    public void AddSelectedBirds(BirdController bc){
        if (selectedBirds != null) selectedBirds.Clear();
        if (bc.GetBirdsAmount() > 0){
            for (int i = 0; i < bc.GetBirdsAmount(); i++){
                if (bc.birdsOnBranch.ElementAt(i).birdType == bc.birdsOnBranch.Peek().birdType){
                    selectedBirds.Add(bc.birdsOnBranch.ElementAt(i));
                    bc.birdsOnBranch.ElementAt(i).SelectBird(true);
                } else break;
            }
        }
    }
    public void MoveToTargetBranch(GameObject newBranch){
        if (selectedBirds != null){
            targetBranch = branches.Find(x => x == newBranch);
            BirdController targetBc = birdControllers[branches.IndexOf(targetBranch)];
            BirdController selectedBc = birdControllers[branches.IndexOf(selectedBranch)];
            if (targetBc.GetBirdsAmount() == 0 || targetBc.GetBirdsAmount() > 0 && targetBc.birdsOnBranch.Peek().birdType == selectedBirds[0].birdType){
                int i = selectedBirds.Count;
                while (i > 0){
                    selectedBc.birdsOnBranch.Pop();
                    targetBc.birdsOnBranch.Push(selectedBirds[i-1]);
                    targetBc.MoveBirds(targetBc.birdsOnBranch.Peek(), targetBc.GetBranchXPos(targetBc.birdsOnBranch.Count-1), 0.75f);
                    i--;
                }
                targetBc.AddBirds(selectedBirds);
                CheckBranch(targetBc);
            }
            UnselectBranch();
        }
    }
    void CheckBranch(BirdController bc){
        if (bc.EmptySpacesOnBranch() == 0 && !bc.birdsOnBranch.Any(x => x.birdType != bc.birdsOnBranch.ElementAt(0).birdType)){
            foreach (Bird bird in bc.birdsOnBranch){
                bird.StartCoroutine(bird.FlyAway());
                allBirds.Remove(bird);
            }
            bc.birdsOnBranch.Clear();
        }
        if (allBirds.Count == 0) Debug.Log("you're winner");
    }
    void UnselectBranch(){
        selectedBranch = null;
        if (selectedBirds.Count > 0){
            foreach (Bird bird in selectedBirds) bird.SelectBird(false);
            selectedBirds.Clear();
        }
    }
}
