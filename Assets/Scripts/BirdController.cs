// using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BirdController : MonoBehaviour, IPointerClickHandler
{
    private BirdManager birdManager;
    private BirdsToSpawn birdSpawner;
    private PlayButtonGroupManager playButtonGroupManager;
    private UndoList undoList;

    [SerializeField] public Stack<Bird> birdsOnBranch = new Stack<Bird>();
    public int branchId;
    public bool isOddBranch = false;
    private float[] branchXpos = {3f, 4f, 5f, 6f};
    private float[] branchYpos = {0.1f, 0f, 0, 0};
    
    private int maxNumberOfBirds;// = 4;

    void Start()
    {
        birdManager = BirdManager.instance;
        birdSpawner = BirdsToSpawn.instance;
        playButtonGroupManager = PlayButtonGroupManager.instance;
        undoList = UndoList.instance;

        maxNumberOfBirds = birdSpawner.MaxNumberOfBirdsOnBranch();
    }
    public void OnPointerClick(PointerEventData eventData){
        if (playButtonGroupManager.canSwap && birdsOnBranch.Count > 1) SwapBirds();
        else birdManager.SelectBranch(GetComponent<BirdController>());
    }
    public void GetBirdsOnBranch(){
        birdsOnBranch = new Stack<Bird>(transform.GetComponentsInChildren<Bird>());
        for (int i = 0; i < birdsOnBranch.Count; i++){
            birdsOnBranch.ElementAt(i).SetBirdPos(branchXpos[birdsOnBranch.Count - 1 - i],
                                                  branchYpos[birdsOnBranch.Count - 1 - i],
                                                  birdsOnBranch.Count - 1 - i);
        }
    }
    // public void AddBirds(List<Bird> selectedBirds){
    //     foreach (Bird bird in selectedBirds){
    //         bird.transform.SetParent(transform);
    //     }
    // }
    public void AddBird(Bird bird, int i){
        bird.transform.SetParent(transform);
        bird.SortOrder(i);
        birdsOnBranch.Push(bird);
    }
    public void MoveBirds(Bird bird, float x, float y){
        bird.SetMovingDirection(x, y);
    }
    public void FlipBirds(Bird bird){
        bird.StartCoroutine(bird.FlipTheBird());
    }
    public void ClearBirds(){
        birdsOnBranch.Clear();
    }
    public void SwapBirds(){
        undoList.AddUndoTurn(this, this, new List<Bird>(), birdsOnBranch.ToList());
        List<Bird> list = birdsOnBranch.OrderBy(x => Random.Range(0, birdsOnBranch.Count)).ToList();
        birdsOnBranch.Clear();
        foreach (Bird bird in list) birdsOnBranch.Push(bird);

        for (int i = 0; i < birdsOnBranch.Count; i++){
            birdsOnBranch.ElementAt(i).SetMovingDirection(branchXpos[birdsOnBranch.Count-1-i], branchYpos[birdsOnBranch.Count-1-i]);
        }

        playButtonGroupManager.AfterSwap();

        birdManager.UnselectBranch();
    }
    public void RevertBirds(List<Bird> birdPos){
        // revert birds to original positions
        birdsOnBranch.Clear();
        birdPos.Reverse();
        foreach (Bird bird in birdPos) birdsOnBranch.Push(bird);

        for (int i = 0; i < birdsOnBranch.Count; i++){
            birdsOnBranch.ElementAt(i).SetMovingDirection(branchXpos[birdsOnBranch.Count-1-i], branchYpos[birdsOnBranch.Count-1-i]);
        }
    }
    public int EmptySpacesOnBranch(){
        return maxNumberOfBirds - birdsOnBranch.Count;
    }
    public int GetBirdsAmount(){
        return birdsOnBranch.Count;
    }
    public float GetBranchXPos(int index){
        return branchXpos[index];
    }
    public float GetBranchYPos(int index){
        return branchYpos[index];
    }
}
