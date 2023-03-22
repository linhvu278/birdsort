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
    [SerializeField] public Transform buttonGroup;
    private List<Button> branchButtons = new List<Button>();
    // [SerializeField] private List<GameObject> birdsToSpawn = new List<GameObject>();
    private Stack<GameObject> birdsToSpawn;// = new Stack<GameObject>();

    void Awake(){
        
    }
    // Start is called before the first frame update
    void Start()
    {
        birdSpawner = BirdsToSpawn.instance;
        birdManager = BirdManager.instance;

        foreach (Transform branch in branchGroup){
            branchPositions.Add(branch);
        }
        
        foreach (Transform button in buttonGroup){
            branchButtons.Add(button.GetComponent<Button>());
            button.GetComponent<Button>().enabled = false;
        }

        // birdsToSpawn = birdSpawner.GetBirdsToSpawn(numberOfBranches, numberOfEmptyBranches);
        birdsToSpawn = new Stack<GameObject>(birdSpawner.GetBirdsToSpawn(numberOfBranches, numberOfEmptyBranches));

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
                    // GameObject birdToAdd = birdsToSpawn[Random.Range(0, birdsToSpawn.Count)];
                    // birdsToSpawn.Remove(birdToAdd);

                    Instantiate(birdsToSpawn.Pop(), branchToSpawn.transform);
                    // Debug.Log(birdsToSpawn.Pop());
                }
            }
        }
        birdManager.GetBirdControllers();
    }
}
