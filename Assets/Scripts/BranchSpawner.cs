using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSpawner : MonoBehaviour
{
    [SerializeField] public GameObject branchPrefab;
    [Range (4,8)] public int numberOfFullBranches;
    private int numberOfEmptyBranches = 2;

    void Awake(){
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
