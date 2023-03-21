using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.Linq;
using UnityEngine.UI;

public class BranchSpawner : MonoBehaviour
{
    private BirdsToSpawn birdSpawner;
    private BirdManager birdManager;

    [SerializeField] public GameObject branchPrefab;
    private const int minNumberOfBranches = 4;
    private const int maxNumberOfBranches = 8;
    [Range (minNumberOfBranches, maxNumberOfBranches)] public int numberOfBranches;
    [Range (1, 2)] public int numberOfEmptyBranches;
    [SerializeField] private Transform branchGroup;
    private List<Transform> branchPositions = new List<Transform>();
    private Stack<GameObject> branchesToSpawn = new Stack<GameObject>();
    [SerializeField] private List<Button> branchButtons = new List<Button>();
    [SerializeField] private List<GameObject> birdsToSpawn = new List<GameObject>();

    void Awake(){
        birdSpawner = BirdsToSpawn.instance;
        birdManager = BirdManager.instance;

        foreach (Transform branch in branchGroup){
            branchPositions.Add(branch);
        }
        
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("BranchButton")){
            button.GetComponent<Button>().enabled = false;
            branchButtons.Add(button.GetComponent<Button>());
        }
        birdsToSpawn = birdSpawner.GetBirdsToSpawn(numberOfBranches, numberOfEmptyBranches);
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfBranches; i++){
            GameObject branchToSpawn = Instantiate(branchPrefab, branchPositions[i]);
            // if (i % 2 != 0) branchToSpawn.GetComponent<SpriteRenderer>().flipX = true;
            birdManager.branches.Add(branchToSpawn);
            // birdManager.birdControllers.Add(branchToSpawn.GetComponent<BirdController>());

            BirdController bc = branchToSpawn.GetComponent<BirdController>();
            bc.branchButton = branchButtons[i];
            branchButtons[i].enabled = true;
            
            if (birdManager.branches.Count < (numberOfBranches - numberOfEmptyBranches) + 1){
                for (int x = 0; x < birdSpawner.MaxNumberOfBirdsOnBranch(); x++){
                    GameObject birdToAdd = birdsToSpawn[Random.Range(0, birdsToSpawn.Count)];
                    Instantiate(birdToAdd, branchToSpawn.transform);
                    // bc.birdsOnBranch.Push(birdToAdd);
                    // birdToAdd.transform.SetParent(birdManager.branches.Peek().transform);
                    birdsToSpawn.Remove(birdToAdd);
                }
            }
        }
        birdManager.GetBirdControllers();
    }
}
