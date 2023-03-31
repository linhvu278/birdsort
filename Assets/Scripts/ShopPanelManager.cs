using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelManager : MonoBehaviour
{
    public static ShopPanelManager instance;

    private ShopManager shopManager;

    private ToggleGroup toggleGroup;

    [Header ("Shop item prefab")]
    [SerializeField] private ShopItem shopItemPrefab;
    
    [SerializeField] private List<ShopItem> shopItemList = new List<ShopItem>();
    public ShopPanelType shopPanelType;

    void Awake(){
        if (instance == null) instance = this;
    }
    void Start(){
        shopManager = ShopManager.instance;
        toggleGroup = GetComponent<ToggleGroup>();

        GetShopItemList();
    }
    public void GetShopItemList(){
        switch (shopPanelType){
            case ShopPanelType.scenery:
                foreach (ShopItemObj sceneryItem in shopManager.SceneryItemList()){
                    shopItemPrefab.SetItem(sceneryItem, toggleGroup);
                    Instantiate(shopItemPrefab, transform);
                }
                break;
            case ShopPanelType.bird:
                foreach (ShopItemObj birdItem in shopManager.BirdItemList()){
                    shopItemPrefab.SetItem(birdItem, toggleGroup);
                    Instantiate(shopItemPrefab, transform);
                }
                break;
            case ShopPanelType.branch:
                foreach (ShopItemObj branchItem in shopManager.BranchItemList()){
                    shopItemPrefab.SetItem(branchItem, toggleGroup);
                    Instantiate(shopItemPrefab, transform);
                }
                break;
        }
    }
    // public void SetShopItem(ShopItem shopItem){
        
    // }
}

public enum ShopPanelType { scenery, bird, branch }