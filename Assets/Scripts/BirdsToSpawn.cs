// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class BirdsToSpawn : MonoBehaviour
{
    public static BirdsToSpawn instance;

    private SkinChanger skinChanger;
    private string currentSkin;

    private const int maxNumberOfBirdsOnBranch = 4;
    [SerializeField] private List<GameObject> birdsToSpawn = new List<GameObject>();

    public List<GameObject> birdTypes = new List<GameObject>();

    void Awake(){
        DontDestroyOnLoad(gameObject);
        
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start(){
        skinChanger = SkinChanger.instance;
        skinChanger.getCurrentBirdSkin += GetBirdSkin;
        GetBirdSkin();
    }
    public List<GameObject> GetBirdsToSpawn(int numberOfBranches, int numberOfEmptyBranches){
        if (birdsToSpawn.Count == 0){
            for (int i = 0; i < numberOfBranches - numberOfEmptyBranches; i++){
            // Bird bird = birdTypes[Random.Range(0, birdTypes.Count)];
                for (int x = 0; x < maxNumberOfBirdsOnBranch; x++){
                    birdsToSpawn.Add(birdTypes[i]);
                }
            }
            // GetBirdSkin();
            foreach (GameObject bird in birdsToSpawn){
                SkeletonAnimation skelAni = bird.GetComponent<SkeletonAnimation>();
                skelAni.Skeleton.SetSkin(currentSkin);
            }
            RandomBirdsToSpawn(birdsToSpawn);
        }
        return birdsToSpawn;
    }
    void RandomBirdsToSpawn(List<GameObject> list){
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1){
            n--;
            int k = rng.Next(n+1);
            GameObject test = list[k];
            list[k] = list[n];
            list[n] = test;
        }
    }
    public void ClearBirdsList(){
        birdsToSpawn.Clear();
    }
    public int MaxNumberOfBirdsOnBranch(){
        return maxNumberOfBirdsOnBranch;
    }
    void GetBirdSkin(){
        currentSkin = skinChanger.currentBirdSkin;
        Debug.Log(currentSkin);
    }
}