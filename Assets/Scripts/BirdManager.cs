using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public static BirdManager instance;

    [SerializeField] private List<GameObject> branches = new List<GameObject>();
    [SerializeField] private GameObject selectedBranch, targetBranch;
    [SerializeField] public List<Bird> selectedBirds = new List<Bird>();
    [SerializeField] private List<Bird> allBirds = new List<Bird>();

    void Awake(){
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        branches.AddRange(GameObject.FindGameObjectsWithTag("Branch"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectBranch(GameObject newBranch){
        if (newBranch != null && selectedBranch != newBranch){
            selectedBranch = newBranch;
            AddSelectedBirds();
        } else {
            selectedBranch = null;
            selectedBirds.Clear();
        }
    }
    public void AddSelectedBirds(){
        BirdController bc = selectedBranch.GetComponent<BirdController>();
        // Bird firstBird = bc.birdsOnBranch[0];
        // selectedBirds.Add(firstBird);
        for (int i = 0; i < bc.birdsOnBranch.Count; i++){
            if (bc.birdsOnBranch[i].birdType == bc.birdsOnBranch[0].birdType){
                selectedBirds.Add(bc.birdsOnBranch[i]);
            } else break;
        }
    }
    public void MoveToTargetBranch(GameObject newBranch){
        targetBranch = newBranch;
        BirdController bc = targetBranch.GetComponent<BirdController>();
        if (bc.emptySpacesOnBranch >= selectedBirds.Count){
            for (int i = 0; i < selectedBirds.Count; i++){
                bc.birdsOnBranch.Add(selectedBirds[i]);
            }
            Debug.Log("moved to new branch " + targetBranch);
        }
    }

    public GameObject SelectedBranch(){
        return selectedBranch;
    }
}
