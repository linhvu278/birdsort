using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : MonoBehaviour
{
    private BirdManager birdManager;
    private BirdsToSpawn birdSpawner;

    private int maxNumberOfBirds;// = 4;
    public int branchId;// {get;set;}
    public bool isOddBranch = false;// {get;set;}
    // private float branchYpos;
    private float[] branchXpos = {-1.8f, -0.6f, 0.6f, 1.8f};
    private float[] branchYpos = {0.75f, 0.6f, 0.5f, 0.45f};

    [SerializeField] public Stack<Bird> birdsOnBranch = new Stack<Bird>();
    public Button branchButton;

    // Start is called before the first frame update
    void Awake(){

    }
    void Start()
    {
        birdManager = BirdManager.instance;
        birdSpawner = BirdsToSpawn.instance;

        maxNumberOfBirds = birdSpawner.MaxNumberOfBirdsOnBranch();

        // branchYpos = transform.position.y;

        branchButton.onClick.AddListener(OnTouch);
    }
    public void GetBirdsOnBranch(){
        birdsOnBranch = new Stack<Bird>(transform.GetComponentsInChildren<Bird>());
        for (int i = 0; i < birdsOnBranch.Count; i++){
            birdsOnBranch.ElementAt(birdsOnBranch.Count-1-i).SetBirdPos(branchXpos[i], branchYpos[i], isOddBranch);
        }
    }
    public void OnTouch(){
        birdManager.SelectBranch(GetComponent<BirdController>());
    }
    public void AddBirds(List<Bird> selectedBirds){
        foreach (Bird bird in selectedBirds){
            bird.transform.SetParent(transform);
        }
    }
    public void MoveBirds(Bird bird, float x, float y){
        bird.SetMovingDirection(x, y);
        bird.StartCoroutine(bird.FlipTheBird(isOddBranch, bird.birdSpeed));
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
