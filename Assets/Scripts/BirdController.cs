using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : MonoBehaviour
{
    BirdManager birdManager;

    public List<Bird> birdsOnBranch = new List<Bird>();
    // private List<Transform> birdsPositions = new List<Transform>();
    private const int maxNumberOfBirds = 4;
    // [SerializeField] public Bird[] birdsOnBranch = new Bird[maxNumberOfBirds];
    // [SerializeField] private Transform[] birdsPositions = new Transform[maxNumberOfBirds];
    [SerializeField] Button branchButton;
    public int emptySpacesOnBranch {get; set;}
    // private BoxCollider2D branchCollider;

    // Start is called before the first frame update
    void Start()
    {
        birdManager = BirdManager.instance;

        for (int i = 0; i < transform.GetComponentsInChildren<Bird>().Length; i++){
            birdsOnBranch.Add(transform.GetComponentsInChildren<Bird>()[i]);
        }
        // birdsPositions = transform.GetComponentsInChildren<Transform>();

        branchButton.onClick.AddListener(OnTouch);
        // branchButton.onClick.AddListener(MoveBirds);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.touchCount > 0){
        //     Touch touch = Input.GetTouch(0);
        //     if (touch.phase == TouchPha se.Began) Debug.Log("test");
        // }
    }

    public void OnTouch(){
        if (birdManager.SelectedBranch() == null) {
            if (birdsOnBranch.Count > 0) birdManager.SelectBranch(gameObject);
        } else {
            emptySpacesOnBranch = maxNumberOfBirds - birdsOnBranch.Count;
            // if (birdManager.selectedBirds.Count <= emptySpacesOnBranch){
                birdManager.SelectBranch(null);
                birdManager.MoveToTargetBranch(gameObject);
                RemoveBirds();
            // }
        }
    }

    void MoveBirds(){
        if (birdManager.SelectedBranch() != null){
            
        }
    }

    void RemoveBirds(){
        // remove selected birds from list
    }
}
