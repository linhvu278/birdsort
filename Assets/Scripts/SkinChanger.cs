using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SkinChanger : MonoBehaviour
{
    public static SkinChanger instance;

    private ShopManager shopManager;

    public string currentBirdSkin;
    public Sprite currentBranchSkin;
    public delegate void CurrentBirdSkin();
    public CurrentBirdSkin getCurrentBirdSkin;

    // [SerializeField] private List<Image> bgList = 

    void Awake(){
        if (instance == null) instance = this;
    }
    void Start(){
        shopManager = ShopManager.instance;

        SetBirdSkin(shopManager.GetSelectedBirdItem());
        SetBranchSkin(shopManager.GetSelectedBranchItem());
    }
    public void SetBGSkin(ShopItemObj item){
        SpriteRenderer bg = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();
        bg.sprite = item.itemSpriteData;
    }
    public void SetBirdSkin(ShopItemObj item){
        currentBirdSkin = item.itemID.ToString();
        GameObject[] birds = GameObject.FindGameObjectsWithTag("Bird");
        foreach (GameObject bird in birds){
            SkeletonAnimation skelAni = bird.GetComponent<SkeletonAnimation>();
            // skelAni.Skeleton.Skin = skelAni.Skeleton.Data.FindSkin(item.itemID++.ToString());
            skelAni.Skeleton.SetSkin(currentBirdSkin);
            // skelAni.initialSkinName = (currentBirdSkin);
        }
        if (getCurrentBirdSkin != null) getCurrentBirdSkin.Invoke();
    }
    public void SetBranchSkin(ShopItemObj item){
        currentBranchSkin = item.itemSpriteData;
        GameObject[] branches = GameObject.FindGameObjectsWithTag("Branch");
        foreach (GameObject branch in branches){
            branch.GetComponent<SpriteRenderer>().sprite = currentBranchSkin;
        }
    }
}
