using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class BranchSpawner : MonoBehaviour
{
    [SerializeField] public Bird birdsToSpawn;
    [SerializeField] public GameObject branchPrefab;
    private const int minNumberOfBranches = 4;
    private const int maxNumberOfBranches = 10;
    [Range (minNumberOfBranches, maxNumberOfBranches)] public int numberOfBranches;
    private int numberOfEmptyBranches = 2;
    [SerializeField] private Transform branchGroup;
    [SerializeField] public Transform[] branchPositions;// = new Transform[maxNumberOfBranches];
    [SerializeField] public Transform[] buttonPositions = new Transform[maxNumberOfBranches];
    public Stack<GameObject> branchesToSpawn = new Stack<GameObject>();

    void Awake(){
        // branchPositions = branchGroup.GetComponentsInChildren<Transform>();
        // int i = 0;
        // while (branchesToSpawn.Count < numberOfBranches){
        //     GameObject newBranch = Instantiate(branchPrefab, branchPositions[i]);
        //     branchesToSpawn.Push(newBranch);
        //     Debug.Log("branch added " + i);
        //     // add birds to branch here
        //     i++;
        // }
        // for (int j = 0; j < numberOfEmptyBranches; j++){
        //     branchesToSpawn.Push(branchPrefab);
        // }
        
    }
    // Start is called before the first frame update
    void Start()
    {

        
    }
}
