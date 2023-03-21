using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsToSpawn : MonoBehaviour
{
    public static BirdsToSpawn instance;

    private const int maxNumberOfBirdsOnBranch = 4;
    public List<GameObject> birdsToSpawn = new List<GameObject>();

    public List<GameObject> birdTypes = new List<GameObject>();

    void Awake(){
        if (instance == null){
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public List<GameObject> GetBirdsToSpawn(int numberOfBranches, int numberOfEmptyBranches){
        for (int i = 0; i < numberOfBranches - numberOfEmptyBranches; i++){
        // Bird bird = birdTypes[Random.Range(0, birdTypes.Count)];
            for (int x = 0; x < maxNumberOfBirdsOnBranch; x++){
                birdsToSpawn.Add(birdTypes[i]);
            }
        }
        return birdsToSpawn;
    }
    public int MaxNumberOfBirdsOnBranch(){
        return maxNumberOfBirdsOnBranch;
    }
}
