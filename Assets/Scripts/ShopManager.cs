using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    private SkinChanger skinChanger;

    [Header ("User money")]
    [SerializeField] private int money;
    public delegate void MoneyDelegate();
    public MoneyDelegate onMoneyChanged;

    [Header ("Shop items lists")]
    [SerializeField] private List<ShopItemObj> sceneryList = new List<ShopItemObj>();
    [SerializeField] private List<ShopItemObj> birdList = new List<ShopItemObj>();
    [SerializeField] private List<ShopItemObj> branchList = new List<ShopItemObj>();

    [Header ("Owned items lists")]
    [SerializeField] private List<ShopItemObj> ownedSceneryList = new List<ShopItemObj>();
    [SerializeField] private List<ShopItemObj> ownedBirdList = new List<ShopItemObj>();
    [SerializeField] private List<ShopItemObj> ownedBranchList = new List<ShopItemObj>();

    [Space]
    [SerializeField] private ShopItemObj selectedSceneryItem, selectedBirdItem, selectedBranchItem;
    
    // [SerializeField] private ShopItem_scenery defaultSceneryItem;

    void Awake(){
        if (instance == null) instance = this;
    }
    void Start(){
        skinChanger = SkinChanger.instance;

        AddShopItem(sceneryList[0]);
        AddShopItem(birdList[0]);
        AddShopItem(branchList[0]);

        if (selectedSceneryItem == null) selectedSceneryItem = ownedSceneryList[0];
        if (selectedBirdItem == null) selectedBirdItem = ownedBirdList[0];
        if (selectedBranchItem == null) selectedBranchItem = ownedBranchList[0];
        skinChanger.SetBGSkin(selectedSceneryItem);
        skinChanger.SetBirdSkin(selectedBirdItem);
        skinChanger.SetBranchSkin(selectedBranchItem);

        money = 999999;
    }
    public void AddShopItem(ShopItemObj item){
        // SetMoney(-item.itemPrice);
        switch (item.shopItemType){
            case ShopItemType.item_scenery:
                ownedSceneryList.Add(item);
                break;
            case ShopItemType.item_bird:
                ownedBirdList.Add(item);
                break;
            case ShopItemType.item_branch:
                ownedBranchList.Add(item);
                break;
        }
    }
    public List<ShopItemObj> SceneryItemList(){
        return sceneryList;
    }
    public List<ShopItemObj> BirdItemList(){
        return birdList;
    }
    public List<ShopItemObj> BranchItemList(){
        return branchList;
    }
    public ShopItemObj GetSelectedSceneryItem(){
        return selectedSceneryItem;
    }
    public ShopItemObj GetSelectedBirdItem(){
        return selectedBirdItem;
    }
    public ShopItemObj GetSelectedBranchItem(){
        return selectedBranchItem;
    }
    public void SelectSceneryItem(ShopItemObj item){
        selectedSceneryItem = item;
        skinChanger.SetBGSkin(selectedSceneryItem);
    }
    public void SelectBirdItem(ShopItemObj item){
        selectedBirdItem = item;
        skinChanger.SetBirdSkin(selectedBirdItem);
    }
    public void SelectBranchItem(ShopItemObj item){
        selectedBranchItem = item;
        skinChanger.SetBranchSkin(selectedBranchItem);
    }
    public bool IsItemOwned(ShopItemObj item){
        switch (item.shopItemType){
            case ShopItemType.item_scenery: return ownedSceneryList.Contains(item);
            case ShopItemType.item_bird: return ownedBirdList.Contains(item);
            case ShopItemType.item_branch: return ownedBranchList.Contains(item);
            default: return false;
        }
    }
    public bool IsItemSelected(ShopItemObj item){
        switch (item.shopItemType){
            case ShopItemType.item_scenery: return item == selectedSceneryItem;
            case ShopItemType.item_bird: return item == selectedBirdItem;
            case ShopItemType.item_branch: return item == selectedBranchItem;
            default: return false;
        }
    }
    public int GetMoney(){
        return money;
    }
    public void SetMoney(int value){
        money += value;
        onMoneyChanged.Invoke();
    }
}
