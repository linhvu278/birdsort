using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : MonoBehaviour
{
    BirdManager birdManager;

    public const int maxNumberOfBirds = 4;
    public int branchId;// {get;set;}
    public bool isOddBranch = false;// {get;set;}
    private float branchYpos;
    private float[] branchXpos = {1f, 2.5f, 4, 5.5f};

    [SerializeField] public Stack<Bird> birdsOnBranch;// = new Stack<Bird>();
    [SerializeField] Button branchButton;

    // Start is called before the first frame update
    void Awake(){
        birdsOnBranch = new Stack<Bird>(transform.GetComponentsInChildren<Bird>());

    }
    void Start()
    {
        birdManager = BirdManager.instance;

        branchYpos = transform.position.y;

        // foreach (Bird bird in transform.GetComponentsInChildren<Bird>()){
        //     birdsOnBranch.Push(bird);
        // }
        for (int i = 0; i < birdsOnBranch.Count; i++){
            // MoveBirds(birdsOnBranch.ElementAt(birdsOnBranch.Count-1-i), branchXpos[i], 0.75f);
            birdsOnBranch.ElementAt(birdsOnBranch.Count-1-i).SetBirdPos(branchXpos[i], 0.75f, isOddBranch);
            // birdsOnBranch.ElementAt(birdsOnBranch.Count-1-i).FlipTheBird(isOddBranch);
        }

        branchButton.onClick.AddListener(OnTouch);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.touchCount > 0){
        //     Touch touch = Input.GetTouch(0);
        //     if (touch.phase == TouchPhase.Began) Debug.Log("test");
        // }
    }
    public void OnTouch(){
        birdManager.SelectBranch(gameObject);
    }
    public void AddBirds(List<Bird> selectedBirds){
        foreach (Bird bird in selectedBirds){
            bird.transform.SetParent(transform);
            bird.StartCoroutine(bird.FlipTheBird(isOddBranch));
        }
    }
    public void MoveBirds(Bird bird, float x, float y){
        bird.SetMovingDirection(x, y);
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
}
